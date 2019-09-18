using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sieve.Services;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InterestController : BaseController
    {
        private readonly ILogger<MemberController> _logger;
        public InterestController(ILogger<MemberController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
        }

        /// <summary>
        /// 生成追息单, 根据 数据:位置信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HbzsResult PostInterest()
        {
            throw new Exception();
        }


    }
}