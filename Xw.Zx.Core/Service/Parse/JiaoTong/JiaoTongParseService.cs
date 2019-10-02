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
    public class JiaoTongParseService : ParseServiceBase, IJiaoTongParseService
    {
        private int _memberId;
        public int Member { set { _memberId = value; } }
        private readonly ILogger<JiaoTongParseService> _logger;
        public JiaoTongParseService(ILogger<JiaoTongParseService> logger
            , XwZxContext xwZxContext):base(xwZxContext, BankCardType.交通银行)//TODO
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
                var matchs = new Regex(@"(\d{4}/\d{2}/\d{2})本期利息RMB\s(\d+\.\d{2})").Matches(content);

                foreach (Match m in matchs)
                {
                    var detail = new BankBillDetail()
                    {
                        Bank = _ThisBankCardType,
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

                var matchWyjs = new Regex(@"(\d{4}/\d{2}/\d{2})违约金RMB\s(\d+\.\d{2})").Matches(content);

                foreach (Match m in matchWyjs)
                {
                    var detail = new BankBillDetail()
                    {
                        Bank = _ThisBankCardType,
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

                var matchs3 = new Regex(@"(\d{4}/\d{2}/\d{2})本期利息RMB(\d+\.\d{2})").Matches(content);

                foreach (Match m in matchs3)
                {
                    var detail = new BankBillDetail()
                    {
                        Bank = _ThisBankCardType,
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

                var matchWyjs3 = new Regex(@"(\d{4}/\d{2}/\d{2})违约金RMB(\d+\.\d{2})").Matches(content);

                foreach (Match m in matchWyjs3)
                {
                    var detail = new BankBillDetail()
                    {
                        Bank = _ThisBankCardType,
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
                   && m.From == BankMailUrl.JIAOTONG
                   && m.IsPrased == false
                   && m.Sublic.Contains("交通银行信用卡")
                   && m.BodyText.Contains("本期利息")).ToList();

            return mails;
        }

    }
}
