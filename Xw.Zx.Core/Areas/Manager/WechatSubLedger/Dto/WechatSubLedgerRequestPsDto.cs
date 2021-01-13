using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Areas.Manager
{



    public class WxDataSubLedgerReceivePsDto
    {
        public string type { get; set; }
        public string account { get; set; }
        public int amount { get; set; }
        public string description { get; set; }
    }

    public enum WechatSubLedgerRequestType
    {
        PERSONAL_OPENID,//个人
        MERCHANT_ID,//商户        
    }
}
