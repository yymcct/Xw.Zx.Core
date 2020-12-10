using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public enum OrderState
    {
        待付款 = 0,
        已付款 = 1,
        已失效 = 2
    }

    public enum OrderPaymentType
    {
        支付宝 = 0,
        微信 = 1,
        线下 = 2,
        碧麒麟=3
    }

    /// <summary>
    /// 订单表
    /// </summary>
    public class Order : ModelBase
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        public string Timestamp { get; set; }

        public int MemberId { get; set; }

        public string MemberPhone { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int ProductId { get; set; }

        public string ProducName { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string CustomerName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string CustomerPhone { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Remark { get; set; }

        public DateTime AddTime { get; set; } = DateTime.Now;

        [Sieve(CanFilter = true, CanSort = true)]
        public OrderState OrderState { get; set; } = OrderState.待付款;

        public OrderPaymentType OrderPaymentType { get; set; } = OrderPaymentType.支付宝;

    }

}
