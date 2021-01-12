using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public enum IncomeAccountType
    {
        直接收益 = 0,
        间接收益 = 1,
        级差收益 = 2
    }

    public enum IncomeAccountState
    {
        待审核 = 0,
        已发放 = 10,
        提现中 = 14,
        已结算 = 17,
        已拒绝 = 20
    }

    public class IncomeAccount : ModelBase
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        public int MemberId { get; set; }

        public int WithdrawDepositId { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 产生收益的单据ID
        /// </summary>
        public int SourceOrderId { get; set; }

        /// <summary>
        /// 产品收益的单据的付款人ID
        /// </summary>
        public int SourceOrderMemberId { get; set; }

        /// <summary>
        /// 付款人的邀请人ID, 如果是直接收益则为0
        /// </summary>
        public int SourceOrderMemberInviteId { get; set; }

        public IncomeAccountType IncomeAccountType { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public IncomeAccountState IncomeAccountState { get; set; } = IncomeAccountState.待审核;


        public int AuditMemberId { get; set; } = 0;

        public DateTime Auditime { get; set; }

        public string Remark { get; set; }

        public DateTime AddTime { get; set; } = DateTime.Now;
    }
}
