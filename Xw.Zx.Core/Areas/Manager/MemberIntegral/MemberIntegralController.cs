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

namespace Xw.Zx.Core.Areas.Manager
{
    [ApiController]
    [Route("manager/[controller]")]
    [Authorize(Policy = "Admins")]
    public class MemberIntegralController : ManagerBaseController
    {
        private readonly ILogger<MemberIntegralController> _logger;
        private readonly IMemberIntegralService _MemberIntegralService;
        public MemberIntegralController(ILogger<MemberIntegralController> logger
           , XwZxContext context
           , IMapper mapper
           , ISieveProcessor sieveProcessor
            , IMemberIntegralService memberIntegralService) : base(context, mapper, sieveProcessor)
        {
            _logger = logger;
            _MemberIntegralService = memberIntegralService;
        }


        /// <summary>
        /// 充值积分
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public HbzsManagerResult<string> Add([FromQuery] int memberId, AddMemberIntegralDtos dto)
        {
            //TODO: 检查用户, 且积分不能为负值
            try
            {      
                _MemberIntegralService.AddMemberIntegral(new MemberIntegralRecord()
                {
                    MemberId = memberId,
                    Integral = dto.Integral,
                    TypeId = MemberIntegral.IntegralType.FromAdd,
                    Remark = $"操作员{Member.Id}手工充值,{dto.Remark}"
                });

                var integral = _MemberIntegralService.GetMemberIntegral(memberId);
                return new HbzsManagerResult<string>($"充值成功, 当前可用积分:{integral.AvailableIntegrals}");
            }
            catch (Exception ex)
            {
                return new HbzsManagerResult<string>(HbzsManagerResultCode.Invalid_Error, "充值异常:" + ex.Message);
            }

        }
    }
}
