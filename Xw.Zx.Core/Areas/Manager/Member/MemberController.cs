


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
    public class MemberController : ManagerBaseController
    {
        private readonly ILogger<MemberController> _logger;

        public MemberController(ILogger<MemberController> logger
            , XwZxContext context
            , IMapper mapper
            , ISieveProcessor sieveProcessor) : base(context, mapper, sieveProcessor)
        {
            _logger = logger;
        }
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HbzsManagerResult<ManagerMemberDto> GetUser()
        {
            try
            {
                var member = _mapper.Map<ManagerMemberDto>(Member);

                return new HbzsManagerResult<ManagerMemberDto>(member);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult<ManagerMemberDto>(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HbzsManagerResult<List<MemberMDto>> GetMembers([FromQuery]SieveModel sieveModel)
        {
            try
            {
                var db = from member in _context.Members
                         select new MemberMDto
                         {
                             Id = member.Id,
                             RoleName = member.RoleName,
                             Password = member.Password,
                             Nick = member.Nick,
                             Photo = member.Photo,
                             BirthDay = member.BirthDay,
                             Phone = member.Phone,
                             WxOpenId = member.WxOpenId,
                             CreateDate = member.CreateDate,
                             Remark = member.Remark,
                             RealName = member.RealName,
                             MemberVipType = member.MemberVipType,
                             MemberVipTypeName = member.MemberVipType.ToString(),
                             AliPayAccount = member.AliPayAccount,
                             QueryTimes = member.QueryTimes
                         };

                var list = _sieveProcessor.Apply(sieveModel, db).ToList();
                var total = _sieveProcessor.Apply(sieveModel, db, null, true, true, false).Count();
                return new HbzsManagerResult<List<MemberMDto>>(list, total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult<List<MemberMDto>>(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="membermdto"></param>
        /// <returns></returns>
        [HttpPost]
        public HbzsManagerResult PostMember([FromBody]PostMemberMDto membermdto)
        {
            try
            {
                if (membermdto.Id == 0)
                {
                    AddMember(membermdto);
                }
                else
                {
                    UpdateMember(membermdto);
                }

                return new HbzsManagerResult(HbzsManagerResultCode.Sucess, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsManagerResult DeleteMember([FromQuery]int id)
        {
            try
            {
                var meetingarea = _context.Members.Find(id);
                if (meetingarea != null)
                {
                    _context.Members.Remove(meetingarea);
                    _context.SaveChanges();
                }
                return new HbzsManagerResult(HbzsManagerResultCode.Sucess, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }

        private void AddMember(PostMemberMDto membermdto)
        {
            var member = new Member()
            {
                RoleName = membermdto.RoleName,
                Password = membermdto.Password,
                Nick = membermdto.Nick,
                Photo = membermdto.Photo,
                BirthDay = membermdto.BirthDay,
                Phone = membermdto.Phone,
                Remark = membermdto.Remark,
            };

            _context.Members.Add(member);

            _context.SaveChanges();
        }

        private void UpdateMember(PostMemberMDto membermdto)
        {
            var member = _context.Members.First(m => m.Id == membermdto.Id);

            member.Password = membermdto.Password;
            member.Nick = membermdto.Nick;
            member.Photo = membermdto.Photo;
            member.BirthDay = membermdto.BirthDay;
            member.Phone = membermdto.Phone;
            member.Remark = membermdto.Remark;

            _context.Entry(member).State = EntityState.Modified;

            _context.SaveChanges();

        }
    }
}