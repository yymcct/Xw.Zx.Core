


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
        public HbzsManagerResult<List<IncomeAccountMDto>> GetIncomeAccounts([FromQuery] SieveModel sieveModel)
        {
            try
            {
                var db = from member in _context.Members.Where(m => m.MemberVipType != MemberVipType.客户)
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
                                            .Where(w => w.WithdrawDepositState == WithdrawDepositState.提现成功 && w.MemberId == member.Id)
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


        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="sieveModel"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsManagerResult<IEnumerable<IncomeAccountMRespone.Income>> GetIncomes([FromQuery] SieveModel sieveModel)
        {
            var db = _context.IncomeAccounts;
            var incomes = _sieveProcessor.Apply(sieveModel, db).ToList();

            var memberIds = incomes.Select(i => i.MemberId).ToArray();
            var members = _context.Members.Where(m => memberIds.Contains(m.Id)).ToArray();

            var orderIds = incomes.Select(i => i.SourceOrderId).ToArray();
            var orders = _context.Orders.Where(o => orderIds.Contains(o.Id));

            var items = new List<IncomeAccountMRespone.Income>();
            foreach (var r in incomes)
            {
                var item = new IncomeAccountMRespone.Income();
                item.Id = r.Id;
                item.MemberId = r.MemberId;
                var member = members.First(m => m.Id == r.MemberId);
                item.MemberName = member.RealName;
                item.MemberPhone = member.Phone;
                item.Amount = r.Amount;
                var order = orders.First(o => o.Id == r.SourceOrderId);
                item.SourceOrderId = order.Id;
                item.SourceOrderTimestamp = order.Timestamp;
                item.SourceOrderMemberId = order.MemberId;
                item.SourceOrderMemberPhone = order.MemberPhone;
                item.SourceOrderProducName = order.ProducName;
                item.SourceOrderProductAmount = order.Amount;
                item.SourceOrderAddTime = order.AddTime;
                item.SourceOrderOrderPaymentType = order.OrderPaymentType;
                item.SourceOrderOrderPaymentTypeName = order.OrderPaymentType.ToString();
                item.IncomeAccountType = r.IncomeAccountType;
                item.IncomeAccountTypeName = r.IncomeAccountType.ToString();
                item.Remark = r.Remark;
                item.AddTime = r.AddTime;

                items.Add(item);
            }
            var total = _sieveProcessor.Apply(sieveModel, db, null, true, true, false).Count();
            return new HbzsManagerResult<IEnumerable<IncomeAccountMRespone.Income>>(items, total);
        }
    }
}