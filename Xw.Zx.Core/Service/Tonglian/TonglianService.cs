using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xw.Zx.Core.Helper;

namespace Xw.Zx.Core.Service
{
    public class TonglianService : ITonglianService
    {
        private readonly HttpClient _client;
        public TonglianService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Method", "Post");
        }

        public BiqilinRespone.QrcodePay CreateQrcodePayUrl(Biqilin_Product biqilin_Product)
        {
            var str = "appid=00000051&cusid=990581007426001&randomstr=82712208&signtype=RSA&trxid=112094120001088317&version=11";
            var rsaKey = "MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGBAJgHMGYsspghvP+yCbjLG43CkZuQ3YJyDcmEKxvmgblITfmiTPx2b9Y2iwDT9gnLGExTDm1BL2A8VzMobjaHfiCmTbDctu680MLmpDDkVXmJOqdlXh0tcLjhN4+iDA2KkRqiHxsDpiaKT6MMBuecXQbJtPlVc1XjVhoUlzUgPCrvAgMBAAECgYAV9saYTGbfsdLOF5kYo0dve1JxaO7dFMCcgkV+z2ujKtNmeHtU54DlhZXJiytQY5Dhc10cjb6xfFDrftuFcfKCaLiy6h5ETR8jyv5He6KH/+X6qkcGTkJBYG1XvyyFO3PxoszQAs0mrLCqq0UItlCDn0G72MR9/NuvdYabGHSzEQJBAMXB1/DUvBTHHH4LiKDiaREruBb3QtP72JQS1ATVXA2v6xJzGPMWMBGQDvRfPvuCPVmbHENX+lRxMLp39OvIn6kCQQDEzYpPcuHW/7h3TYHYc+T0O6z1VKQT2Mxv92Lj35g1XqV4Oi9xrTj2DtMeV1lMx6n/3icobkCQtuvTI+AcqfTXAkB6bCz9NwUUK8sUsJktV9xJN/JnrTxetOr3h8xfDaJGCuCQdFY+rj6lsLPBTnFUC+Vk4mQVwJIE0mmjFf22NWW5AkAmsVaRGkAmui41Xoq52MdZ8WWm8lY0BLrlBJlvveU6EPqtcZskWW9KiU2euIO5IcRdpvrB6zNMgHpLD9GfMRcPAkBUWOV/dH13v8V2Y/Fzuag/y5k3/oXi/WQnIxdYbltad2xjmofJ7DbB7MJqiZZD8jlr8PCZPwRNzc5ntDStc959";

            var rsaHelper = new RSAHelper( RSAType.RSA, System.Text.Encoding.UTF8, rsaKey);
            var restrict= rsaHelper.Encrypt(str);

            throw new NotImplementedException();

        }

        public BiqilinRespone.JsapiPay CreateWeixinJsApi(Biqilin_Product biqilin_Product)
        {
            throw new NotImplementedException();
        }

        public BiqilinRespone.Query QueryOrder(string biqilinOrderNo)
        {
            throw new NotImplementedException();
        }
    }
}
