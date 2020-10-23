using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Xw.Zx.Core.Service
{
    public class BiqilinService : IBiqilinService
    {
        private readonly HttpClient _client;
        private readonly BiqilinOption _biqilinOption;
        private readonly ILogger<BiqilinService> _logger;
        public BiqilinService(ILogger<BiqilinService> logger, IOptionsMonitor<BiqilinOption> biqilinOption)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Method", "Post");
            _biqilinOption = biqilinOption.CurrentValue;
        }
        public string CreateQrcodePayUrl(Biqilin_Product biqilin_Product)
        {
            const string url = "https://pay.biqilin.com/api/pay/partner/qrcode_pay.do";
            //var key = GetToken(); 获取一次就不再改变           
            var token = "326b5d06397c08c379b447f1917d6ce38db006d43c31da324b4358eda54911f8";
            var dto = new Biqilinrequest.QrcodePay(_biqilinOption);
            dto.pay_type = biqilin_Product.Biqilin_PayType == Biqilin_PayType.微信 ? "WEIXIN_PAY" : "ALI_PAY";
            dto.out_goods_name = biqilin_Product.Name;
            dto.out_trade_no = biqilin_Product.Timestamp;
            dto.amount = biqilin_Product.Amount.ToString("F2");

            dto.merchant_account = _biqilinOption.MerchantAccount;
            dto.out_store_name = "债减减";
            dto.out_cashier_name = "APP";
            dto.terminal_type = "3";
            dto.fee_type = "CNY";
            dto.partner_code = _biqilinOption.PartnerCode;
            dto.notify_url = "http://139.155.8.217/api/Biqilin/Notifyurl";
            dto.token = token; //key.token;

            var res = Post<BiqilinRespone.QrcodePay>(url, dto.ToDic());
            return res.codeUrl;
        }

        public string CreateWeixinJsApi(Biqilin_Product biqilin_Product)
        {
            throw new NotImplementedException();
        }

        public BiqilinRespone.KeyDownLoad GetToken()
        {
            const string url = "https://pay.biqilin.com/api/user/partner/key_download.do";
            var biqilin_Key_Dto = new Biqilinrequest.KeyDownLoad(_biqilinOption);

            biqilin_Key_Dto.merchant_account = _biqilinOption.MerchantAccount;
            biqilin_Key_Dto.merchant_pwd = _biqilinOption.MerchantPwd;
            biqilin_Key_Dto.partner_code = _biqilinOption.PartnerCode;

            return Post<BiqilinRespone.KeyDownLoad>(url, biqilin_Key_Dto.ToDic());
        }

        private TResult Post<TResult>(string url, Dictionary<string, string> dic) where TResult : class
        {
            LogsArray(dic);
            var resStr = _client.PostAsync(url, new FormUrlEncodedContent(dic)).Result.Content
                 .ReadAsStringAsync()
                 .Result;
            _logger.LogWarning("FROM碧麒麟:" + resStr);
            var res = JsonConvert.DeserializeObject<BiqilinRespone.Respone<TResult>>(resStr);
            if (res.flag != "1")
            {
                throw new Exception($"碧麒麟支付接口异常, 代码:{res.errorCode},消息:{res.errorMsg}");
            }
            return res.detail;
        }
        private void LogsArray(Dictionary<string, string> dict)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("TO碧麒麟:");
            foreach (var kv in dict)
            {
                sb.AppendFormat("{0}:{1};", kv.Key, kv.Value);
            }
            string str = sb.ToString();

            _logger.LogWarning(str);
        }

    }
}
