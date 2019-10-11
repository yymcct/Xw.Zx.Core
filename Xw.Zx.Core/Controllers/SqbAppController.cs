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
using Microsoft.Extensions.Logging;
using Sieve.Services;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SqbAppController : BaseController
    {
        private readonly ILogger<SqbAppController> _logger;

        public SqbAppController(ILogger<SqbAppController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor
            , IUpDateVip1Service upDateVip1Service) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
        }

        /// <summary>
        /// App升级
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public HbzsResult<AppVersion> GetAndroidVersion()
        {
            try
            {
               var ver =  _context.AppVersions.Where(a => a.AppPlatform == AppPlatform.Android).OrderByDescending(a => a.AddTime).First();
                return new HbzsResult<AppVersion>(ver);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsResult<AppVersion>(HbzsResultCode.Invalid_Error, ex.Message);
            }

        }    
    }
}