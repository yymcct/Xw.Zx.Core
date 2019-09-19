using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Dto
{
    public class AliPayDto
    {
    }

    public class AliPayOrderDto
    {
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }

        public string AlipayTradeAppPayResponse { get; set; }
    }
}
