using Microsoft.EntityFrameworkCore;
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

        public MemberIntegral CouponToMemberIntegral(int couponReceiveId)
        {

            var couponReceive = _context
                 .CouponReceives
                 .Include(c => c.Coupon)
                 .FirstOrDefault(c => c.Id == couponReceiveId && c.CouponUseState == CouponUseState.未使用);

            if (couponReceive == null)
            {
                throw new ZzzException("此优惠券已使用或不存在！");
            }



            _memberIntegralService.AddMemberIntegral(new MemberIntegralRecord()
            {
                MemberId = couponReceive.Memberid,
                Integral = _memberIntegralService.AmountToIntegral(couponReceive.Coupon.Money),
                TypeId = MemberIntegral.IntegralType.FromCoupon,
                Remark = $"使用优惠券ID:{couponReceive.Id} 兑换"
            });

            couponReceive.CouponUseState = CouponUseState.兑换为积分;
            _context.SaveChanges();

            return _memberIntegralService.GetMemberIntegral(couponReceive.Memberid);


        }
    }
}
