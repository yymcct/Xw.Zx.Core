using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Models.Dto
{
    public class OrderListDto
    {
        public OrderListDto()
        {
            ProductDto = new ProductDto();
        }
        public int Id { get; set; }

        public string Timestamp { get; set; }

        public int MemberId { get; set; }

        public string MemberPhone { get; set; }

        public int ProductId { get; set; }

        public string ProducName { get; set; }

        public int ProductCount { get; set; }

        public decimal Amount { get; set; }

        public DateTime AddTime { get; set; }

        public OrderState OrderState { get; set; }

        public OrderPaymentType OrderPaymentType { get; set; }

        public ProductDto ProductDto { get; set; }

    }
    public class OrderDto
    {
        public int Id { get; set; }

        public string Timestamp { get; set; }

        public int MemberId { get; set; }

        public string MemberPhone { get; set; }

        public int ProductId { get; set; }

        public string ProducName { get; set; }

        public decimal Amount { get; set; }

        public DateTime AddTime { get; set; }

        public OrderState OrderState { get; set; }

        public OrderPaymentType OrderPaymentType { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public int ProductCount { get; set; }

        public string Remark { get; set; }
    }

    public class PostOrderDto
    {
        public int ProductId { get; set; }

        public int ProductCount { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public string Remark { get; set; }

        public OrderPaymentType OrderPaymentType { get; set; } = OrderPaymentType.支付宝;
    }
}
