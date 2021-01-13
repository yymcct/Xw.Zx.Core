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
            CreateMap<Coupon, CouponMRespone.CouponDrop>();

            CreateMap<Coupon, CouponRespone.Coupon>();

            CreateMap<Product, CouponRespone.Porduct>();

            CreateMap<Coupon, CouponMRespone.CouponItem>();

            CreateMap<CouponReceive, CouponMRespone.CouponItem>()
                .ForMember(dest => dest.CouponReceiveId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CouponUseStateName, opt => opt.MapFrom(src => src.CouponUseState.ToString()));

            CreateMap<CouponUseLog, CouponMRespone.CouponItem>()
                .ForMember(dest => dest.UseTime, opt => opt.MapFrom(src => src.CreateTime));

            CreateMap<Member, CouponMRespone.CouponItem>()
                .ForMember(dest => dest.MemberVipTypeName, opt => opt.MapFrom(src => src.MemberVipType.ToString()));

            CreateMap<Product, CouponMRespone.CouponItem>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
