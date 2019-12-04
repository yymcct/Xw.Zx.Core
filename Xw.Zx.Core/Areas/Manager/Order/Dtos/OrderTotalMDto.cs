using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Areas.Manager
{
    public class OrderTotalMDto
    {
        public OrderTotalMDto()
        {
            OrderMDtos = new List<OrderMDto>();
        }

        public List<OrderMDto> OrderMDtos { get; set; }

        public decimal PageTotal { get; set; }

        public decimal QueryTotal { get; set; }

        public decimal AllOrderTotal { get; set; }

        public decimal WithdrawDepositsTotal { get; set; }

        public decimal Balance { get; set; }
    }
}
