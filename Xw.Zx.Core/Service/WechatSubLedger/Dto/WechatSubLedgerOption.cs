using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Service
{
    public class WechatSubLedgerOption
    {
        public string Appid { get; set; }
        public string Mchid { get; set; }
        public string Key { get; set; }
        public string Appsecret { get; set; }
        public string SecretPath { get; set; }
        public string SecretPassword { get; set; }
        public string Notify_url { get; set; }
        public decimal Rate { get; set; }
        public decimal MaxRate { get; set; }
    }
}
