using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public enum WithdrawDepositState
    {
        申请提现 = 0,
        通过 = 1,
        拒绝 = 2,
        失败=3
    }
    public class WithdrawDeposit : ModelBase
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        public string Timestamp { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmssffffff");

        public int MemberId { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Amount { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime AddTime { get; set; } = DateTime.Now;

        public WithdrawDepositState WithdrawDepositState { get; set; } = WithdrawDepositState.申请提现;

        public string Remark { get; set; } = "";
    }
}
