using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service.MailSync
{
    public class MailSync: IMailSync
    {
        private int _memberId;
        public int MemberId {
            set {
                _memberId = value;
            }
        }
        private IMailService _mailService;
        public IMailService MailService {
            set {
                _mailService = value;
            }
        }
        private readonly ILogger<SyncService> _logger;
        private readonly XwZxContext _xwZxContext;
        public MailSync(ILogger<SyncService> logger
            , XwZxContext xwZxContext)
        {
            _logger = logger;
            _xwZxContext = xwZxContext;
        }


        #region 同步目录
        /// <summary>
        /// 同步该邮箱所有银行卡目录
        /// </summary>
        /// <returns></returns>
        public int SyncMailDirToDb()
        {

            var zhaoshang =  SyncMailDirToDb(BankMailUrl.ZHAOSHANG);
            var zhongxin =  SyncMailDirToDb(BankMailUrl.ZHONGXIN);

            //_logger.LogError($"邮箱目录同步,用户{ _memberId},邮件ID{.Id},Exception:{ex.Message}");
            return  zhaoshang.Result + zhongxin.Result;
        }

        private async Task<int> SyncMailDirToDb(string mailurl)
        {
            int cnt = 0;
            if (IsFirstSyncDir(mailurl))
            {
                cnt = await FirstSyncDirAsync(mailurl);
            }
            else
            {
                cnt = await SyncDirAsync(mailurl);
            }
            return cnt;
        }

        private async Task<int> SyncDirAsync(string mailurl)
        {
            var lastmail = GetLastMail(mailurl);

            var mails = await _mailService.SearchByFrom(mailurl, 1, 100);

            mails.Where(m => m.Date > lastmail.SendTime).ToList();

            SaveMailDir(mails);

            return mails.Count;
        }

        private async Task<int> FirstSyncDirAsync(string mailurl)
        {
            int pagesize = 100;
            int page = 1;
            int cnt = 0;
            do
            {
                var mails = await _mailService.SearchByFrom(mailurl, page++, pagesize);

                if (mails.Count == 0) break;

                SaveMailDir(mails);

                cnt += mails.Count;

            } while (page < 5);

            return cnt;
        }

        private bool IsFirstSyncDir(string mailurl)
        {
            return _xwZxContext.MailSrcs
                .Where(m => m.MemberId == _memberId
                    && m.From == mailurl).Any();
        }

        private MailSrc GetLastMail(string mailurl)
        {
            return _xwZxContext.MailSrcs
                .Where(m => m.MemberId == _memberId
                    && m.From == mailurl).OrderByDescending(m => m.SendTime).First();
        }

        private void SaveMailDir(List<MailInfoDto> mails)
        {
            foreach (var m in mails)
            {
                try
                {
                    if (_xwZxContext.MailSrcs.Any(t => t.MemberId == _memberId && t.Uid == m.Id) == false)
                    {
                        var tmail = new MailSrc()
                        {
                            MemberId = _memberId,
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
                    _logger.LogError($"保存邮件Info出错!用户:{ _memberId},邮件ID{m.Id},Exception:{ex.Message}");
                }
            }

        }

        #endregion

        #region 同步邮件详情
        public int SyncMailToDb()
        {
            var zhaoshang =  SyncMailToDb_Zhaoshang();
            var zhongxin =  SyncMailToDb_ZhongXin();
            //TODO 中信等

            return zhaoshang.Result + zhongxin.Result;
        }

        private async Task<int> SyncMailToDb_Zhaoshang()
        {
            var mails = _xwZxContext.MailSrcs
                    .Where(m => m.MemberId == _memberId
                        && m.From == BankMailUrl.ZHAOSHANG
                        && m.IsPrased == false
                        && m.Sublic.Contains("招商银行信用卡电子账单")
                        && string.IsNullOrEmpty(m.BodyText)).ToList();

            await GetMailSaveDbAsync(mails);
            return mails.Count;
        }

        private async Task<int> SyncMailToDb_ZhongXin()
        {
            var mails = _xwZxContext.MailSrcs
                    .Where(m => m.MemberId == _memberId
                        && m.From == BankMailUrl.ZHAOSHANG
                        && m.IsPrased == false
                        && m.Sublic.Contains("中信银行信用卡电子账单")
                        && string.IsNullOrEmpty(m.BodyText)).ToList();

            await GetMailSaveDbAsync(mails);
            return mails.Count;
        }

        private async Task GetMailSaveDbAsync(List<MailSrc> mailDirs)
        {
            for (var i = 0; i < mailDirs.Count; i++)
            {
                try
                {
                    var mail = mailDirs[i];
                    var tmpmail = await _mailService.GetMail(mail.Uid);
                    if (tmpmail != null)
                    {
                        mail.Body = tmpmail.Body;
                        mail.BodyText = tmpmail.BodyText;
                    }
                    _xwZxContext.Entry(mail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _xwZxContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"保存邮件详情出错!用户:{ _memberId},邮件ID{ mailDirs[i].Uid}");
                }
            }
        }
        #endregion
    }
}
