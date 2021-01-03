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
using Sieve.Services;
using Microsoft.EntityFrameworkCore;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Manager
{
    [ApiController]
    [Route("manager/[controller]/[action]")]
    [Authorize(Policy = "Admins")]
    public class OrderController : ManagerBaseController
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger
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
        public HbzsManagerResult<OrderTotalMDto> GetOrders([FromQuery] SieveModel sieveModel)
        {
            try
            {
                var db = from order in _context.Orders
                         select new OrderMDto
                         {
                             Id = order.Id,
                             MemberId = order.MemberId,
                             MemberPhone = order.MemberPhone,
                             ProductId = order.ProductId,
                             ProducName = order.ProducName,
                             Amount = order.Amount,
                             AddTime = order.AddTime,
                             CustomerName = order.CustomerName,
                             CustomerPhone = order.CustomerPhone,
                             RealName = _context.Members.FirstOrDefault(m => m.Id == order.MemberId).RealName,
                             OrderPaymentType = order.OrderPaymentType,
                             OrderPaymentTypeName = order.OrderPaymentType.ToString(),
                             Timestamp = order.Timestamp,
                             Remark = order.Remark,
                             ProductAmount = order.ProductAmount,
                             OrderState = order.OrderState
                         };

                var result = new OrderTotalMDto()
                {
                    OrderMDtos = _sieveProcessor.Apply(sieveModel, db).ToList(),
                    QueryTotal = _sieveProcessor.Apply(sieveModel, db, null, true, true, false).Sum(o => o.Amount),
                    AllOrderTotal = db.Sum(o => o.Amount),
                    WithdrawDepositsTotal = _context.WithdrawDeposits.Where(w => w.WithdrawDepositState == WithdrawDepositState.提现成功).Sum(w => w.Amount),
                };
                result.Balance = result.AllOrderTotal - result.WithdrawDepositsTotal;

                var total = _sieveProcessor.Apply(sieveModel, db, null, true, true, false).Count();

                return new HbzsManagerResult<OrderTotalMDto>(result, total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult<OrderTotalMDto>(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }

        [HttpGet]
        public HbzsManagerResult<OrderMRespone.Info> GetInfo(DateTime startTime, DateTime endTime)
        {
            var info = new OrderMRespone.Info();

            //订单
            info.OrderWaitPay.Count = _context
                .Orders
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.OrderState == OrderState.待付款)
                .Count();
            info.OrderWaitPay.Amount = _context
                .Orders
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.OrderState == OrderState.待付款)
                .Sum(o => o.Amount);

            info.OrderSucess.Count = _context
                .Orders
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.OrderState == OrderState.已付款)
                .Count();
            info.OrderSucess.Amount = _context
                .Orders
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.OrderState == OrderState.已付款)
                .Sum(o => o.Amount);

            //分润
            info.IncomeWaitAudit.Count = _context
                .IncomeAccounts
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.IncomeAccountState == IncomeAccountState.待审核)
                .Count();
            info.IncomeWaitAudit.Amount = _context
                .IncomeAccounts
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.IncomeAccountState == IncomeAccountState.待审核)
                .Sum(o => o.Amount);

            info.IncomeSucess.Count = _context
                .IncomeAccounts
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.IncomeAccountState == IncomeAccountState.已发放)
                .Count();
            info.IncomeSucess.Amount = _context
                .IncomeAccounts
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.IncomeAccountState == IncomeAccountState.已发放)
                .Sum(o => o.Amount);

            info.IncomeFail.Count = _context
                .IncomeAccounts
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.IncomeAccountState == IncomeAccountState.已拒绝)
                .Count();
            info.IncomeFail.Amount = _context
                .IncomeAccounts
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.IncomeAccountState == IncomeAccountState.已拒绝)
                .Sum(o => o.Amount);

            //提现
            info.WithdrawApplyFor.Count = _context
                .WithdrawDeposits
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.WithdrawDepositState == WithdrawDepositState.申请提现)
                .Count();
            info.WithdrawApplyFor.Amount = _context
                .WithdrawDeposits
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.WithdrawDepositState == WithdrawDepositState.申请提现)
                .Sum(o => o.Amount);

            info.WithdrawTongjibuAudit.Count = _context
                .WithdrawDeposits
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.WithdrawDepositState == WithdrawDepositState.统计部审核)
                .Count();
            info.WithdrawTongjibuAudit.Amount = _context
                .WithdrawDeposits
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.WithdrawDepositState == WithdrawDepositState.统计部审核)
                .Sum(o => o.Amount);

            info.WithdrawCaiwubuAudit.Count = _context
                .WithdrawDeposits
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.WithdrawDepositState == WithdrawDepositState.统计部审核)
                .Count();
            info.WithdrawCaiwubuAudit.Amount = _context
                .WithdrawDeposits
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.WithdrawDepositState == WithdrawDepositState.统计部审核)
                .Sum(o => o.Amount);

            info.WithdrawSucess.Count = _context
                .WithdrawDeposits
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.WithdrawDepositState == WithdrawDepositState.提现成功)
                .Count();
            info.WithdrawSucess.Amount = _context
                .WithdrawDeposits
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.WithdrawDepositState == WithdrawDepositState.提现成功)
                .Sum(o => o.Amount);

            info.WithdrawFail.Count = _context
                .WithdrawDeposits
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.WithdrawDepositState == WithdrawDepositState.提现失败)
                .Count();
            info.WithdrawFail.Amount = _context
                .WithdrawDeposits
                .Where(o => startTime <= o.AddTime && o.AddTime <= endTime && o.WithdrawDepositState == WithdrawDepositState.提现失败)
                .Sum(o => o.Amount);

            return new HbzsManagerResult<OrderMRespone.Info>(info);
        }
    }
}