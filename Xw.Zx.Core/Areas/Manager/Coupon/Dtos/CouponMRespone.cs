using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Manager.Coupon
{
    public class CouponMRespone
    {
        public class CouponDrop
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }


        public class CouponItem
        {
            public int CouponReceiveId { get; set; }

            public int? ProductId { get; set; }

            public string ProductName { get; set; }

            public string Name { get; set; }

            public DateTime? StartTime { get; set; }

            public DateTime? EndTime { get; set; }

            public decimal Money { get; set; }

            public CouponUseState CouponUseState { get; set; }

            public string CouponUseStateName { get; set; }

            public int? Orderid { get; set; }

            public DateTime? UseTime { get; set; }

            public string RealName { get; set; }

            public string Phone { get; set; }

            public int? MemberId { get; set; }

            public MemberVipType MemberVipType { get; set; }

            public string MemberVipTypeName { get; set; }

            /// <summary>
            /// 领取时间
            /// </summary>
            public DateTime CreateTime { get; set; }
        }
    }
}
