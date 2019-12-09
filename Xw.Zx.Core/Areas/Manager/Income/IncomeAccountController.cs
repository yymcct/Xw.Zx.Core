


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Xw.Zx.Core.Models.Model;
using Sieve.Services;
using Microsoft.EntityFrameworkCore;

namespace Xw.Zx.Core.Areas.Manager
{

    [ApiController]
    [Route("manager/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class IncomeAccountController : ManagerBaseController
    {
        private readonly ILogger<IncomeAccountController> _logger;

        public IncomeAccountController(ILogger<IncomeAccountController> logger
            , XwZxContext context
            , IMapper mapper
            , ISieveProcessor sieveProcessor) : base(context, mapper, sieveProcessor)
        {
            _logger = logger;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HbzsManagerResult<List<IncomeAccountMDto>> GetIncomeAccounts([FromQuery]SieveModel sieveModel)
        {
            try
            {

                //var db = from member in _context.Members
                //         join income in _context.IncomeAccounts on member.Id equals income.MemberId into joinTmp
                //         from tmp in joinTmp.DefaultIfEmpty()
                //         join withdrawDeposit in _context.WithdrawDeposits on member.Id equals withdrawDeposit.MemberId into wjoinTmp
                //         from wtmp in wjoinTmp.DefaultIfEmpty()
                //         where member.MemberVipType != MemberVipType.普通
                //         group tmp by new
                //         {
                //             member.Id,
                //             member.RealName,
                //             member.Phone,
                //             member.MemberVipType,
                //             iAmount = tmp.Amount,
                //             wAmount = wtmp.Amount
                //         } into groupkeywords
                //         select new IncomeAccountMDto
                //         {
                //             MemberId = groupkeywords.Key.Id,

                //             MemberName = groupkeywords.Key.RealName,
                //             MemberPhone = groupkeywords.Key.Phone,
                //             MemberVipType = groupkeywords.Key.MemberVipType,
                //             MemberVipTypeName = groupkeywords.Key.MemberVipType.ToString(),
                //             ZhijieTotla = groupkeywords.Where(g => g.IncomeAccountType == IncomeAccountType.直接收益).Sum(g => g.Amount),

                //             JianjieTotla = groupkeywords.Where(g => g.IncomeAccountType == IncomeAccountType.间接收益).Sum(g => g.Amount),

                //             ChajiTotla = groupkeywords.Where(g => g.IncomeAccountType == IncomeAccountType.级差收益).Sum(g => g.Amount),

                //             //IncomeTotal = groupkeywords.Sum(g =>g.w),

                //             //WithdrawDepositTotal = groupkeywords.amo,

                //             //Balance = _context.IncomeAccounts
                //             //                            .Where(i => i.MemberId == member.Id)
                //             //                            .Sum(i => i.Amount) -
                //             //                            _context.WithdrawDeposits
                //             //                            .Where(w => w.WithdrawDepositState == WithdrawDepositState.通过 && w.MemberId == member.Id)
                //             //                            .Sum(w => w.Amount)
                //         };


                var db = from member in _context.Members.Where(m => m.MemberVipType != MemberVipType.普通)
                         select new IncomeAccountMDto
                         {
                             MemberId = member.Id,
                             MemberName = member.RealName,
                             MemberPhone = member.Phone,
                             MemberVipType = member.MemberVipType,
                             MemberVipTypeName = member.MemberVipType.ToString(),
                             ZhijieTotla = _context.IncomeAccounts
                                            .Where(i => i.MemberId == member.Id && i.IncomeAccountType == IncomeAccountType.直接收益)
                                            .Sum(i => new decimal?(i.Amount)),

                             JianjieTotla = _context.IncomeAccounts
                                            .Where(i => i.MemberId == member.Id && i.IncomeAccountType == IncomeAccountType.间接收益)
                                            .Sum(i => new decimal?(i.Amount)),

                             ChajiTotla = _context.IncomeAccounts
                                            .Where(i => i.MemberId == member.Id && i.IncomeAccountType == IncomeAccountType.级差收益)
                                            .Sum(i => new decimal?(i.Amount)),

                             IncomeTotal = _context.IncomeAccounts
                                            .Where(i => i.MemberId == member.Id)
                                            .Sum(i => new decimal?(i.Amount)),

                             WithdrawDepositTotal = _context.WithdrawDeposits
                                            .Where(w => w.WithdrawDepositState == WithdrawDepositState.通过 && w.MemberId == member.Id)
                                            .Sum(w => new decimal?(w.Amount)),
                         };

                var list = _sieveProcessor.Apply(sieveModel, db).ToList();
                list = list.Select(l =>
                {
                    if (l.ZhijieTotla == null) l.ZhijieTotla = 0;
                    if (l.JianjieTotla == null) l.JianjieTotla = 0;
                    if (l.ChajiTotla == null) l.ChajiTotla = 0;
                    if (l.IncomeTotal == null) l.IncomeTotal = 0;
                    if (l.WithdrawDepositTotal == null) l.WithdrawDepositTotal = 0;
                    l.Balance = l.IncomeTotal - l.WithdrawDepositTotal;

                    return l;
                }).ToList();
                var total = _sieveProcessor.Apply(sieveModel, db, null, true, true, false).Count();
                return new HbzsManagerResult<List<IncomeAccountMDto>>(list, total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult<List<IncomeAccountMDto>>(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }

    }
}