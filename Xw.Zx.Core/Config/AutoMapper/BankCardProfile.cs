using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Controllers;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Config.AutoMapper
{
    public class BankCardProfile : Profile
    {
        public BankCardProfile()
        {
            CreateMap<PostBankDto, BankCard>().ReverseMap();
            
            CreateMap<BankCard, BankInfoDto>();
            
            CreateMap<Member, MyTeamUserDto>();

            CreateMap<Member, MemberDto>();

            CreateMap<BankBillDetail, BankBillDetailDto>();
            
            CreateMap<UpdateVipAuthCode, UpdaeVipAuthCodeDto>();

            CreateMap<IncomeAccount, IncomeDetailDto>();

            CreateMap<PostUserDto, Member>();

            CreateMap<ApplyForZx, ApplyDto>();

            CreateMap<WithdrawDeposit, GetWithdrawDepositDetailsDto>()
                .ForMember(dest => dest.WithdrawDepositStateName, opt=>opt.MapFrom(src=>src.WithdrawDepositState.ToString()));
        }
    }
}
