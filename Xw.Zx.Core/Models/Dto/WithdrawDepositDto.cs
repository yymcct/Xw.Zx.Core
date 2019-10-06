using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Models.Dto
{
    public class WithdrawDepositDto
    {
    }

    public class PostWithdrawDepositDto
    {
        public decimal Amount { get; set; }
    }

    public class GetWithdrawDepositDetailsDto
    {
        public string Timestamp { get; set; }

        public decimal Amount { get; set; }

        public DateTime AddTime { get; set; }

        public WithdrawDepositState WithdrawDepositState { get; set; }
    }

    public class AuditWithdrawDepositdetailDto
    {
        public string Timestamp { get; set; }
        public bool IsPass { get; set; }
    }
}
