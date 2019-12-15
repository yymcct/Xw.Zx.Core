


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
using Xw.Zx.Core.Utility;
using Alipay.AopSdk.Core.Request;
using Newtonsoft.Json;
using Alipay.AopSdk.AspnetCore;
using Alipay.AopSdk.Core.Response;

namespace Xw.Zx.Core.Areas.Manager
{

    /// <summary>
    /// TODO
    /// </summary>
    [ApiController]
    [Route("manager/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class WithdrawDepositController : ManagerBaseController
    {
        private readonly ILogger<WithdrawDepositController> _logger;
        private readonly AlipayService _alipayService;
        public WithdrawDepositController(ILogger<WithdrawDepositController> logger
            , XwZxContext context
            , IMapper mapper
            , AlipayService alipayService
            , ISieveProcessor sieveProcessor) : base(context, mapper, sieveProcessor)
        {
            _logger = logger;
            _alipayService = alipayService;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="sieveModel"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsManagerResult<WithdrawDepositTotalMDto> GetWithdrawDeposits([FromQuery]SieveModel sieveModel)
        {
            try
            {
                var db = from withdrawdeposit in _context.WithdrawDeposits
                         join member in _context.Members on withdrawdeposit.MemberId equals member.Id
                         select new WithdrawDepositMDto
                         {
                             Id = withdrawdeposit.Id,

                             MemberId = withdrawdeposit.MemberId,
                             Amount = withdrawdeposit.Amount,
                             AddTime = withdrawdeposit.AddTime,
                             WithdrawDepositState = withdrawdeposit.WithdrawDepositState,
                             Remark = withdrawdeposit.Remark,
                             RealName = member.RealName,
                             Phone = member.Phone,
                             AliPayAccount = member.AliPayAccount,
                             WithdrawDepositStateName = withdrawdeposit.WithdrawDepositState.ToString(),
                         };

                var list = _sieveProcessor.Apply(sieveModel, db).ToList();


                var result = new WithdrawDepositTotalMDto()
                {
                    WithdrawDepositMDtos = _sieveProcessor.Apply(sieveModel, db).ToList(),
                    QueryTotal = _sieveProcessor.Apply(sieveModel, db.Where(w => w.WithdrawDepositState == WithdrawDepositState.通过), null, true, true, false).Sum(o => o.Amount),
                    AllTotal = db.Where(w => w.WithdrawDepositState == WithdrawDepositState.通过).Sum(o => o.Amount),
                    OrderTotal = _context.Orders.Where(o => o.OrderState == OrderState.已付款).Sum(w => w.Amount),
                };
                result.Balance = result.OrderTotal - result.AllTotal;

                var total = _sieveProcessor.Apply(sieveModel, db, null, true, true, false).Count();
                return new HbzsManagerResult<WithdrawDepositTotalMDto>(result, total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult<WithdrawDepositTotalMDto>(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// 通过或拒绝
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public HbzsManagerResult AuditWithdrawDepositdetail([FromBody]PostAuditWithdrawDepositdetailDto dto)
        {
            try
            {

                var detail = _context.WithdrawDeposits.First(w => w.Timestamp == dto.Timestamp);
                var detailMemnber = _context.Members.First(m => m.Id == detail.MemberId);

                if (!AppsettingsUtility.CanCreateUpdateVipCodePhone.Any(p => p == Member.Phone))
                {
                    return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, "账户无权限!");
                }

                if (detail.WithdrawDepositState != WithdrawDepositState.申请中)
                {
                    return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, "单据状态异常,无法处理!");
                }

                if (detailMemnber.Disabled == true)
                {
                    return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, "申请人账户异常,无法处理!");
                }

                var IncomTotal = _context.IncomeAccounts
                    .Where(b => b.MemberId == detail.MemberId)
                    .Sum(b => b.Amount);

                var WithdrawDeposit = _context.WithdrawDeposits
                        .Where(b => b.MemberId == detail.MemberId
                                && b.WithdrawDepositState == WithdrawDepositState.通过)
                        .Sum(b => b.Amount);

                var canGet = IncomTotal - WithdrawDeposit;
                if (detail.Amount < 2.09m || detail.Amount > canGet)
                {
                    detail.WithdrawDepositState = WithdrawDepositState.拒绝;
                    detail.Remark = "提现的金额过大或过小, 无法处理";
                    _context.SaveChanges();
                    return new HbzsManagerResult(HbzsManagerResultCode.Sucess, "提现的金额过大或过小, 无法处理");
                }

                if (dto.IsPass == false)
                {
                    detail.WithdrawDepositState = WithdrawDepositState.拒绝;
                    _context.SaveChanges();
                    return new HbzsManagerResult(HbzsManagerResultCode.Sucess, "");
                }


                //转账
                var paylog = Alipayment(detail, detailMemnber);

                if (paylog.code != "10000")
                {
                    detail.Remark = paylog.sub_msg;
                    detail.WithdrawDepositState = WithdrawDepositState.失败;
                    _context.SaveChanges();

                    return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, paylog.sub_msg);
                }


                using (var transaction = _context.Database.BeginTransaction())
                {
                    detail.WithdrawDepositState = WithdrawDepositState.通过;

                    _context.Payments.Add(new Payment()
                    {
                        OrderId = detail.Id,
                        MemberId = Member.Id,
                        Amount = detail.Amount
                    });

                    _context.SaveChanges();

                    transaction.Commit();
                }

                return new HbzsManagerResult(HbzsManagerResultCode.Sucess, "");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }


        private AlipayLog Alipayment(WithdrawDeposit withdrawDeposit, Member PayMember)
        {
            var bizContent = new
            {
                out_biz_no = withdrawDeposit.Timestamp,
                payee_type = "ALIPAY_LOGONID",
                payee_account = PayMember.AliPayAccount,
                amount = withdrawDeposit.Amount - 2,
                payer_show_name = $"{PayMember.Phone}申请提现",
                payee_real_name = PayMember.RealName,
                remark = "转账备注:送钱包提现"
            };

            AlipayFundTransToaccountTransferRequest request = new AlipayFundTransToaccountTransferRequest();
            request.BizContent = JsonConvert.SerializeObject(bizContent);
            AlipayFundTransToaccountTransferResponse response = _alipayService.Execute(request);
            var log = JsonConvert.DeserializeObject<AlipayResponse>(response.Body).alipay_fund_trans_toaccount_transfer_response;

            log.PaymentId = withdrawDeposit.Id;
            _context.AlipayLogs.Add(log);
            _context.SaveChanges();

            return log;
        }

        /// <summary>
        /// 获取明细
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsManagerResult<GetAuditWithdrawDepositdetailDto> GetAuditWithdrawDepositdetails([FromQuery] int memberId)
        {
            try
            {

                var result = new GetAuditWithdrawDepositdetailDto()
                {
                    IncomeDetails = _context.IncomeAccounts
                                    .Where(i => i.MemberId == memberId)
                                    .OrderBy(i => i.AddTime)
                                    .Select(i => new IncomeDetail
                                    {
                                        Id = i.Id,
                                        IncomeAccountTypeName = i.IncomeAccountType.ToString(),
                                        Amount = i.Amount,
                                        Remark = i.Remark,
                                        AddTime = i.AddTime
                                    }).ToList(),

                    WithdrawDepositDetails = _context.WithdrawDeposits
                                    .Where(w => w.MemberId == memberId)
                                    .OrderBy(w => w.Id)
                                    .Select(w => new WithdrawDepositDetail
                                    {
                                        Id = w.Id,
                                        WithdrawDepositStateName = w.WithdrawDepositState.ToString(),
                                        Amount = w.Amount,
                                        AddTime = w.AddTime
                                    }).ToList(),

                    IncomeTotal = _context.IncomeAccounts
                                .Where(i => i.MemberId == memberId)
                                .Sum(i => i.Amount),

                    WithdrawDeposit = _context.WithdrawDeposits
                                .Where(i => i.MemberId == memberId && i.WithdrawDepositState == WithdrawDepositState.通过)
                                .Sum(i => i.Amount),
                };

                result.Balance = result.IncomeTotal - result.WithdrawDeposit;

                return new HbzsManagerResult<GetAuditWithdrawDepositdetailDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult<GetAuditWithdrawDepositdetailDto>(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }
    }
}