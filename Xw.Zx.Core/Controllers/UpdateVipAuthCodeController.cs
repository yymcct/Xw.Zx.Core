using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Utility;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UpdateVipAuthCodeController : BaseController
    {
        private readonly ILogger<UpdateVipAuthCodeController> _logger;

        public UpdateVipAuthCodeController(ILogger<UpdateVipAuthCodeController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
        }


        /// <summary>
        /// 获取自己名下所有的VIP升级码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult<List<UpdaeVipAuthCodeDto>> Get([FromQuery] SieveModel sieveModel)
        {
            try
            {
                var db = _context.UpdateVipAuthCodes
                        .AsNoTracking()
                        .Where(m => m.OwinId == Member.Id);

                var codes = _sieveProcessor
                       .Apply(sieveModel, db)
                       .ToList();

                var res = _mapper.Map<List<UpdaeVipAuthCodeDto>>(codes);

                return new HbzsResult<List<UpdaeVipAuthCodeDto>>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<List<UpdaeVipAuthCodeDto>>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// 生成指定数量的优惠券到指定用户的账号下, 内部使用,
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="cnt"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult Create([FromQuery] int memberid, int cnt)
        {
            try
            {
                if (!AppsettingsUtility.CanCreateUpdateVipCodePhone.Any(p => p == Member.Phone))
                {
                    return new HbzsResult(HbzsResultCode.Invalid_Error, "无权限");
                }

                var member = _context.Members.First(m => m.Id == memberid);
                if (member.MemberVipType == MemberVipType.普通) throw new Exception("普通会员不能拥有升级码");

                var random = new Random();
                for (int i = 0; i < cnt; i++)
                {
                    var codeNum = random.Next(100000, 999999).ToString();

                    var code = new UpdateVipAuthCode()
                    {
                        OwinId = memberid,
                        Code = codeNum,
                    };
                    _context.UpdateVipAuthCodes.Add(code);
                }
                _context.SaveChanges();

                return new HbzsResult(HbzsResultCode.Sucess);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult(HbzsResultCode.Invalid_Error, ex.Message);
            }

        }


        /// <summary>
        /// TODO:预留 暂不使用 将自己的升级码赠送给自己团队的某人
        /// </summary>
        /// <param name="code"></param>
        /// <param name="toMemberId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> Send(string code, int toMemberId)
        {
            // _mailService.MailSyanc(1195);
            return "预留";
        }

        /// <summary>
        /// 使用VIp升级码 升级为会员
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public HbzsResult<MemberDto> Use(string code)
        {
            try
            {
                var vipUpdateCode = _context
                                        .UpdateVipAuthCodes
                                        .Where(v => v.Code == code && v.ExpiesTime > DateTime.Now && v.UPdateVipAuthCodeState == UpdateVipAuthCodeState.待使用)
                                        .FirstOrDefault();

                //检查升级码 状态
                if (vipUpdateCode == null)
                {
                    throw new Exception("兑换卷不存在, 或已过期!");
                }

                //检查自身状态
                if (!(vipUpdateCode.MemberVipType > Member.MemberVipType))
                {
                    throw new Exception("兑换卷对您的级别不适用");
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    Member.MemberVipType = vipUpdateCode.MemberVipType;
                    Member.Remark = $"{DateTime.Now}通过升级码:{vipUpdateCode.Id}升级";
                    _context.Entry(Member).State = EntityState.Modified;

                    vipUpdateCode.UPdateVipAuthCodeState = UpdateVipAuthCodeState.已使用;
                    vipUpdateCode.UsedMemberId = Member.Id;
                    vipUpdateCode.UsedTime = DateTime.Now;
                    vipUpdateCode.Remark = $"用户ID{Member.Id}, 电话{Member.Phone} 使用";

                    _context.SaveChanges();
                    scope.Complete();
                }
                var selef = _mapper.Map<MemberDto>(Member);
                if (selef.InviteId != 0)
                {
                    var inviteUser = _context.Members
                       .FirstOrDefault(m => m.Id == selef.InviteId && m.Disabled == false);
                    if (inviteUser != null)
                    {
                        selef.InvitePhone = inviteUser.Phone;
                    }
                }

                return new HbzsResult<MemberDto>(selef);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<MemberDto>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// 获取是否有生成VIP升级码的权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult<bool> CanCreateCode()
        {
            if (AppsettingsUtility.CanCreateUpdateVipCodePhone.Any(phone => phone == Member.Phone))
            {
                return new HbzsResult<bool>(true);
            }

            return new HbzsResult<bool>(false);
        }
    }
}
