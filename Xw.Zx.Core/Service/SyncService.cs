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

        public async Task<(bool, string)> SyncAsync(PostSyncMailDto postSyncMailDto)
        {
            _postSyncMailDto = postSyncMailDto;
            _postSyncMailDto.Mail = "68771803@qq.com";
            if (_postSyncMailDto.Mail.Contains("@qq.com"))
            {
                _mailService = _qqMailService.Init(_postSyncMailDto.Sid, _postSyncMailDto.Cookie);
            }
            await ZhaoShangSyncAsync();
            return (true, "");
        }

        private async Task ZhaoShangSyncAsync()
        {
            //同步招商银行
            var mils = await _mailService.SearchByFrom(ZhaoShangMailUrl);

            //只取信用消费清单
            mils = mils.Where(m => m.Subj == "招商银行信用卡电子账单").OrderByDescending(m => m.Date).ToList();

            //检查最后一次同步的邮件时间
            var lastSyncDate = GetDbLastMail(ZhaoShangMailUrl, _postSyncMailDto.MemberId);

            if (lastSyncDate != null)
            {
                mils = mils.Where(m => m.Date > lastSyncDate).ToList();
            }

            //_logger.LogWarning("开始同步");
            foreach (var m in mils)
            {
                try
                {
                    var mail = await _mailService.GetMail(m.Id);
                    mail.MemberId = _postSyncMailDto.MemberId;
                    mail.SendTime = m.Date;
                    _xwZxContext.MailSrcs.Add(mail);
                    _xwZxContext.SaveChanges();

                    IMailParse mailParse = new ZhaoShangParse();
                    var details = mailParse.Parse(mail);
                    if (details != null && details.Count>0)
                    {
                        SaveBank(details[0].CardNum);
                        mail.IsPrased = true;
                        foreach (var detail in details)
                        {
                            _xwZxContext.BankBillDetails.Add(detail);
                        }
                        _xwZxContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                Thread.Sleep(200);
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
                });
                _xwZxContext.SaveChanges();
            }
        }

        /// <summary>
        /// 获取最后一次同步的ID
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
    }
}
