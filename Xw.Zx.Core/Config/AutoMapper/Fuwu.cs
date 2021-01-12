using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Areas.Fuwu.Dtos;
using Xw.Zx.Core.Areas.Manager;
using Xw.Zx.Core.Controllers;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Config.AutoMapper
{
    public class Fuwu : Profile
    {
        public Fuwu()
        {                                         
            CreateMap<Member, MemberRespone.Member>().ForMember(dest => dest.MemberVipTypeName, opt => opt.MapFrom(src => src.MemberVipType.ToString()));        
        }
    }
}
