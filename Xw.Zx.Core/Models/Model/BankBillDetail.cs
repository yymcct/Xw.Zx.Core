using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public class BankBillDetail
    {
        public int Id { get; set; }

        public string BankCardId { get; set; }

        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime TreadTime { get; set; }

        /// <summary>
        /// 商户名
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// 交易额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 币别
        /// </summary>
        public string Unit { get; set; }

    }
}
