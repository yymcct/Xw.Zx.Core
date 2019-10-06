using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alipay.AopSdk.AspnetCore;
using Alipay.AopSdk.Core.Domain;
using Alipay.AopSdk.Core.Request;
using Alipay.AopSdk.Core.Response;
using AutoMapper;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sieve.Services;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WithdrawDepositController : BaseController
    {
        private readonly ILogger<WithdrawDepositController> _logger;
        private readonly AlipayService _alipayService;
        private readonly IUpDateVip1Service _upDateVip1Service;
        public WithdrawDepositController(ILogger<WithdrawDepositController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor
            , AlipayService alipayService
            , IUpDateVip1Service upDateVip1Service) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
            _alipayService = alipayService;
            _upDateVip1Service = upDateVip1Service;
        }

        /// <summary>
        /// 提交提现申请, 生成申请单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public HbzsResult PostWithdrawDeposit(PostWithdrawDepositDto postWithdrawDepositDto)
        {
            try
            {
                var withdrawDeposit = new WithdrawDeposit()
                {
                    MemberId = Member.Id,
                    Amount = postWithdrawDepositDto.Amount,
                };
                _context.WithdrawDeposits.Add(withdrawDeposit);
                _context.SaveChanges();
                return new HbzsResult(HbzsResultCode.Sucess);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsResult(HbzsResultCode.Invalid_Error, ex.Message);
            }

        }





    }
}