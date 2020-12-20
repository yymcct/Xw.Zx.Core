using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Dto
{
    public class CouponRespone
    {
        public class Porduct
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class CouponList
        {
            public int CouponReceiveId { get; set; }
            public string Name { get; set; }

            public DateTime StartTime { get; set; }

            public DateTime EndTime { get; set; }

            public decimal Money { get; set; }
        }

        public class Coupon
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public DateTime StartTime { get; set; }

            public DateTime EndTime { get; set; }

            public decimal Money { get; set; }
        }

        public class CouponContent
        {
            public int CouponReceiveId { get; set; }

            public Coupon Coupon { get; set; }

            public Porduct Porduct { get; set; }
        }
    }
}
