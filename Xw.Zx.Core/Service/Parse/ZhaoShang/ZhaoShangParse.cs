using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service.Parse
{
    public class ZhaoShangParse : IMailParse
    {
        public List<BankBillDetail> Parse(string content)
        {
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
                    new Exception($"ZhaoShangParse:{content}");
                }

                if ((arrays.Length - 1 - 10) % 6 != 0)
                    new Exception($"ZhaoShangParse:{content}");

                for (int i = 10; i < arrays.Length - 1;)
                {
                    var detail = new BankBillDetail();

                    detail.BankCardId = arrays[i++];

                    detail.TreadTime = DateTime.ParseExact(arrays[i++] + " " + arrays[i++], "yyyyMMdd HH:mm:ss", new CultureInfo("zh-CN", true));
                    detail.Unit = arrays[i++];
                    detail.SellerName = arrays[i++];
                    detail.Amount = decimal.Parse(arrays[i++]);
                    details.Add(detail);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("账单解析异常:"+ex.Message);
            }
                      
            return details;
        }
    }
}
