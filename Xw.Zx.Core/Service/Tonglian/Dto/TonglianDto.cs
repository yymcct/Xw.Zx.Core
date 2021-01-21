using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Service
{

    //TODO
    public class Tonglianrequest
    {
        public class RequestBase
        {
            private readonly TonglianOption _tonglianOption;
            public RequestBase(TonglianOption tonglianOption)
            {
                _tonglianOption = tonglianOption;
            }

            public string sign
            {
                get
                {
                    var dic = GetType()
                        .GetProperties()
                        .Where(q => q.Name != "sign" )
                        .OrderBy(q => q.Name)
                        .ToDictionary(q => q.Name, q => q.GetValue(this).ToString());
                    string str = "";
                    foreach (var d in dic)
                    {
                        if (string.IsNullOrEmpty(str))
                        {
                            str = $"{d.Key}={d.Value}";
                        }
                        else
                        {
                            str += $"&{d.Key}={d.Value}";
                        }

                    }
                    
                    return CreateMD5(str).ToUpper();
                }
            }

            public Dictionary<string, string> ToDic()
            {
                var d = GetType().GetProperties()              
                    .OrderBy(q => q.Name)
                    .ToDictionary(q => q.Name, q => q.GetValue(this).ToString());

                return d;
            }

            public static string CreateMD5(string input)
            {
                using (var md5 = MD5.Create())
                {
                    var result = md5.ComputeHash(Encoding.Default.GetBytes(input));
                    var strResult = BitConverter.ToString(result);
                    return strResult.Replace("-", "");
                }
            }
        }


        public class QrcodePay : RequestBase
        {
            public QrcodePay(BiqilinOption biqilinOption) : base(biqilinOption)
            {

            }
            public string merchant_account { get; set; }
            public string out_store_name { get; set; }
            public string out_cashier_name { get; set; }
            public string local_time
            {
                get
                {
                    TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    return Convert.ToInt64(ts.TotalMilliseconds).ToString();
                }
            }
            public string out_goods_name { get; set; }
            public string out_trade_no { get; set; }
            public string terminal_type { get; set; }
            public string amount { get; set; }
            public string pay_type { get; set; }
            public string fee_type { get; set; }
            public string partner_code { get; set; }
            public string notify_url { get; set; }
            public string token { get; set; }
        }

        public class JsapiPay : RequestBase
        {
            public JsapiPay(BiqilinOption biqilinOption) : base(biqilinOption)
            {

            }
            public string merchant_account { get; set; }
            public string out_store_name { get; set; }
            public string out_cashier_name { get; set; }
            public string local_time
            {
                get
                {
                    TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    return Convert.ToInt64(ts.TotalMilliseconds).ToString();
                }
            }
            public string out_goods_name { get; set; }
            public string out_trade_no { get; set; }
            public string terminal_type { get; set; }
            public string amount { get; set; }
            public string pay_type { get; set; }
            public string fee_type { get; set; }
            public string partner_code { get; set; }
            public string out_sub_appid { get; set; }
            public string out_sub_openid { get; set; }
            public string notify_url { get; set; }
            public string token { get; set; }
        }

        public class Query : RequestBase
        {
            public Query(BiqilinOption biqilinOption) : base(biqilinOption)
            {

            }
            public string merchant_account { get; set; }
            public string out_store_name { get; set; }
            public string out_cashier_name { get; set; }
            public string order_no { get; set; }
            public string partner_code { get; set; }
            public string token { get; set; }
        }
    }


    public class TonglianRespone
    {
        public class Respone<T> where T : class
        {
            public T detail { get; set; }
            public string errorCode { get; set; }
            public string errorMsg { get; set; }
            public string flag { get; set; }
        }
        public class KeyDownLoad
        {
            public string merchantName { get; set; }
            public string serviceMerchantName { get; set; }
            public string serviceMerchantAccount { get; set; }
            public string merchantAccount { get; set; }
            public string serviceMerchantPhone { get; set; }
            public string sign { get; set; }
            public string token { get; set; }
        }

        public class QrcodePay
        {
            public string merchantAccount { get; set; }
            public string outStoreName { get; set; }
            public string outCashierName { get; set; }
            public string codeUrl { get; set; }
            public string qrCodeUrl { get; set; }
            public string qrCodeName { get; set; }
            public string outGoodsName { get; set; }
            public string outOrderNo { get; set; }
            public string orderNo { get; set; }
            public string orderStatus { get; set; }
            public string orderTime { get; set; }
            public string payType { get; set; }
            public string amount { get; set; }
            public string rateAfterAmount { get; set; }
            public string orderStatusMsg { get; set; }
            public string sign { get; set; }
        }

     

        public class Query
        {
            public string storeAccount { get; set; }
            public string cashierAccount { get; set; }
            public string goodsName { get; set; }
            public string orderNo { get; set; }
            public string outOrderNo { get; set; }
            public string orderStatus { get; set; }
            public string orderTime { get; set; }
            public string payType { get; set; }
            public string amount { get; set; }
            public string rateAfterAmount { get; set; }
            public string orderStatusMsg { get; set; }
            public string sign { get; set; }
        }
    }



}
