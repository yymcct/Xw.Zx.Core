using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class IncomeController : BaseController
    {
        private readonly ILogger<IncomeController> _logger;
        public IncomeController(ILogger<IncomeController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;

        }

        /// <summary>
        /// 获取自己名下收益明细, 可检索, 排序, 分页      
        /// </summary>
        /// <param name="sieveModel">可空</param>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult<List<IncomeDetailDto>> GetDetails([FromQuery]SieveModel sieveModel)
        {
            try
            {
                var db = _context.IncomeAccounts
                    .AsNoTracking()
                    .Where(b => b.MemberId == Member.Id && b.IncomeAccountState == IncomeAccountState.已发放);

                var details = _sieveProcessor
                    .Apply(sieveModel, db)
                    .ToList();

                var res = _mapper.Map<List<IncomeDetailDto>>(details);

                res = res.Select(r =>
                {
                    r.IncomeAccountTypeName = r.IncomeAccountType.ToString();
                    return r;
                }).ToList();

                return new HbzsResult<List<IncomeDetailDto>>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<List<IncomeDetailDto>>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }
        /// <summary>
        /// 获取某个人的利润记录      
        /// </summary>
        /// <param name="sieveModel">可空</param>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult<List<IncomeDetailDto>> GetPeronDetails(int memberId)
        {
            try
            {
                var db = _context.IncomeAccounts
                    .AsNoTracking()
                    .Where(b => b.MemberId == memberId && b.IncomeAccountState == IncomeAccountState.已发放);

                var details = _sieveProcessor
                    .Apply(new SieveModel(),db)
                    .ToList();

                var res = _mapper.Map<List<IncomeDetailDto>>(details);

                res = res.Select(r =>
                {
                    r.IncomeAccountTypeName = r.IncomeAccountType.ToString();
                    return r;
                })
                    .OrderByDescending(item=>item.AddTime)
                    .ToList();

                return new HbzsResult<List<IncomeDetailDto>>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<List<IncomeDetailDto>>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// 获取自己名下收益概括
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult<IncomInfo> GetIncomeInfo()
        {
            try
            {
                var IncomTotal = _context.IncomeAccounts
                    .Where(b => b.MemberId == Member.Id && b.IncomeAccountState == IncomeAccountState.已发放)
                    .Sum(b => b.Amount);

                var WithdrawDeposit = _context.WithdrawDeposits
                        .Where(b => b.MemberId == Member.Id
                                && b.WithdrawDepositState == WithdrawDepositState.提现成功)
                        .Sum(b => b.Amount);

                var res = new IncomInfo()
                {
                    IncomTotal = IncomTotal,

                    CanGet = IncomTotal - WithdrawDeposit,
                };

                return new HbzsResult<IncomInfo>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<IncomInfo>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }
    
    }
}
