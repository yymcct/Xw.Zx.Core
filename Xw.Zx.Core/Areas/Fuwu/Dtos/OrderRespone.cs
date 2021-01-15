using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Fuwu.Dtos
{
    public class OrderRespone
    {
        public class Order
        {
            public int Id { get; set; }

            public string Timestamp { get; set; }

            public int MemberId { get; set; }

            public int ProductId { get; set; }

            public DateTime AddTime { get; set; }

            public OrderState OrderState { get; set; }

           

        }
    }
}
