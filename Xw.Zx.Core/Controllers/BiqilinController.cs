using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service;

namespace Xw.Zx.Core.Controllers
{
    /// <summary>
    /// 碧麒麟支付
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Config.Swagger.HiddenApi]
    public class BiqilinController : BaseController
    {
        private readonly ILogger<BiqilinController> _logger;
        private readonly IBiqilinService _biqilinService;
        private readonly IOrderService _orderService;

        public BiqilinController(ILogger<BiqilinController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor
            , IBiqilinService biqilinService
            , IOrderService orderService) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
            _biqilinService = biqilinService;
            _orderService = orderService;
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [Authorize]
        public HbzsResult<string> ScanCodeGen([FromBody] BiqilinDto biqilinDto)
        {
            try
            {
                var order = _context.Orders.First(o => o.MemberId == Member.Id
                                                && o.OrderState == OrderState.待付款
                                                && o.Id == biqilinDto.OrderId);

                var res = _biqilinService.CreateQrcodePayUrl(new Biqilin_Product()
                {
                    Name = order.ProducName,
                    Timestamp = order.Timestamp,
                    Amount = order.Amount,
                    Biqilin_PayType = biqilinDto.Biqilin_PayType
                });

                _context.BiqilinLogs.Add(new BiqilinLog
                {
                    OrderId = order.Id,
                    BiqilinOrderNo = res.orderNo
                });
                _context.SaveChanges();

                return new HbzsResult<string>(res.codeUrl);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsResult<string>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public HbzsResult<JsapiPayResponeDto.JsapiPay> JsapiPay([FromQuery] int orderId, string openId)
        {
            try
            {
                var order = _context.Orders.First(o => o.MemberId == Member.Id
                                                && o.OrderState == OrderState.待付款
                                                && o.Id == orderId);

                var wxRespone = _biqilinService.CreateWeixinJsApi(new Biqilin_Product()
                {
                    Name = order.ProducName,
                    Timestamp = order.Timestamp,
                    Amount = order.Amount,
                    Biqilin_PayType = Biqilin_PayType.微信,
                    OpenId = openId
                });

                _context.BiqilinLogs.Add(new BiqilinLog
                {
                    OrderId = order.Id,
                    BiqilinOrderNo = wxRespone.orderNo
                });
                _context.SaveChanges();

                return new HbzsResult<JsapiPayResponeDto.JsapiPay>(_mapper.Map<JsapiPayResponeDto.JsapiPay>(wxRespone));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsResult<JsapiPayResponeDto.JsapiPay>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }



        /// <summary>
        /// 支付宝支付成功回调地址 
        /// TODO: 校验是否是来自于支付宝的域名
        /// </summary>
        [HttpPost]
        public async void Notifyurl([FromBody] BiqilinNotifyDto biqilinNotifyDto)
        {
            _logger.LogWarning("biqilinNotifyDto:" + JsonConvert.SerializeObject(biqilinNotifyDto));


            if (biqilinNotifyDto.orderStatus == "TRADE_SUCCESS")
            {
                var query = _biqilinService.QueryOrder(biqilinNotifyDto.orderNo);
                var waitPayOrder = _context.Orders.First(o => o.Timestamp == biqilinNotifyDto.outOrderNo);
                if (CheckBiqilinOrder(waitPayOrder, query))
                {
                    _orderService.OrderPay(biqilinNotifyDto.outOrderNo, OrderPaymentType.碧麒麟);
                }
            }
            await Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                flag = "1"
            }));
        }

        //TODO 注释掉
        [HttpGet]
        public void Notifyurl2()
        {
            _orderService.OrderPay("20210120100012461521", OrderPaymentType.碧麒麟);
        }

        private bool CheckBiqilinOrder(Order order, BiqilinRespone.Query query)
        {
            if (order != null && query != null
                && query.orderStatus == "TRADE_SUCCESS"
                && query.outOrderNo == order.Timestamp
                && (decimal.Parse(query.amount)) == order.Amount)
            {
                return true;
            }

            return false;
        }
    }
}
