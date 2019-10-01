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
    public class ZhongxinParseService : IZhongxinParseService
    {
        private int _memberId;
        public int Member { set { _memberId = value; } }
        private readonly ILogger<ZhongxinParseService> _logger;
        private readonly XwZxContext _xwZxContext;
        public ZhongxinParseService(ILogger<ZhongxinParseService> logger
            , XwZxContext xwZxContext)
        {
            _logger = logger;
            _xwZxContext = xwZxContext;
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
                        SaveBank(details[0].CardNum);
                        SaveBankBillDetail(details);
                    }
                    UpdateMailIsPrased(mail);
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError($"解析账单出错!用户:{ _memberId},邮件ID{mails[i].Uid}");
                }
            }

            UpdateBankCardSate();
        }

        private void UpdateMailIsPrased(MailSrc mail)
        {
            mail.IsPrased = true;
            _xwZxContext.Entry(mail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _xwZxContext.SaveChanges();
        }

        private void SaveBankBillDetail(List<BankBillDetail> details)
        {
            foreach (var detail in details)
            {
                _xwZxContext.BankBillDetails.Add(detail);
            }

            _xwZxContext.SaveChanges();
        }

        public List<BankBillDetail> ToBillDetail(MailSrc mail)
        {
            var content = mail.BodyText;
            var details = new List<BankBillDetail>();
            try
            {
               // var year = new Regex(@"账单\s+(\d\d\d\d)/\d\d").Matches(content)[0].Groups[1].Value;

                var matchs = new Regex(@"(\d\d\d\d\d\d)\n(\d+)\n利息(.+)\nRMB\n(\d+\.\d+)").Matches(content);

                foreach (Match m in matchs)
                {
                    var detail = new BankBillDetail()
                    {
                        Bank = BankCardType.中信银行,
                        MemberID = mail.MemberId,
                        MailId = mail.Id
                    };

                    detail.CardNum = "3000";

                    detail.TreadTime = DateTime.ParseExact( m.Groups[1].Value, "yyMMdd", new CultureInfo("zh-CN", true));
                    detail.Unit = "人民币";
                    detail.SellerName = "利息";
                    detail.Amount = decimal.Parse(m.Groups[4].Value);
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
            var mails = _xwZxContext.MailSrcs
               .Where(m => m.MemberId == _memberId
                   && m.From == BankMailUrl.ZHONGXIN
                   && m.IsPrased == false
                   && m.Sublic.Contains("中信银行信用卡电子账单")
                   && m.BodyText.Contains("利息")).ToList();

            return mails;
        }

        private void SaveBank(string CardNum)
        {
            if (_xwZxContext.BankCards.Any(b => b.MemberId == _memberId
                    && b.Bank == BankCardType.中信银行) == false)
            {
                _xwZxContext.BankCards.Add(new BankCard()
                {
                    MemberId = _memberId,
                    CardNum = CardNum,
                    Bank = BankCardType.中信银行,
                    LastSyncTime = DateTime.Now,
                    LastSyncIsOk = true,
                });
                _xwZxContext.SaveChanges();
            }
        }


        private void UpdateBankCardSate()
        {
            var bank = _xwZxContext.BankCards
                .Where(b => b.MemberId == _memberId && b.Bank == BankCardType.中信银行)
                .FirstOrDefault();

            if (bank != null)
            {
                bank.LastSyncIsOk = true;
                bank.LastSyncTime = DateTime.Now;
                _xwZxContext.SaveChanges();
            }
        }
    }
}
