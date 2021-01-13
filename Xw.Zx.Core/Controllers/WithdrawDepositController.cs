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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service;
using Xw.Zx.Core.Utility;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class WithdrawDepositController : BaseController
    {
        private readonly ILogger<WithdrawDepositController> _logger;
        private readonly AlipayService _alipayService;
        private readonly IUpDateVip1Service _upDateVip1Service;
        private readonly IWithdrawService _withdrawService;
        public WithdrawDepositController(ILogger<WithdrawDepositController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor
            , AlipayService alipayService
            , IUpDateVip1Service upDateVip1Service
            , IWithdrawService withdrawService) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
            _alipayService = alipayService;
            _upDateVip1Service = upDateVip1Service;
            _withdrawService = withdrawService;
        }

        /// <summary>
        /// 提交提现申请, 生成申请单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HbzsResult PostWithdrawDeposit([FromBody] PostWithdrawDepositDto postWithdrawDepositDto)
        {
            try
            {
                if (Member.MemberVipType == MemberVipType.客户 || Member.Disabled == true)
                {
                    return new HbzsResult(HbzsResultCode.Invalid_Error, "您的账户异常, 请人工处理!");
                }

                if (string.IsNullOrEmpty(Member.RealName))
                {
                    return new HbzsResult(HbzsResultCode.Invalid_Error, "请前往[我的],补全个人信息后再申请提现!");
                }

                if (_context.WithdrawDeposits.Any(w => w.MemberId == Member.Id
                     && w.WithdrawDepositState == WithdrawDepositState.申请提现))
                {
                    return new HbzsResult(HbzsResultCode.Sucess, "我们已收到您的申请,正在处理中,请稍后");
                }

                var IncomTotal = _context.IncomeAccounts
                        .Where(b => b.MemberId == Member.Id && b.IncomeAccountState == IncomeAccountState.已发放)
                        .Sum(b => b.Amount);

                var WithdrawDeposit = _context.WithdrawDeposits
                        .Where(b => b.MemberId == Member.Id
                                && b.WithdrawDepositState == WithdrawDepositState.提现成功)
                        .Sum(b => b.Amount);

                var canGet = IncomTotal - WithdrawDeposit;

                if (postWithdrawDepositDto.Amount < 1.0m || postWithdrawDepositDto.Amount > canGet)
                {
                    return new HbzsResult(HbzsResultCode.Invalid_Error, "提现的金额过大或过小, 无法处理");
                }

                var charge = decimal.Parse((postWithdrawDepositDto.Amount * 0.15m / 100).ToString("#0.00"));

                var withdrawDeposit = new WithdrawDeposit()
                {
                    MemberId = Member.Id,
                    Amount = postWithdrawDepositDto.Amount,
                    WithdrawCharge = charge,
                    RealityAmount = postWithdrawDepositDto.Amount - charge
                };
                _context.WithdrawDeposits.Add(withdrawDeposit);
                _context.SaveChanges();
                return new HbzsResult(HbzsResultCode.Sucess, "我们已收到您的申请,正在处理中,请稍后");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsResult(HbzsResultCode.Invalid_Error, ex.Message);
            }

        }

        [HttpPost]
        public HbzsResult PostWithdrawDepositByShareProfitId([FromBody] WithdrawDepositRquest.shareProfitDto dto)
        {
            try
            {

                if (string.IsNullOrEmpty(Member.AliPayAccountName) || string.IsNullOrEmpty(Member.AliPayAccount))
                {
                    return new HbzsResult(HbzsResultCode.Invalid_Error, "请前往[我的],补全个人信息后再申请提现!");
                }


                _withdrawService.AddWithdrawByShareprofits(Member, dto.ShareProfitId);

                return new HbzsResult(HbzsResultCode.Sucess, "我们已收到您的申请,正在处理中,请稍后");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsResult(HbzsResultCode.Invalid_Error, ex.Message);
            }

        }


        /// <summary>
        /// 获取申请单
        /// </summary>
        /// <param name="sieveModel"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult<List<GetWithdrawDepositDetailsDto>> GetWithdrawDepositdetails([FromQuery] SieveModel sieveModel)
        {
            try
            {
                var db = _context.WithdrawDeposits
                     .AsNoTracking()
                     .Where(w => w.MemberId == Member.Id);

                var details = _sieveProcessor
                        .Apply(sieveModel, db)
                        .OrderByDescending(d => d.AddTime)
                        .ToList();

                var res = _mapper.Map<List<GetWithdrawDepositDetailsDto>>(details);

                return new HbzsResult<List<GetWithdrawDepositDetailsDto>>(res);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsResult<List<GetWithdrawDepositDetailsDto>>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// 待审核的提现申请单, 只有白名单账号可以查看
        /// </summary>
        /// <param name="sieveModel"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult<List<GetAuditWithdrawDepositDetailsDto>> GetAuditWithdrawDepositdetails([FromQuery] SieveModel sieveModel)
        {
            try
            {
                if (sieveModel.Page == null) sieveModel.Page = 1;

                if (sieveModel.PageSize == null) sieveModel.PageSize = 50;
                if (string.IsNullOrEmpty(sieveModel.Sorts)) sieveModel.Sorts = "-Id";
                if (!AppsettingsUtility.CanCreateUpdateVipCodePhone.Any(p => p == Member.Phone))
                {
                    return new HbzsResult<List<GetAuditWithdrawDepositDetailsDto>>(HbzsResultCode.Invalid_Error, "无权限");
                }
                var db = _context.WithdrawDeposits
                     .AsNoTracking();
                // .Where(w => w.WithdrawDepositState == WithdrawDepositState.申请中);

                var details = _sieveProcessor
                        .Apply(sieveModel, db)
                        .ToList();

                var res = details.Select(d => new GetAuditWithdrawDepositDetailsDto()
                {
                    DetailsDto = _mapper.Map<GetWithdrawDepositDetailsDto>(d),
                    MemberDto = _mapper.Map<MemberDto>(_context.Members.First(m => m.Id == d.MemberId))
                }).ToList();

                return new HbzsResult<List<GetAuditWithdrawDepositDetailsDto>>(res);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsResult<List<GetAuditWithdrawDepositDetailsDto>>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// 通过或拒绝
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public HbzsResult AuditWithdrawDepositdetail([FromBody] AuditWithdrawDepositdetailDto dto)
        {
            try
            {
                var detail = _context.WithdrawDeposits.First(w => w.Timestamp == dto.Timestamp);
                var detailMemnber = _context.Members.First(m => m.Id == detail.MemberId);

                if (!AppsettingsUtility.CanCreateUpdateVipCodePhone.Any(p => p == Member.Phone))
                {
                    return new HbzsResult(HbzsResultCode.Invalid_Error, "账户无权限!");
                }

                if (detail.WithdrawDepositState != WithdrawDepositState.申请提现)
                {
                    return new HbzsResult(HbzsResultCode.Invalid_Error, "单据状态异常,无法处理!");
                }

                if (detailMemnber.Disabled == true)
                {
                    return new HbzsResult(HbzsResultCode.Invalid_Error, "申请人账户异常,无法处理!");
                }

                var IncomTotal = _context.IncomeAccounts
                    .Where(b => b.MemberId == detail.MemberId && b.IncomeAccountState == IncomeAccountState.已发放)
                    .Sum(b => b.Amount);

                var WithdrawDeposit = _context.WithdrawDeposits
                        .Where(b => b.MemberId == detail.MemberId
                                && b.WithdrawDepositState == WithdrawDepositState.提现成功)
                        .Sum(b => b.Amount);

                var canGet = IncomTotal - WithdrawDeposit;
                if (detail.Amount < 2.09m || detail.Amount > canGet)
                {
                    detail.WithdrawDepositState = WithdrawDepositState.提现失败;
                    detail.Remark = "提现的金额过大或过小, 无法处理";
                    _context.SaveChanges();
                    return new HbzsResult(HbzsResultCode.Sucess, "提现的金额过大或过小, 无法处理");
                }

                if (dto.IsPass == false)
                {
                    detail.WithdrawDepositState = WithdrawDepositState.提现失败;
                    _context.SaveChanges();
                    return new HbzsResult(HbzsResultCode.Sucess);
                }


                //转账
                var paylog = Alipayment(detail, detailMemnber);

                if (paylog.code != "10000")
                {
                    detail.Remark = paylog.sub_msg;
                    detail.WithdrawDepositState = WithdrawDepositState.提现失败;
                    _context.SaveChanges();

                    return new HbzsResult(HbzsResultCode.Invalid_Error, paylog.sub_msg);
                }


                using (var transaction = _context.Database.BeginTransaction())
                {
                    detail.WithdrawDepositState = WithdrawDepositState.提现成功;

                    _context.Payments.Add(new Payment()
                    {
                        OrderId = detail.Id,
                        MemberId = Member.Id,
                        Amount = detail.Amount
                    });

                    _context.SaveChanges();

                    transaction.Commit();
                }

                return new HbzsResult(HbzsResultCode.Sucess);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsResult(HbzsResultCode.Invalid_Error, ex.Message);
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

    }
}