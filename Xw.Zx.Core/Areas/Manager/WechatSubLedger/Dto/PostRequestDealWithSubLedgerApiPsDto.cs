using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Areas.Manager
{
    public class PostRequestDealWithSubLedgerApiPsDto
    {
        //public string transaction_id { get; set; }
        public string out_order_no { get; set; }

        public List<PostWechatSubLedgerListInfoDto> SubLedgerListInfo { get;set; }

    }

    public class WechatSubLedgerRequestReceivePsDto
    {
        public string type { get; set; }
        public string account { get; set; }
        public decimal amount { get; set; }
        public string description { get; set; }
    }
}
