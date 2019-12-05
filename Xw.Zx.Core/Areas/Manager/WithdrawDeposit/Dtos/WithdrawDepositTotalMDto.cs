using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Areas.Manager
{
    public class WithdrawDepositTotalMDto
    {
        public WithdrawDepositTotalMDto()
        {
            WithdrawDepositMDtos = new List<WithdrawDepositMDto>();
        }
        public List<WithdrawDepositMDto> WithdrawDepositMDtos { get; set; }

        public decimal QueryTotal { get; set; }

        public decimal AllTotal { get; set; }

        public decimal OrderTotal { get; set; }

        public decimal Balance { get; set; }
    }
}

