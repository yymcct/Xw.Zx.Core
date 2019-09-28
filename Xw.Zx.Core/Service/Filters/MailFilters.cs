using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service.Filters
{
    public class MailFilters
    {
        private int _memberId;

        private readonly ILogger<SyncService> _logger;
        private readonly XwZxContext _xwZxContext;
        public MailFilters(ILogger<SyncService> logger
            , XwZxContext xwZxContext)
        {
            _logger = logger;
            _xwZxContext = xwZxContext;
        }
        public List<MailSrc> Filter(BankCardType bankCardType, int memberId)
        {
            switch (bankCardType)
            {
                case BankCardType.招商银行:
                    break;
                case BankCardType.中信银行:
                    break;
                case BankCardType.华夏银行:
                    break;
                case BankCardType.平安银行:
                    break;
                case BankCardType.广大银行:
                    break;
                case BankCardType.民生银行:
                    break;
                case BankCardType.浦发银行:
                    break;
            }
        }

        private List<MailSrc> ZhaoShang()
        {
            _xwZxContext.MailSrcs
                    .Where(m => m.MemberId == _memberId
                        && m.From == BankMailUrl.ZHAOSHANG
                        && m.Sublic.Contains("招商银行信用卡电子账单")
                        && m.IsPrased==false
                        && string.IsNullOrEmpty(m.BodyText))
                    .ToList();
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
    }
}
