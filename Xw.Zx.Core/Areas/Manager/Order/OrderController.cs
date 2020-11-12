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
    [Authorize(Roles = "Admin")]
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
        public HbzsManagerResult<OrderTotalMDto> GetOrders([FromQuery]SieveModel sieveModel)
        {
            try
            {
                var db = from order in _context.Orders
                         where order.OrderState == OrderState.已付款
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
                             Timestamp = order.Timestamp
                         };

                var result = new OrderTotalMDto()
                {
                    OrderMDtos = _sieveProcessor.Apply(sieveModel, db).ToList(),
                    QueryTotal = _sieveProcessor.Apply(sieveModel, db, null, true, true, false).Sum(o => o.Amount),
                    AllOrderTotal = db.Sum(o => o.Amount),
                    WithdrawDepositsTotal = _context.WithdrawDeposits.Where(w => w.WithdrawDepositState == WithdrawDepositState.通过).Sum(w => w.Amount),
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
    }
}