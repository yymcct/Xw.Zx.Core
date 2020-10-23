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

namespace Xw.Zx.Core.Service
{
    public class BiqilinService : IBiqilinService
    {
        private readonly HttpClient _client;
        private readonly BiqilinOption _biqilinOption;
        public BiqilinService(IOptionsMonitor<BiqilinOption> biqilinOption)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Method", "Post");
            _biqilinOption = biqilinOption.CurrentValue;
        }
        public string CreateQrcodePayUrl(Biqilin_Product biqilin_Product)
        {
            const string url = "https://pay.biqilin.com/api/pay/partner/qrcode_pay.do";
            var key = GetToken();

            var dto = new Biqilinrequest.QrcodePay(_biqilinOption);

            dto.out_goods_name = "测试商品";
            dto.out_trade_no = "20200222";
            dto.amount = "0.10";

            dto.merchant_account = "1100000522";
            dto.out_store_name = "admin";
            dto.out_cashier_name = "APP";                       
            dto.terminal_type = "3";            
            dto.pay_type = "WEIXIN_PAY";
            dto.fee_type = "CNY";
            dto.partner_code = _biqilinOption.PartnerCode;
            dto.notify_url = "http://www.baidu.com";
            dto.token = key.token;

            var res=  Post<BiqilinRespone.QrcodePay>(url, dto.ToDic());
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
            var resStr = _client.PostAsync(url, new FormUrlEncodedContent(dic)).Result.Content
                 .ReadAsStringAsync()
                 .Result;

            var res = JsonConvert.DeserializeObject<BiqilinRespone.Respone<TResult>>(resStr);
            if (res.flag != "1")
            {
                throw new Exception($"碧麒麟支付接口异常, 代码:{res.errorCode},消息:{res.errorMsg}");
            }
            return res.detail;
        }

    }
}
