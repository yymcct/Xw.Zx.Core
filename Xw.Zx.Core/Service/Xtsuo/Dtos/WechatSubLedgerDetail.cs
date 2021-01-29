using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service.Xtsuo.Dtos
{
    public class WechatSubLedgerDetail
    {
        public decimal TotalAmount { get; set; }

        public IEnumerable<WechatSubDetail> Details { get; set; }
    }
}
