using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Manager
{
    public class OrderMDto
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        public int MemberId { get; set; }

        [Sieve(CanFilter = true)]
        public string MemberPhone { get; set; }

        public int ProductId { get; set; }

        public string ProducName { get; set; }

        public int ProductCount { get; set; }

        public decimal Amount { get; set; }

        public decimal ProductAmount { get; set; }

        [Sieve(CanFilter = true)]
        public DateTime AddTime { get; set; } = DateTime.Now;

        [Sieve(CanFilter = true)]
        public string RealName { get; set; }

        [Sieve(CanFilter = true)]
        public string CustomerName { get; set; }

        [Sieve(CanFilter = true)]
        public string CustomerPhone { get; set; }

        [Sieve(CanFilter = true)]
        public OrderPaymentType OrderPaymentType { get; set; }

        public string OrderPaymentTypeName { get; set; }
        [Sieve(CanFilter = true)]
        public string Timestamp { get; set; }

        [Sieve(CanFilter = true)]
        public string Remark { get; set; }

        [Sieve(CanFilter = true)]
        public OrderState OrderState { get; set; }
    }
}
