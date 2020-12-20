using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Areas.Manager.Coupon.Dtos
{
    public class CouponRequest
    {
        public class GiveCoupon
        {
            public int Couponid { get; set; }

            public int Memberid { get; set; }

            public int Count { get; set; }
        }
    }
}
