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
        /// 获取自己名下所有的VIP授权码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult<List<UpdaeVipAuthCodeDto>> Get([FromQuery]SieveModel sieveModel)
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
        public HbzsResult Create([FromQuery]int memberid, int cnt)
        {
            try
            {
                if (Member.Phone != "18624938007") throw new Exception("无权限!");

                var member = _context.Members.First(m => m.Id == memberid);
                if (member.MemberVipType == MemberVipType.普通) throw new Exception("普通会员不能拥有升级码");

                for (int i = 0; i < cnt; i++)
                {
                    var code = new UpdateVipAuthCode() { 
                        OwinId = memberid,
                        Code =  Guid.NewGuid().ToString(),
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
        [HttpGet]
        public HbzsResult<MemberDto> Use(string code)
        {
            try
            {
                var vipUpdateCode = _context
                                        .UpdateVipAuthCodes
                                        .Where(v => v.Code == code && v.ExpiesTime > DateTime.Now)
                                        .FirstOrDefault();

                //检查升级码 状态
                if (vipUpdateCode == null)
                {
                    throw new Exception("升级码不存在, 或已过期!");
                }

                //检查自身状态
                if (Member.MemberVipType != MemberVipType.普通)
                {
                    throw new Exception("已经是VIP! 使用无效");
                }

                //检查我是否是升级码所有者的团队成员
                var InviteId = Member.InviteId;
                var InviteInviteId = _context.Members.First(m => m.Id == InviteId).InviteId;
                if (!(vipUpdateCode.OwinId == Member.Id
                    || vipUpdateCode.OwinId == InviteId
                    || vipUpdateCode.OwinId == InviteInviteId))
                {
                    throw new Exception("您不是赠送人团队成员,无法使用!");
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    Member.MemberVipType = MemberVipType.Vip会员;
                    Member.Remark = $"{DateTime.Now}通过升级码:{vipUpdateCode.Id}升级";
                    _context.Entry(Member).State = EntityState.Modified;

                    vipUpdateCode.UPdateVipAuthCodeState = UpdateVipAuthCodeState.已使用;
                    vipUpdateCode.UsedMemberId = Member.Id;
                    vipUpdateCode.UsedTime = DateTime.Now;
                    vipUpdateCode.Remark = $"用户ID{Member.Id}, 电话{Member.Phone} 使用的";
                    //Save and discard changes
                    _context.SaveChanges();

                    //if we get here things are looking good.
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
    }
}
