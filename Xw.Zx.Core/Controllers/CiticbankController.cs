using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly ICiticbankService _swiftPass;
        private readonly IOrderService _orderService;

        public CiticbankController(ILogger<CiticbankController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor
            , ICiticbankService swiftPass
            , IOrderService orderService) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
            _swiftPass = swiftPass;
            _orderService = orderService;
        }

        /// <summary>
        /// 
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

                var res = _swiftPass.CreateQrcodePayUrl(new CiticbankDto.Product()
                {
                    Name = order.ProducName,
                    Timestamp = order.Timestamp,
                    Amount = order.Amount,
                });

                _context.CiticbankLogs.Add(new CiticbankLog
                {
                    OrderId = order.Id,
                    SwiftPassUUID = res.uuid
                });
                _context.SaveChanges();

                return new HbzsResult<string>(res.CodeUrl);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsResult<string>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }




        /// <summary>
        /// 支付宝支付成功回调地址 
        /// TODO: 校验是否是来自于支付宝的域名
        /// </summary>
        [HttpPost]
        public async void Notifyurl()
        {


            var cfg = Utils.loadCfg();
            //初始化数据
            var buffer = new MemoryStream();
            Request.Body.CopyTo(buffer);

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

                        var waitPayOrder = _context.Orders.First(o => o.Timestamp == resParam["out_trade_no"].ToString());
                        // if (CheckOrder(waitPayOrder, resParam))
                        {
                            _orderService.OrderPay(resParam["out_trade_no"].ToString(), OrderPaymentType.中信);
                        }

                        await Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            flag = "1"
                        }));
                    }

                }
                _logger.LogWarning("中信验签错误");
            }
            //if (biqilinNotifyDto.orderStatus == "TRADE_SUCCESS")
            //{
            //    var query = _biqilinService.QueryOrder(biqilinNotifyDto.orderNo);
            //    var waitPayOrder = _context.Orders.First(o => o.Timestamp == biqilinNotifyDto.outOrderNo);
            //    if (CheckBiqilinOrder(waitPayOrder, query))
            //    {
            //        _orderService.OrderPay(biqilinNotifyDto.outOrderNo, OrderPaymentType.碧麒麟);
            //    }
            //}
            //await Response.WriteAsync(JsonConvert.SerializeObject(new
            //{
            //    flag = "1"
            //}));
        }
        //private bool CheckOrder(Order order, Hashtable resParam)
        //{
        //    if (order != null && resParam != null
        //        && query.orderStatus == "TRADE_SUCCESS"
        //        && query.outOrderNo == order.Timestamp
        //        && (decimal.Parse(query.amount)) == order.Amount)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

    }
}
