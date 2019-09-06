using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Config.AutoMapper
{
    public class BankCardProfile : Profile
    {
        public BankCardProfile()
        {
            CreateMap<PostBankDto, BankCard>().ReverseMap();
        }
    }
}
