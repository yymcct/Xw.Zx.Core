


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

namespace Xw.Zx.Core.Areas.Manager
{

    [ApiController]
    [Route("manager/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class UpdateVipAuthCodeController : ManagerBaseController
    {
        private readonly ILogger<UpdateVipAuthCodeController> _logger;

        public UpdateVipAuthCodeController(ILogger<UpdateVipAuthCodeController> logger
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
        public HbzsManagerResult<List<UpdateVipAuthCodeMDto>> GetUpdateVipAuthCodes([FromQuery]SieveModel sieveModel)
        {
            try
            {
                var db = from updatevipauthcode in _context.UpdateVipAuthCodes
                         join member in _context.Members on updatevipauthcode.UsedMemberId equals member.Id into joinTemp
                         from tmp in joinTemp.DefaultIfEmpty()
                         where (updatevipauthcode.UPdateVipAuthCodeState == UpdateVipAuthCodeState.已使用)
                                 || (updatevipauthcode.UPdateVipAuthCodeState == UpdateVipAuthCodeState.待使用
                                 && updatevipauthcode.ExpiesTime > DateTime.Now) //只显示已使用的和待使用的
                         select new UpdateVipAuthCodeMDto
                         {
                             Id = updatevipauthcode.Id,
                             UsedMemberId = updatevipauthcode.UsedMemberId,
                             UsedTime = updatevipauthcode.UsedTime,
                             Code = updatevipauthcode.Code,
                             ExpiesTime = updatevipauthcode.ExpiesTime,
                             UPdateVipAuthCodeState = updatevipauthcode.UPdateVipAuthCodeState,
                             Remark = updatevipauthcode.Remark,
                             AddTime = updatevipauthcode.AddTime,
                             UsedMemberName = tmp.RealName,
                             UsedMemberPhone = tmp.Phone,
                             UPdateVipAuthCodeStateName = updatevipauthcode.UPdateVipAuthCodeState.ToString(),
                         };

                var list = _sieveProcessor.Apply(sieveModel, db).ToList();
                var total = _sieveProcessor.Apply(sieveModel, db, null, true, true, false).Count();
                return new HbzsManagerResult<List<UpdateVipAuthCodeMDto>>(list, total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult<List<UpdateVipAuthCodeMDto>>(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="updatevipauthcodemdto"></param>
        /// <returns></returns>
        [HttpPost]
        public HbzsManagerResult PostUpdateVipAuthCode([FromBody]PostUpdateVipAuthCodeMDto updatevipauthcodemdto)
        {
            try
            {
                if (!AppsettingsUtility.CanCreateUpdateVipCodePhone.Any(p => p == Member.Phone))
                {
                    return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, "无权限");
                }


                if (Member.MemberVipType == MemberVipType.普通) throw new Exception("普通会员不能拥有升级码");

                var random = new Random();
                for (int i = 0; i < updatevipauthcodemdto.Cnt; i++)
                {
                    var codeNum = random.Next(100000, 999999).ToString();

                    var code = new UpdateVipAuthCode()
                    {
                        OwinId = Member.Id,
                        Code = codeNum,
                    };
                    _context.UpdateVipAuthCodes.Add(code);
                }
                _context.SaveChanges();

                return new HbzsManagerResult(HbzsManagerResultCode.Sucess, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }

        //[HttpGet]
        //public HbzsManagerResult DeleteUpdateVipAuthCode([FromQuery]int id)
        //{
        //    try
        //    {
        //        var meetingarea = _context.UpdateVipAuthCodes.Find(id);
        //        if (meetingarea != null)
        //        {
        //            _context.UpdateVipAuthCodes.Remove(meetingarea);
        //            _context.SaveChanges();
        //        }
        //        return new HbzsManagerResult(HbzsManagerResultCode.Sucess, "");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, ex.Message);
        //    }
        //}
    }
}