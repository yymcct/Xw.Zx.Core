using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Helper;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public class CouponService : ICouponService
    {
        private readonly XwZxContext _context;

        private readonly IMemberIntegralService _memberIntegralService;

        public CouponService(XwZxContext xwZxContext, IMemberIntegralService memberIntegralService)
        {
            _context = xwZxContext;
            _memberIntegralService = memberIntegralService;
        }

        public MemberIntegral CouponToMemberIntegral(int couponId)
        {
            var couponReceive = _context.CouponReceives.FirstOrDefault(c => c.Id == couponId && c.CouponUseState == CouponUseState.未使用);

            if (couponReceive == null)
            {
                throw new ZzzException("此优惠券已使用或不存在！");
            }

            _memberIntegralService.AddMemberIntegral(new MemberIntegralRecord()
            {
                MemberId = couponReceive.Memberid,
                Integral = Convert.ToInt32(couponReceive.Coupon.Money * 100m),
                TypeId = MemberIntegral.IntegralType.FromCoupon,
                Remark = $"优惠券ID:{couponReceive.Id} 转积分"
            });

            couponReceive.IsDelete = true;
            _context.SaveChanges();

            return _memberIntegralService.GetMemberIntegral(couponReceive.Memberid);
        }
    }
}
