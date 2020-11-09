using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Service;

namespace Xw.Zx.Core.Config.AutoMapper
{
    public class BiqilinProfile: Profile
    {
        public BiqilinProfile()
        {
            CreateMap<BiqilinRespone.JsapiPay, JsapiPayResponeDto.JsapiPay>();
        }
    }
}
