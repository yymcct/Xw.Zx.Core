using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Models.Dto
{
    public class OrderListDto { 
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
    }
}
