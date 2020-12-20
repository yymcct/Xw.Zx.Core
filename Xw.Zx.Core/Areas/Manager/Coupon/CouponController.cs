using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Areas.Manager.Coupon.Dtos;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Manager.Coupon
{
    [Route("api/[controller]")]
    [ApiController]
    [Route("manager/[controller]/[action]")]
    public class CouponController : ManagerBaseController
    {
        private readonly ILogger<CouponController> _logger;

        public CouponController(ILogger<CouponController> logger
           , XwZxContext context
           , IMapper mapper
           , ISieveProcessor sieveProcessor) : base(context, mapper, sieveProcessor)
        {
            _logger = logger;
        }

        public HbzsManagerResult<IEnumerable<CouponRespone.CouponList>> GetCouponList()
        {
            var result = _context.Coupons
                 .ProjectTo<CouponRespone.CouponList>(_mapper.ConfigurationProvider)
                 .ToArray();

            return new HbzsManagerResult<IEnumerable<CouponRespone.CouponList>>(result);
        }


        public HbzsManagerResult GiveCoupon(CouponRequest.GiveCoupon dto)
        {
            if (!_context.Members.Any(m => m.MemberVipType == MemberVipType.运营中心
                                        && m.Id == dto.Memberid))
            {
                return new HbzsManagerResult(HbzsManagerResultCode.Remote_Service_Error, "该用户不存在或不是运营中心");
            }

            if (!_context.Coupons.Any(c => c.CurCount - dto.Count > 0
                            && c.Id == dto.Couponid))
            {
                return new HbzsManagerResult(HbzsManagerResultCode.Remote_Service_Error, "优惠券不存在或数量不足");
            }

            for (var i = 0; i < dto.Count; i++)
            {
                var couponeReceive = new CouponReceive()
                {
                    Couponid = dto.Couponid,
                    Memberid = dto.Memberid,
                    Code = Guid.NewGuid().ToString(),
                    CouponUseState = CouponUseState.未使用
                };

                _context.CouponReceives.Add(couponeReceive);
            }

            var coupon = _context.Coupons.First(c => c.Id == dto.Couponid);
            coupon.CurCount -= dto.Count;

            _context.SaveChanges();

            return new HbzsManagerResult(HbzsManagerResultCode.Sucess, "");
        }
    }
}
