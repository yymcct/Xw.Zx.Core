using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Service;

namespace Xw.Zx.Core.Config.AutoMapper
{
    public class BiqilinProfile : Profile
    {
        public BiqilinProfile()
        {
            CreateMap<BiqilinRespone.JsapiPay, JsapiPayResponeDto.JsapiPay>()
                .ForMember(dest => dest.AppId, opt => opt.MapFrom(src => src.jsAppId))
                .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(src => src.jsTimeStamp))
                .ForMember(dest => dest.SignType, opt => opt.MapFrom(src => src.jsSignType))
                .ForMember(dest => dest.Package, opt => opt.MapFrom(src => src.jsPackages))
                .ForMember(dest => dest.NonceStr, opt => opt.MapFrom(src => src.jsNonceStr))
                .ForMember(dest => dest.PaySign, opt => opt.MapFrom(src => src.jsPaySign));
        }
    }
}
