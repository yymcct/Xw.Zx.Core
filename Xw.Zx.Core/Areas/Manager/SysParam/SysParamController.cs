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
    [Authorize(Policy = "Admins")]
    public class SysParamController : ManagerBaseController
    {
        private readonly ILogger<SysParamController> _logger;

        public SysParamController(ILogger<SysParamController> logger
            , XwZxContext context
            , IMapper mapper
            , ISieveProcessor sieveProcessor) : base(context, mapper, sieveProcessor)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        [HttpPost]
        public HbzsManagerResult<bool> SetValue([FromQuery] string name, string val)
        {
            try
            {
                var model = _context.SysParams.First(s => s.Name == name);

                model.Value = val;

                _context.SaveChanges();

                return new HbzsManagerResult<bool>(true);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult<bool>(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }

        [HttpPost]
        public HbzsManagerResult<string> GetValue([FromQuery] string name)
        {
            try
            {
                var model = _context.SysParams.First(s => s.Name == name);

                return new HbzsManagerResult<string>(model.Value);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult<string>(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }
    }
}