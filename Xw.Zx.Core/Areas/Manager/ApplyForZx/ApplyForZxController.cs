


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
    public class ApplyForZxController : ManagerBaseController
    {
        private readonly ILogger<ApplyForZxController> _logger;

        public ApplyForZxController(ILogger<ApplyForZxController> logger
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
        public HbzsManagerResult<List<ApplyForZxMDto>> GetApplyForZxs([FromQuery]SieveModel sieveModel)
        {
            try
            {
                var db = from applyforzx in _context.ApplyForZxs
                         join member in _context.Members on applyforzx.MemberId equals member.Id
                         select new ApplyForZxMDto
                         {
                             Id = applyforzx.Id,
                             MemberId = applyforzx.MemberId,
                             Remark = applyforzx.Remark,
                             ApplyForZxState = applyforzx.ApplyForZxState,
                             AddTime = applyforzx.AddTime,
                             MemberName = member.RealName,
                             MemberPhone = member.Phone,
                             ApplyForZxStateName = applyforzx.ApplyForZxState.ToString()
                         };

                var list = _sieveProcessor.Apply(sieveModel, db).ToList();
                var total = _sieveProcessor.Apply(sieveModel, db, null, true, true, false).Count();
                return new HbzsManagerResult<List<ApplyForZxMDto>>(list, total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult<List<ApplyForZxMDto>>(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }


        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsManagerResult DeleteApplyForZx([FromQuery]int id)
        {
            try
            {
                var meetingarea = _context.ApplyForZxs.Find(id);
                if (meetingarea != null)
                {
                    _context.ApplyForZxs.Remove(meetingarea);
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
    }
}