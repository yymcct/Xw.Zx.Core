


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
    [Authorize(Policy = "Admins")]
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
        public HbzsManagerResult<WithdrawDepositTotalMDto> GetWithdrawDeposits([FromQuery] SieveModel sieveModel)
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
                             MemberVipType = member.MemberVipType,
                             MemberVipTypeName = member.MemberVipType.ToString(),
                             Address = member.Address,
                             BusinessCode = member.BusinessCode,
                             WithdrawDepositStateName = withdrawdeposit.WithdrawDepositState.ToString(),
                             WithdrawCharge = withdrawdeposit.WithdrawCharge,
                             RealityAmount = withdrawdeposit.RealityAmount,
                         };


                var result = new WithdrawDepositTotalMDto()
                {
                    WithdrawDepositMDtos = _sieveProcessor.Apply(sieveModel, db).ToList(),
                  //  QueryTotal = _sieveProcessor.Apply(sieveModel, db.Where(w => w.WithdrawDepositState == WithdrawDepositState.提现成功), null, true, true, false).Sum(o => o.Amount),
                  //  AllTotal = db.Where(w => w.WithdrawDepositState == WithdrawDepositState.提现成功).Sum(o => o.Amount),
                  //  OrderTotal = _context.Orders.Where(o => o.OrderState == OrderState.已付款).Sum(w => w.Amount),
                };
               // result.Balance = result.OrderTotal - result.AllTotal;

                var total = _sieveProcessor.Apply(sieveModel, db, null, true, true, false).Count();
                return new HbzsManagerResult<WithdrawDepositTotalMDto>(result, total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult<WithdrawDepositTotalMDto>(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }



        private bool CheckDetailMember(WithdrawDeposit detail, Member detailMemnber)
        {
            if (detail == null)
            {
                throw new Exception("单据状态异常,无法处理!");
            }

            if (detailMemnber.Disabled == true || string.IsNullOrEmpty(detailMemnber.AliPayAccount))
            {
                throw new Exception("申请人账户异常,无法处理!");
            }

            //TODO 系统校验
            var IncomTotal = _context.IncomeAccounts
                .Where(b => b.MemberId == detail.MemberId && b.IncomeAccountState == IncomeAccountState.已发放)
                .Sum(b => b.Amount);

            var WithdrawDeposit = _context.WithdrawDeposits
                    .Where(b => b.MemberId == detail.MemberId
                            && b.WithdrawDepositState == WithdrawDepositState.提现成功)
                    .Sum(b => b.Amount);

            var canGet = IncomTotal - WithdrawDeposit;
            if (detail.Amount < 1m || detail.Amount > canGet)
            {
                detail.WithdrawDepositState = WithdrawDepositState.提现失败;
                detail.Remark = "提现的金额过大或过小, 无法处理";
                _context.SaveChanges();

                throw new Exception("提现的金额过大或过小, 无法处理");
            }

            return true;
        }

        /// <summary>
        /// 统计部审核通过
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = nameof(MemberRole.Admin_Tongjibu))]        
        public HbzsManagerResult TongjibuAudit([FromQuery] int id)
        {
            try
            {

                var detail = _context.WithdrawDeposits.First(w => w.Id == id && w.WithdrawDepositState == WithdrawDepositState.申请提现);
                var detailMemnber = _context.Members.First(m => m.Id == detail.MemberId);

                if (CheckDetailMember(detail, detailMemnber))
                {
                    _context.WithdrawDepositLogs.Add(new WithdrawDepositLog()
                    {
                        WithdrawDepositId = id,
                        AddUserId = Member.Id,
                        WithdrawDepositState = WithdrawDepositState.统计部审核,
                    });

                    detail.WithdrawDepositState = WithdrawDepositState.统计部审核;
                    _context.SaveChanges();


                    return new HbzsManagerResult(HbzsManagerResultCode.Sucess, "");
                }

                return new HbzsManagerResult(HbzsManagerResultCode.Sucess, "");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        [HttpPost]
        [Authorize(Roles = nameof(MemberRole.Admin_Caiwu))]
        public HbzsManagerResult CaiwuAudit([FromQuery] int id)
        {
            try
            {
                var detail = _context.WithdrawDeposits.First(w => w.Id == id && w.WithdrawDepositState == WithdrawDepositState.统计部审核);
                var detailMemnber = _context.Members.First(m => m.Id == detail.MemberId);

                if (CheckDetailMember(detail, detailMemnber))
                {
                    _context.WithdrawDepositLogs.Add(new WithdrawDepositLog()
                    {
                        WithdrawDepositId = id,
                        AddUserId = Member.Id,
                        WithdrawDepositState = WithdrawDepositState.财务部审核,
                    });

                    detail.WithdrawDepositState = WithdrawDepositState.财务部审核;
                    _context.SaveChanges();


                    return new HbzsManagerResult(HbzsManagerResultCode.Sucess, "");
                }

                return new HbzsManagerResult(HbzsManagerResultCode.Sucess, "");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }
        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = nameof(MemberRole.Admin_CaiwuManager))]
        public HbzsManagerResult Pay([FromQuery] int id)
        {
            try
            {
                var detail = _context.WithdrawDeposits.First(w => w.Id == id && w.WithdrawDepositState == WithdrawDepositState.财务部审核);
                var detailMemnber = _context.Members.First(m => m.Id == detail.MemberId);

                if (CheckDetailMember(detail, detailMemnber))
                {
                    var withdrawLog = new WithdrawDepositLog()
                    {
                        WithdrawDepositId = id,
                        AddUserId = Member.Id,
                        WithdrawDepositState = WithdrawDepositState.提现成功,
                    };

                    //转账
                    var paylog = Alipayment(detail, detailMemnber);

                    if (paylog.code == "10000")
                    {
                        using (var transaction = _context.Database.BeginTransaction())
                        {
                            detail.WithdrawDepositState = WithdrawDepositState.提现成功;

                            _context.Payments.Add(new Payment()
                            {
                                OrderId = detail.Id,
                                MemberId = Member.Id,
                                Amount = detail.Amount
                            });

                            _context.WithdrawDepositLogs.Add(withdrawLog);

                            _context.MemberBalanceLogs.Add(new MemberBalanceLog()
                            {
                                Memberid = detailMemnber.Id,
                                memberMoneySource = MemberMoneySource.提现,
                                SourceId = detail.Id,
                                Amount = detail.Amount,
                                OriginalMoney = detailMemnber.Money,
                                CurMoney = detailMemnber.Money - detail.Amount,
                                Remark = $"{detailMemnber.RealName}[{detailMemnber.Phone}]提现:{detail.Amount}元, 手续费:{detail.WithdrawCharge}"
                            });
                            detailMemnber.Money -= detail.Amount;
                            _context.SaveChanges();

                            transaction.Commit();
                        }


                        return new HbzsManagerResult(HbzsManagerResultCode.Sucess, "");
                    }
                    else
                    {
                        detail.Remark = paylog.sub_msg;
                        detail.WithdrawDepositState = WithdrawDepositState.提现失败;

                        withdrawLog.WithdrawDepositState = WithdrawDepositState.提现失败;
                        _context.WithdrawDepositLogs.Add(withdrawLog);

                        _context.SaveChanges();

                        return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, paylog.sub_msg);
                    }
                }

                return new HbzsManagerResult(HbzsManagerResultCode.Sucess, "");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin_Tongjibu,Admin_Caiwu, Admin_CaiwuManager")]
        //[Authorize(Roles =  nameof(MemberRole.Admin_Tongjibu))]
        //[Authorize(Roles = nameof(MemberRole.Admin_Caiwu))]
        //[Authorize(Roles = nameof(MemberRole.Admin_CaiwuManager))]
        public HbzsManagerResult Fail([FromQuery] int id)
        {
            try
            {

                var detail = _context.WithdrawDeposits.First(w => w.Id == id && (w.WithdrawDepositState == WithdrawDepositState.申请提现 || w.WithdrawDepositState == WithdrawDepositState.统计部审核 || w.WithdrawDepositState == WithdrawDepositState.财务部审核));
                var detailMemnber = _context.Members.First(m => m.Id == detail.MemberId);

                if (detail == null)
                {
                    throw new Exception("单据状态异常,无法处理!");
                }

                var withdrawLog = new WithdrawDepositLog()
                {
                    WithdrawDepositId = id,
                    AddUserId = Member.Id,
                    WithdrawDepositState = WithdrawDepositState.提现失败,
                };

                _context.WithdrawDepositLogs.Add(withdrawLog);

                detail.WithdrawDepositState = WithdrawDepositState.提现失败;
                _context.SaveChanges();

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
                amount = withdrawDeposit.RealityAmount,
                payer_show_name = $"{PayMember.Phone}申请提现",
                payee_real_name = PayMember.RealName,
                remark = "转账备注:债减减提现到账"
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
                                    .Where(i => i.MemberId == memberId && i.IncomeAccountState == IncomeAccountState.已发放)
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
                                .Where(i => i.MemberId == memberId && i.IncomeAccountState == IncomeAccountState.已发放)
                                .Sum(i => i.Amount),

                    WithdrawDeposit = _context.WithdrawDeposits
                                .Where(i => i.MemberId == memberId && i.WithdrawDepositState == WithdrawDepositState.提现成功)
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