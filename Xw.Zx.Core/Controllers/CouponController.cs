using AutoMapper;
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
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CouponController : BaseController
    {
        private readonly ILogger<CouponController> _logger;
        public CouponController(ILogger<CouponController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;

        }

        [HttpGet]
        public HbzsResult<IEnumerable<CouponRespone.CouponList>> GetCoupons([FromQuery] SieveModel sieveModel)
        {
            try
            {
                var db = from coupon in _context.Coupons
                         join couponreceive in _context.CouponReceives
                         on coupon.Id equals couponreceive.Couponid
                         where couponreceive.Memberid == Member.Id
                         select new CouponRespone.CouponList
                         {
                             CouponReceiveId = couponreceive.Id,
                             ProductId = coupon.ProductId,
                             Name = coupon.Name,
                             StartTime = coupon.StartTime,
                             EndTime = coupon.EndTime,
                             Money = coupon.Money,
                             CouponUseState= couponreceive.CouponUseState
                         };

                var details = _sieveProcessor
                    .Apply(sieveModel, db)
                    .ToArray();

                return new HbzsResult<IEnumerable<CouponRespone.CouponList>>(details);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<IEnumerable<CouponRespone.CouponList>>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

        [HttpGet]
        public HbzsResult<CouponRespone.CouponContent> GetCoupon([FromQuery] int couponReceiveId)
        {
            try
            {

                var couponReceive = _context.CouponReceives.First(c => c.Id == couponReceiveId);

                var coupon = _context.Coupons.First(c => c.Id == couponReceive.Couponid);

                var product = _context.Products.First(p => p.Id == coupon.ProductId);

                var result = new CouponRespone.CouponContent()
                {
                    CouponReceiveId = couponReceiveId,
                    Coupon = _mapper.Map<CouponRespone.Coupon>(coupon),
                    Porduct = _mapper.Map<CouponRespone.Porduct>(product),
                };

                return new HbzsResult<CouponRespone.CouponContent>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<CouponRespone.CouponContent>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

        [HttpGet]
        public HbzsResult<CouponRespone.CouponContent> GetCouponByProductId([FromQuery] int productId)
        {
            try
            {
                int couponReceiveId = (from coupon in _context.Coupons
                                        join couponreceive in _context.CouponReceives
                                        on coupon.Id equals couponreceive.Couponid
                                        where couponreceive.Memberid == Member.Id
                                            && coupon.ProductId == productId
                                            && couponreceive.CouponUseState == CouponUseState.未使用
                                        select couponreceive.Id)
                         .FirstOrDefault();

                if (couponReceiveId != 0)
                {
                    return GetCoupon((int)couponReceiveId);
                }

                return new HbzsResult<CouponRespone.CouponContent>(null);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<CouponRespone.CouponContent>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }
    }
}
