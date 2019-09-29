using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class VipUpdateCodeController : ControllerBase
    {
        private readonly XwZxContext _xwZxContext;
        private readonly ILogger<VipUpdateCodeController> _logger;


        public VipUpdateCodeController(
             XwZxContext xwZxContext
            , ILogger<VipUpdateCodeController> logger
        )
        {

            _xwZxContext = xwZxContext;
            _logger = logger;

        }


        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> Get()
        {
            // _mailService.MailSyanc(1195);
            return "同步完成";
        }

    }
}
