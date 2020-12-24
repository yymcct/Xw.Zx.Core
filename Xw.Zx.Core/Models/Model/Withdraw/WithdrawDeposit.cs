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
        审核通过 = 10,
        提现成功 = 20,
        提现失败 = 30
    }
    public class WithdrawDeposit : ModelBase
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        public string Timestamp { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmssffffff");

        public int MemberId { get; set; }

        /// <summary>
        /// 申请提现金额
        /// </summary>
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 提现手续费
        /// </summary>
        [Column(TypeName = "decimal(8, 2)")]
        public decimal WithdrawCharge { get; set; }

        /// <summary>
        /// 实际提现金额
        /// </summary>
        [Column(TypeName = "decimal(8, 2)")]
        public decimal RealityAmount { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime AddTime { get; set; } = DateTime.Now;

        public WithdrawDepositState WithdrawDepositState { get; set; } = WithdrawDepositState.申请提现;

        [Column(TypeName = "nvarchar(500)")]
        public string Remark { get; set; } = "";
    }
}
