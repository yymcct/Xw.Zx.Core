using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class VoiceNewController : BaseController
    {
        private readonly ILogger<VoiceNewController> _logger;
        public VoiceNewController(ILogger<VoiceNewController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取音频文件
        /// </summary>
        /// <param name="sieveModel"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult<List<VoiceNew>> GetVoiceNews([FromQuery]SieveModel sieveModel)
        {
            try
            {
                var db = _context.VoiceNews;

                var res = _sieveProcessor
                    .Apply(sieveModel, db)
                    .ToList();

                return new HbzsResult<List<VoiceNew>>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<List<VoiceNew>>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }
    }
}