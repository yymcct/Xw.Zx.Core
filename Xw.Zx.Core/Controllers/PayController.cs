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
using Microsoft.EntityFrameworkCore;
using Alipay.AopSdk.F2FPay.Domain;
using Alipay.AopSdk.F2FPay.Business;
using Alipay.AopSdk.F2FPay.Model;
using Alipay.AopSdk.Core;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Config.Swagger.HiddenApi]
    public class PayController : BaseController
    {
        private readonly ILogger<PayController> _logger;
        private readonly AlipayService _alipayService;
        private readonly AlipayF2FService _alipayF2FService;
        private readonly IUpDateVip1Service _upDateVip1Service;
        private readonly IOrderService _orderService;
        private readonly ITonglianService _tonglianService;
        public PayController(ILogger<PayController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor
            , AlipayService alipayService
            , AlipayF2FService alipayF2FService
            , IUpDateVip1Service upDateVip1Service
            , IOrderService orderService
            , ITonglianService tonglianService) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
            _alipayService = alipayService;
            _alipayF2FService = alipayF2FService;
            _upDateVip1Service = upDateVip1Service;
            _orderService = orderService;
            _tonglianService = tonglianService;
        }

        [HttpGet]
        public string Get()
        {
            _tonglianService.CreateQrcodePayUrl(null);
            return "333";
        }
    }
}