using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Areas.Manager.Coupon.Dtos;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service;

namespace Xw.Zx.Core.Areas.Manager.Coupon
{
    [ApiController]
    [Route("manager/[controller]/[action]")]
    [Authorize(Policy = "Admins")]
    public class CouponController : ManagerBaseController
    {
        private readonly ILogger<CouponController> _logger;
        private readonly ICouponService _CouponService;
        public CouponController(ILogger<CouponController> logger
           , XwZxContext context
           , IMapper mapper
           , ISieveProcessor sieveProcessor
            , ICouponService couponService) : base(context, mapper, sieveProcessor)
        {
            _logger = logger;
            _CouponService = couponService;
        }

        [HttpGet]
        public HbzsManagerResult<IEnumerable<CouponMRespone.CouponDrop>> GetCouponList()
        {
            var result = _context.Coupons
                 .ProjectTo<CouponMRespone.CouponDrop>(_mapper.ConfigurationProvider)
                 .ToArray();

            return new HbzsManagerResult<IEnumerable<CouponMRespone.CouponDrop>>(result);
        }

        /// <summary>
        /// 发送优惠券
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public HbzsManagerResult GiveCoupon(CouponMRequest.GiveCoupon dto)
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



        /// <summary>
        /// 根据用户ID查
        /// </summary>
        /// <param name="sieveModel"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsManagerResult<IEnumerable<CouponMRespone.CouponItem>> GetCoupon([FromQuery] SieveModel sieveModel)
        {
            var couponReceiveDb = _context.CouponReceives;
            var couponReceives = _sieveProcessor.Apply(sieveModel, couponReceiveDb).ToList();

            var couponIds = couponReceives.Select(c => c.Couponid).ToArray();
            var coups = _context.Coupons.Where(c => couponIds.Contains(c.Id)).ToArray();

            var couponlogIds = couponReceives.Select(c => c.Id).ToArray();
            var couponlogs = _context.CouponUseLogs.Where(c => couponlogIds.Contains(c.CouponReceiveid)).ToArray();

            var memberIds = couponReceives.Select(c => c.Memberid).ToArray();
            var members = _context.Members.Where(m => memberIds.Contains(m.Id)).ToArray();

            var productIds = coups.Select(c => c.ProductId).ToArray();
            var products = _context.Products.Where(p => productIds.Contains(p.Id)).ToArray();

            var items = new List<CouponMRespone.CouponItem>();
            foreach (var r in couponReceives)
            {
                var item = new CouponMRespone.CouponItem();

                var coup = coups.FirstOrDefault(c => c.Id == r.Couponid);
                var couponlog = couponlogs.FirstOrDefault(c => c.CouponReceiveid == r.Id);
                var member = members.FirstOrDefault(m => m.Id == r.Memberid);
                var product = products.FirstOrDefault(p => p.Id == coup.ProductId);

                item.CouponReceiveId = r.Id;
                item.ProductId = product?.Id;
                item.ProductName = product?.Name;
                item.Name = coup.Name;
                item.StartTime = coup.StartTime;
                item.EndTime = coup.EndTime;
                item.Money = coup.Money;
                item.CouponUseState = r.CouponUseState;
                item.CouponUseStateName = r.CouponUseState.ToString();
                item.Orderid = couponlog?.Orderid;
                item.UseTime = couponlog?.CreateTime;
                item.RealName = member?.RealName;
                item.Phone = member?.Phone;
                item.MemberVipType = member.MemberVipType;
                item.MemberVipTypeName = member.MemberVipType.ToString();
                item.CreateTime = r.CreateTime;
                item.MemberId = member?.Id;
                items.Add(item);
            }
            var total = _sieveProcessor.Apply(sieveModel, couponReceiveDb, null, true, true, false).Count();
            return new HbzsManagerResult<IEnumerable<CouponMRespone.CouponItem>>(items, total);
        }

        /// <summary>
        /// 优惠券转积分
        /// </summary>
        /// <param name="couponReceiveId"></param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize(Roles = nameof(MemberRole.Admin_Tongjibu))]
        public HbzsManagerResult<MemberIntegral> CouponToMemberIntegral(int couponReceiveId)
        {
            return new HbzsManagerResult<MemberIntegral>(_CouponService.CouponToMemberIntegral(couponReceiveId));
        }
    }
}
