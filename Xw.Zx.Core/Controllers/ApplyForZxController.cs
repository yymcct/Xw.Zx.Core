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
    public class ApplyForZxController : BaseController
    {
        private readonly ILogger<ApplyForZxController> _logger;
        public ApplyForZxController(ILogger<ApplyForZxController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;

        }

        /// <summary>
        /// 提交追息申请, 提交前需补全用户基本信息
        /// </summary>
        /// <param name="postApplyDto"></param>
        /// <returns></returns>
        [HttpPost]
        public HbzsResult PostApply([FromBody]PostApplyDto postApplyDto)
        {
            try
            {
                if (string.IsNullOrEmpty(Member.RealName)
                    || string.IsNullOrEmpty(Member.CityCode))
                {
                    return new HbzsResult(HbzsResultCode.Invalid_Error, "请先补全基本信息");
                }

                if (_context.ApplyForZxs.Any(a => a.MemberId == Member.Id && a.ApplyForZxState != ApplyForZxState.已完结))
                {
                    return new HbzsResult(HbzsResultCode.Invalid_Error, "已收到您的申请,无需重复提交!");
                }

                var apply = new ApplyForZxMDto()
                {
                    MemberId = Member.Id,
                    Remark = postApplyDto.Remark,
                };
                _context.ApplyForZxs.Add(apply);
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
        /// 查看系统匹配到自己的追息申请单,服务站及以上级别可看, 否提示异常信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult<List<ApplyDto>> GettApply([FromQuery]SieveModel sieveModel)
        {
            List<ApplyDto> applyDtos = new List<ApplyDto>();

            try
            {
                if (Member.MemberVipType < MemberVipType.服务站)
                {
                    return new HbzsResult<List<ApplyDto>>(HbzsResultCode.Invalid_Error, "请升级服务站及以上级别查看");
                }
                if (string.IsNullOrEmpty(Member.CityCode))
                {
                    return new HbzsResult<List<ApplyDto>>(HbzsResultCode.Invalid_Error, "请补全自身地址信息");
                }
                var provinceCode = Member.CityCode.Substring(0, 2);//取省的代码


                var applysDb = (from apply in _context.ApplyForZxs
                    join member in _context.Members
                    on apply.MemberId equals member.Id
                    where member.CityCode.StartsWith(provinceCode)
                    select apply).AsNoTracking();                

                var applys = _sieveProcessor.Apply(sieveModel, applysDb).ToList();                

                for (var i = 0; i < applys.Count; i++)
                {
                    var dto = _mapper.Map<ApplyDto>(applys[i]);

                    dto.Amount = _context.BankBillDetails.Where(b=>b.MemberID == applys[i].MemberId).Sum(b => b.Amount);

                    dto.ApplyMember = _mapper.Map<MemberDto>(_context.Members.First(m=>m.Id == applys[i].MemberId));
                    applyDtos.Add(dto);
                }

                return new HbzsResult<List<ApplyDto>>(applyDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<List<ApplyDto>>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }
    }
}
