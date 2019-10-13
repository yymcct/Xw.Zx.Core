using System;
using System.Collections.Generic;
using System.Text;
using qcloudsms_csharp;
using qcloudsms_csharp.json;
using qcloudsms_csharp.httpclient;

namespace Xw.Zx.Core.Service
{
    public class SmsService : ISmsService
    {
        private const int appid = 1400269404;

        private const string appkey = "bc7d74f0c14e0ebc266fd6f4a8ce9b7e";

        // 短信模板ID，需要在短信应用中申请
        private const int templateId = 442664; // NOTE: 这里的模板ID`7839`只是一个示例，真实的模板ID需要在短信控制台中申请
                                               //templateId 7839 对应的内容是"您的验证码是: {1}"
                                               // 签名
        private const string smsSign = "多宝格"; // NOTE: 这里的签名只是示例，请使用真实的已申请的签名, 签名参数使用的是`签名内容`，而不是`签名ID`


        public bool Send(string phone, int number)
        {
            bool isOk = false;
            try
            {
                string[] phoneNumbers = { phone };
                SmsSingleSender ssender = new SmsSingleSender(appid, appkey);
                ssender.sendWithParam("86", phoneNumbers[0],
                    templateId, new[] { number.ToString(), "5" }, smsSign, "", "");  // 签名参数未提供或者为空时，会使用默认签名发送短信       

                return true;
            }
            catch (Exception e)
            {
                //TODO
            }
            return isOk;
        }
    }
}
