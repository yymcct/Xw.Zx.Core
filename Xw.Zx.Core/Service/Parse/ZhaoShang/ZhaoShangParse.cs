using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service.Parse
{
    public class ZhaoShangParse : IMailParse
    {
        public List<BankBillDetail> Parse(MailSrc mail)
        {
            return Parse2(mail);
        }

        public List<BankBillDetail> Parse1(MailSrc mail)
        {
            var content = mail.BodyText;
            var details = new List<BankBillDetail>();
            try
            {
                var arrays = content.Split("\n\n\n");
                if (arrays[4] != "卡号末四位"
                    || arrays[5] != "交易日期"
                    || arrays[6] != "时间"
                    || arrays[7] != "币别"
                    || arrays[8] != "商户名称"
                    || arrays[9] != "交易金额")
                {
                    throw new Exception($"ZhaoShangParse:{content}");
                }

                if ((arrays.Length - 1 - 10) % 6 != 0)
                    throw new Exception($"ZhaoShangParse:{content}");

                for (int i = 10; i < arrays.Length - 1;)
                {
                    var detail = new BankBillDetail()
                    {
                        MemberID = mail.MemberId,
                        MailId = mail.Id
                    };

                    detail.CardNum = arrays[i++];

                    detail.TreadTime = DateTime.ParseExact(arrays[i++] + " " + arrays[i++], "yyyyMMdd HH:mm:ss", new CultureInfo("zh-CN", true));
                    detail.Unit = arrays[i++];
                    detail.SellerName = arrays[i++];
                    detail.Amount = decimal.Parse(arrays[i++]);
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

        public List<BankBillDetail> Parse2(MailSrc mail)
        {
            var content = mail.BodyText;
            var details = new List<BankBillDetail>();
            try
            {
                var year = new Regex(@"账单\s+(\d\d\d\d)/\d\d").Matches(content)[0].Groups[1].Value;

                var matchs = new Regex(@"(\d+)\n循环利息\n￥\s(\d+\.\d+)").Matches(content);

                foreach (Match m in matchs)
                {
                    var detail = new BankBillDetail()
                    {
                        MemberID = mail.MemberId,
                        MailId = mail.Id
                    };

                    detail.CardNum = "0000";

                    detail.TreadTime = DateTime.ParseExact(year + m.Groups[1].Value, "yyyyMMdd", new CultureInfo("zh-CN", true));
                    detail.Unit = "人民币";
                    detail.SellerName = "循环利息";
                    detail.Amount = decimal.Parse(m.Groups[2].Value);
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
    }
}
