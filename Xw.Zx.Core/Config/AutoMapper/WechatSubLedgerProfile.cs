using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Areas.Manager;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Config.AutoMapper
{
    public class WechatSubLedgerProfile: Profile
    {
        public WechatSubLedgerProfile()
        {
            CreateMap<WechatOrders, WechatOrdersDetailsDto>().ReverseMap();
        }
    }
}
