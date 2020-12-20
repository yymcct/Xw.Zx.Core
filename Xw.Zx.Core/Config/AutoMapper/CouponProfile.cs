using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Xw.Zx.Core.Areas.Manager.Coupon;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Config.AutoMapper
{
    public class CouponProfile : Profile
    {
        public CouponProfile()
        {
            CreateMap<Coupon, CouponMRespone.CouponList>();

            CreateMap<Coupon, CouponRespone.Coupon>();

            CreateMap<Product, CouponRespone.Porduct>();

        }
    }
}
