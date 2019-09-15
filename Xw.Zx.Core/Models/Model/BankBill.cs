using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    /// <summary>
    /// 银行卡账单
    /// </summary>
    public class BankBill
    {
        public int Id { get; set; }
        public int BankCardId { get; set; }
        public DateTime CycleStart { get; set; }
        public DateTime CycleStop { get; set; }
        /// <summary>
        /// 额度
        /// </summary>
        public decimal Limit { get; set; }
        /// <summary>
        /// 本期还款总额
        /// </summary>
        public decimal NewBalance { get; set; }

        /// <summary>
        /// 到期还款日
        /// </summary>
        public DateTime PaymentDueData { get; set; }

        /// <summary>
        /// 滞纳金
        /// </summary>
        public decimal OverdueFine { get; set; } = 0;
    }
}
