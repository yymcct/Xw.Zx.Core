using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sieve.Services;
using swiftpass.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service;

namespace Xw.Zx.Core.Controllers
{
    /// <summary>
    /// 中信支付
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Config.Swagger.HiddenApi]
    public class CiticbankController : BaseController
    {
        private readonly ILogger<CiticbankController> _logger;
        private readonly ICiticbankService _citicbankService;
        private readonly IOrderService _orderService;

        public CiticbankController(ILogger<CiticbankController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor
            , ICiticbankService citicbankService
            , IOrderService orderService) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
            _citicbankService = citicbankService;
            _orderService = orderService;
        }

        /// <summary>
        /// 获取支付二维码
        /// </summary>
        [HttpPost]
        [Authorize]
        public HbzsResult<string> ScanCodeGen([FromQuery] int orderId)
        {
            try
            {
                var order = _context.Orders.First(o => o.MemberId == Member.Id
                                                && o.OrderState == OrderState.待付款
                                                && o.Id == orderId);

                var res = _citicbankService.CreateQrcodePayUrl(new CiticbankDto.Product()
                {
                    Name = order.ProducName,
                    Timestamp = order.Timestamp,
                    Amount = order.Amount,
                    MemberId = order.MemberId
                });

                return new HbzsResult<string>(res.CodeUrl);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsResult<string>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// 获取微信jsapi
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public HbzsResult<string> JsApi([FromQuery] int orderId)
        {
            try
            {
                var order = _context.Orders.First(o => o.MemberId == Member.Id
                                                && o.OrderState == OrderState.待付款
                                                && o.Id == orderId);

                var res = _citicbankService.CreateJSApiPayInfo(new CiticbankDto.Product()
                {
                    Name = order.ProducName,
                    Timestamp = order.Timestamp,
                    Amount = order.Amount,
                    MemberId = order.MemberId
                });

                return new HbzsResult<string>(res);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsResult<string>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }


        [HttpPost]
        public async void Notifyurl()
        {
            using (var buffer = new MemoryStream())
            {
                Request.Body.CopyTo(buffer);

                buffer.Position = 0;
                using (StreamReader sr = new StreamReader(buffer))
                {
                    var body = sr.ReadToEnd();
                    ClientResponseHandler resHandler = new ClientResponseHandler();
                    resHandler.setContent(body);

                    _logger.LogWarning("中信Body" + body);
                    Hashtable resParam = resHandler.getAllParameters();
                    if (resHandler.isTenpaySign())
                    {
                        _logger.LogWarning("中信验签正确");
                        if (int.Parse(resParam["status"].ToString()) == 0 && int.Parse(resParam["result_code"].ToString()) == 0)
                        {
                            _orderService.OrderPay(resParam["out_trade_no"].ToString(), OrderPaymentType.中信);

                            await Response.WriteAsync(JsonConvert.SerializeObject(new
                            {
                                flag = "1"
                            }));
                        }

                    }
                    _logger.LogWarning("中信验签错误");
                }
            }
        }
    }
}
