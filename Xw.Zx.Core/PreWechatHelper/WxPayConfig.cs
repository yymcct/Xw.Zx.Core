using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.PreWechatHelper
{
    public class WxPayConfig
    {
        public static Service.WechatSubLedgerOption Config=null;
        public static string ContenxtPaht="";

        public static void Init(IConfiguration configuration) {
            if (Config == null)
                Config = new Service.WechatSubLedgerOption();
            if (configuration != null) {
                try
                {
                    Config.Appid = configuration.GetSection("WechatSubLedger:Appid").Get<string>();
                    Config.Mchid = configuration.GetSection("WechatSubLedger:Mchid").Get<string>();
                    Config.Key = configuration.GetSection("WechatSubLedger:Key").Get<string>();
                    Config.Appsecret = configuration.GetSection("WechatSubLedger:Appsecret").Get<string>();
                    Config.SecretPath = configuration.GetSection("WechatSubLedger:SecretPath").Get<string>();
                    Config.SecretPassword = configuration.GetSection("WechatSubLedger:SecretPassword").Get<string>();
                    Config.Rate = configuration.GetSection("WechatSubLedger:Rate").Get<decimal>();
                    Config.MaxRate = configuration.GetSection("WechatSubLedger:MaxRate").Get<decimal>();
                }
                catch { }
            }
        }
    }
}
