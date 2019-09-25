using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service.Parse;

namespace Xw.Zx.Core.Service
{
    public class SyncService : ISyncService
    {
        private readonly ILogger<SyncService> _logger;
        private readonly XwZxContext _xwZxContext;
        private readonly IQQMailService _qqMailService;
        private PostSyncMailDto _postSyncMailDto;
        private IMailService _mailService;
        private string ZhaoShangMailUrl = "ccsvc@message.cmbchina.com";

        public SyncService(ILogger<SyncService> logger
            , IQQMailService qqMailService
            , XwZxContext xwZxContext)
        {
            _logger = logger;
            _qqMailService = qqMailService;
            _xwZxContext = xwZxContext;
        }

        public async Task<PostSyncMailResuleDto> SyncAsync(PostSyncMailDto postSyncMailDto)
        {
            _postSyncMailDto = postSyncMailDto;

            _mailService = _qqMailService.Init(_postSyncMailDto.Sid, _postSyncMailDto.Cookie);

            return await ZhaoShangSyncAsync();
        }

        private async Task<PostSyncMailResuleDto> ZhaoShangSyncAsync()
        {
            //同步目录
            var isok = await SyncMailsInfo();

            //补全疑似账单的详情
            var mail = await SyncNeedAllInfoMailAsync();

            PraseNeedPare();

            return BuildSyncInfo();

        }

        public PostSyncMailResuleDto BuildSyncInfo()
        {
            var res = new PostSyncMailResuleDto()
            {
                LastSyncTime = DateTime.Now,
                BankBillAmount = "0"
            };
            var mail = _xwZxContext.MailSrcs.Where(m=>m.MemberId == _postSyncMailDto.MemberId).OrderBy(m => m.SendTime).FirstOrDefault();
            if (mail != null)
            {
                res.LastSyncTime = mail.SendTime;
            }
            var amount = _xwZxContext.BankBillDetails.Where(b=>b.MemberID==_postSyncMailDto.MemberId).Sum(b => b.Amount);

            res.BankBillAmount = amount.ToString();
            return res;
        }

        private void PraseNeedPare()
        {
            var mails = _xwZxContext.MailSrcs
               .Where(m => m.MemberId == _postSyncMailDto.MemberId
                   && m.From == ZhaoShangMailUrl
                   && m.IsPrased == false
                   && m.Sublic.Contains("招商银行信用卡电子账单")
                   && m.BodyText.Contains("循环利息")).ToList();

            IMailParse mailParse = new ZhaoShangParse();
            for (var i = 0; i < mails.Count; i++)
            {
                try
                {
                    var mail = mails[i];
                    var details = mailParse.Parse(mail);
                    if (details != null && details.Count > 0)
                    {
                        SaveBank(details[0].CardNum);

                        foreach (var detail in details)
                        {
                            _xwZxContext.BankBillDetails.Add(detail);
                        }
                        mail.IsPrased = true;
                        _xwZxContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"解析账单出错!用户:{ _postSyncMailDto.MemberId},邮件ID{mails[i].Uid}");
                }

            }
        }

        private async Task<List<MailSrc>> SyncNeedAllInfoMailAsync()
        {
            var mails = _xwZxContext.MailSrcs
                .Where(m => m.MemberId == _postSyncMailDto.MemberId
                    && m.From == ZhaoShangMailUrl
                    && m.IsPrased == false
                    && m.Sublic.Contains("招商银行信用卡电子账单")
                    && string.IsNullOrEmpty(m.BodyText)).ToList();


            for (var i = 0; i < mails.Count; i++)
            {
                try
                {
                    var mail = mails[i];
                    var tmpmail = await _mailService.GetMail(mail.Uid);
                    if (tmpmail != null)
                    {
                        mail.Body = tmpmail.Body;
                        mail.BodyText = tmpmail.BodyText;
                    }

                    _xwZxContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"保存邮件详情出错!用户:{ _postSyncMailDto.MemberId},邮件ID{mails[i].Uid}");
                }
            }

            return mails;
        }

        /// <summary>
        /// 获取本次需要同步的邮件批次
        /// </summary>
        /// <returns></returns>
        private async Task<bool> SyncMailsInfo()
        {
            int pagesize = 50;
            List<MailInfoDto> mails = null;
            if (string.IsNullOrEmpty(_postSyncMailDto.IsBefore))
            {
                mails = await _mailService.SearchByFrom(ZhaoShangMailUrl, "1", pagesize.ToString());
            }
            else
            {
                int cnt = _xwZxContext.MailSrcs.Where(m=>m.MemberId==_postSyncMailDto.MemberId).Count();

                mails = await _mailService.SearchByFrom(ZhaoShangMailUrl, (((int)cnt / pagesize) + 1).ToString(), pagesize.ToString());
            }
            SavemailInfo(mails);
            return true;
        }

        private void SavemailInfo(List<MailInfoDto> mails)
        {
            foreach (var m in mails)
            {
                try
                {
                    if (_xwZxContext.MailSrcs.Any(t => t.MemberId == _postSyncMailDto.MemberId && t.Uid == m.Id) == false)
                    {
                        var tmail = new MailSrc()
                        {
                            MemberId = _postSyncMailDto.MemberId,
                            Uid = m.Id,
                            Sublic = m.Subj,
                            From = m.From,
                            To = m.To,
                            SendTime = m.Date,
                            AddTime = DateTime.Now
                        };

                        _xwZxContext.MailSrcs.Add(tmail);
                        _xwZxContext.SaveChanges();
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError($"保存邮件Info出错!用户:{ _postSyncMailDto.MemberId},邮件ID{m.Id}");
                }
            }

        }

        private void SaveBank(string CardNum)
        {
            if (_xwZxContext.BankCards.Any(b => b.MemberId == _postSyncMailDto.MemberId
                    && b.CardNum == CardNum) == false)
            {
                _xwZxContext.BankCards.Add(new BankCard()
                {
                    MemberId = _postSyncMailDto.MemberId,
                    CardNum = CardNum,
                    Bank = BankCardType.招商银行,
                    LastSyncTime = DateTime.Now,
                    LastSyncIsOk = true,
                });
                _xwZxContext.SaveChanges();
            }
        }

        /// <summary>
        /// 获取最近一次的同步时间
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        private DateTime? GetDbLastMail(string fromMail, int memberid)
        {
            var mail = _xwZxContext
                            .MailSrcs
                            .Where(m => m.MemberId == memberid && m.From == fromMail)
                            .OrderByDescending(m => m.SendTime)
                            .FirstOrDefault();

            return mail?.SendTime;
        }

        /// <summary>
        /// 获取最远的一次同步时间
        /// </summary>
        /// <param name="fromMail"></param>
        /// <param name="memberid"></param>
        /// <returns></returns>
        private DateTime? GetDbFarthestMail(string fromMail, int memberid)
        {
            var mail = _xwZxContext
                            .MailSrcs
                            .Where(m => m.MemberId == memberid && m.From == fromMail)
                            .OrderBy(m => m.SendTime)
                            .FirstOrDefault();

            return mail?.SendTime;
        }
    }
}
