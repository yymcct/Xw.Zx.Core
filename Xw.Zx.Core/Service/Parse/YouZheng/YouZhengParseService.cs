using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public class YouZhengParseService : ParseServiceBase,IYouZhengParseService
    {
        //TODO
        private int _memberId;
        public int Member { set { _memberId = value; } }
        private readonly ILogger<YouZhengParseService> _logger;
        public YouZhengParseService(ILogger<YouZhengParseService> logger
            , XwZxContext xwZxContext) : base(xwZxContext, BankCardType.邮政银行)
        {
            _logger = logger;
        }
        public void Parse()
        {
            var mails = GetNeedParse();

            for (var i = 0; i < mails.Count; i++)
            {
                try
                {
                    var mail = mails[i];
                    var details = ToBillDetail(mail);
                    if (details != null && details.Count > 0)
                    {
                        SaveBank(details[0].CardNum, _memberId);
                        SaveBankBillDetail(details);
                    }
                    UpdateMailIsPrased(mail);
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError($"解析账单出错!用户:{ _memberId},邮件ID{mails[i].Uid}");
                }
            }

            UpdateBankCardSate(_memberId);
        }

        public List<BankBillDetail> ToBillDetail(MailSrc mail)
        {
            //TODO 修改
            var content = mail.BodyText;
            var details = new List<BankBillDetail>();
            try
            {
                var matchs = new Regex(@"(\d{4}/\d{2}/\d{2})利息交易￥(\d+\.\d{2})").Matches(content);

                foreach (Match m in matchs)
                {
                    var detail = new BankBillDetail()
                    {
                        Bank = BankCardType.邮政银行,
                        MemberID = mail.MemberId,
                        MailId = mail.Id,
                        CardNum = "0000",
                        Unit = "人民币",
                        SellerName = "利息",
                        TreadTime = DateTime.ParseExact(m.Groups[1].Value, "yyyy/MM/dd", new CultureInfo("zh-CN", true)),
                        Amount = decimal.Parse(m.Groups[2].Value)
                    };

                    details.Add(detail);
                }

                var matchWyjs = new Regex(@"(\d{4}/\d{2}/\d{2})违约金￥(\d+\.\d{2})").Matches(content);

                foreach (Match m in matchWyjs)
                {
                    var detail = new BankBillDetail()
                    {
                        Bank = BankCardType.邮政银行,
                        MemberID = mail.MemberId,
                        MailId = mail.Id,
                        CardNum = "0000",
                        Unit = "人民币",
                        SellerName = "违约金",
                        TreadTime = DateTime.ParseExact(m.Groups[1].Value, "yyyy/MM/dd", new CultureInfo("zh-CN", true)),
                        Amount = decimal.Parse(m.Groups[2].Value)
                    };
                    details.Add(detail);
                }

                return details;
            }
            catch (Exception ex)
            {

                //throw new Exception("账单解析异常:" + ex.Message);
            }

            return null;
        }

        private List<MailSrc> GetNeedParse()
        {
            //TODO 修改
            var mails = _xwZxContext.MailSrcs
               .Where(m => m.MemberId == _memberId
                   && m.From == BankMailUrl.YOUZHENG
                   && m.IsPrased == false
                   && m.Sublic.Contains("邮储银行信用卡电子账单")
                   && m.BodyText.Contains("利息交易")).ToList();

            return mails;
        }
       
    }
}
