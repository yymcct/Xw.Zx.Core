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

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlipayController : BaseController
    {
        private readonly ILogger<AlipayController> _logger;
        private readonly AlipayService _alipayService;
        private readonly AlipayF2FService _alipayF2FService;
        private readonly IUpDateVip1Service _upDateVip1Service;
        private readonly IWapOrderPayService _wapOrderPayService;
        public AlipayController(ILogger<AlipayController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor
            , AlipayService alipayService
            , AlipayF2FService alipayF2FService
            , IUpDateVip1Service upDateVip1Service
            , IWapOrderPayService wapOrderPayService) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
            _alipayService = alipayService;
            _alipayF2FService = alipayF2FService;
            _upDateVip1Service = upDateVip1Service;
            _wapOrderPayService = wapOrderPayService;
        }

        /// <summary>
        /// 获取升级VIP1订单信息, 用户支付宝支付
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public HbzsResult<AliPayOrderDto> GetUpdateVip1Order(MemberVipType toVipType = MemberVipType.Vip会员)
        {
            try
            {
                return new HbzsResult<AliPayOrderDto>(_upDateVip1Service.CreateAliPayOrder(Member, toVipType));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsResult<AliPayOrderDto>(HbzsResultCode.Invalid_Error, ex.Message);
            }

        }

        [HttpPost("{orderId}")]
        [Authorize]
        public HbzsResult<AliPayOrderDto> WapPay(int orderId, [FromQuery] string returnUrl)
        {
            try
            {
                var order = _context.Orders.First(o => o.MemberId == Member.Id
                                                && o.OrderState == OrderState.待付款
                                                && o.Id == orderId);
                AlipayTradeWapPayModel model = new AlipayTradeWapPayModel()
                {
                    Body = order.ProducName,
                    Subject = order.ProducName,
                    TotalAmount = order.Amount.ToString("F2"),
                    ProductCode = "QUICK_WAP_PAY",
                    OutTradeNo = order.Timestamp,
                    TimeoutExpress = "50m",
                    GoodsType = "0"

                };

                AlipayTradeWapPayRequest request = new AlipayTradeWapPayRequest();
                request.SetNotifyUrl("http://139.155.8.217/api/Alipay/WapNotifyurl");
                request.SetReturnUrl(returnUrl);
                request.SetBizModel(model);

                var response = _alipayService.PageExecute(request);  // _alipayService.SdkExecute(request);

                return new HbzsResult<AliPayOrderDto>(new AliPayOrderDto()
                {
                    ProductName = order.ProducName,
                    ProductPrice = order.Amount.ToString("n"),
                    AlipayTradeAppPayResponse = response.Body
                });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsResult<AliPayOrderDto>(HbzsResultCode.Invalid_Error, ex.Message);
            }

        }
        /// <summary>
        /// 支付宝支付成功回调地址 
        /// TODO: 校验是否是来自于支付宝的域名
        /// </summary>
        [HttpPost]
        public async void Notifyurl()
        {
            /* 实际验证过程建议商户添加以下校验。
               1、商户需要验证该通知数据中的out_trade_no是否为商户系统中创建的订单号，
               2、判断total_amount是否确实为该订单的实际金额（即商户订单创建时的金额），
               3、校验通知中的seller_id（或者seller_email) 是否为out_trade_no这笔单据的对应的操作方（有的时候，一个商户可能有多个seller_id/seller_email）
               4、验证app_id是否为该商户本身。
               */
            _logger.LogWarning("GetUpdateVip1Order");
            Dictionary<string, string> sArray = GetRequestPost();

            LogsArray(sArray);
            if (sArray.Count != 0)
            {
                bool flag = _alipayService.RSACheckV1(sArray);
                if (flag)
                {   //https://docs.open.alipay.com/204/105301/
                    //交易状态
                    //判断该笔订单是否在商户网站中已经做过处理
                    //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                    //请务必判断请求时的total_amount与通知时获取的total_fee为一致的
                    //如果有做过处理，不执行商户的业务程序

                    //注意：
                    //退款日期超过可退款期限后（如三个月可退款），支付宝系统发送该交易状态通知
                    Console.WriteLine(Request.Form["trade_status"]);

                    if (sArray["trade_status"] == "TRADE_SUCCESS")
                    {
                        _upDateVip1Service.AliPayMentSucessHandle(sArray);
                    }
                }

                await Response.WriteAsync("success");
            }
            else
            {
                await Response.WriteAsync("fail");
            }
        }
        [HttpPost]
        public async void WapNotifyurl()
        {
            /* 实际验证过程建议商户添加以下校验。
               1、商户需要验证该通知数据中的out_trade_no是否为商户系统中创建的订单号，
               2、判断total_amount是否确实为该订单的实际金额（即商户订单创建时的金额），
               3、校验通知中的seller_id（或者seller_email) 是否为out_trade_no这笔单据的对应的操作方（有的时候，一个商户可能有多个seller_id/seller_email）
               4、验证app_id是否为该商户本身。
               */
            try
            {
                Dictionary<string, string> sArray = GetRequestPost();
                //LogsArray(sArray);
                if (sArray.Count != 0)
                {
                    bool flag = _alipayService.RSACheckV1(sArray);
                    if (flag)
                    {   //https://docs.open.alipay.com/204/105301/
                        //交易状态
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //请务必判断请求时的total_amount与通知时获取的total_fee为一致的
                        //如果有做过处理，不执行商户的业务程序

                        //注意：
                        //退款日期超过可退款期限后（如三个月可退款），支付宝系统发送该交易状态通知
                        if (sArray["trade_status"] == "TRADE_SUCCESS")
                        {

                            _wapOrderPayService.SucessHandle(sArray);
                        }
                    }

                    await Response.WriteAsync("success");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await Response.WriteAsync("fail");
            }

        }




        private void LogsArray(Dictionary<string, string> dict)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var kv in dict)
            {
                sb.AppendFormat("{0}:{1};", kv.Key, kv.Value);
            }
            string str = sb.ToString();

            _logger.LogWarning(str);
        }



        [HttpGet]
        public string WithdrawDeposit()
        {

            AlipayFundTransToaccountTransferRequest request = new AlipayFundTransToaccountTransferRequest();

            AlipayFundTransToaccountTransferResponse response = _alipayService.Execute(request);


            Console.WriteLine(response.Body);
            return response.Body;
        }


        #region 解析请求参数

        private Dictionary<string, string> GetRequestGet()
        {
            Dictionary<string, string> sArray = new Dictionary<string, string>();

            ICollection<string> requestItem = Request.Query.Keys;
            foreach (var item in requestItem)
            {
                sArray.Add(item, Request.Query[item]);

            }
            return sArray;

        }

        private Dictionary<string, string> GetRequestPost()
        {
            Dictionary<string, string> sArray = new Dictionary<string, string>();

            ICollection<string> requestItem = Request.Form.Keys;
            foreach (var item in requestItem)
            {
                sArray.Add(item, Request.Form[item]);

            }
            return sArray;

        }

        #endregion

        #region 当面付
        [HttpPost("{orderId}")]
        [Authorize]
        public async Task<HbzsResult<string>> ScanCodeGen(int orderId)
        {

            AlipayTradePrecreateContentBuilder builder = BuildPrecreateContent();

            //如果需要接收扫码支付异步通知，那么请把下面两行注释代替本行。
            //推荐使用轮询撤销机制，不推荐使用异步通知,避免单边账问题发生。
            //AlipayF2FPrecreateResult precreateResult = await _alipayF2FService.TradePrecreateAsync(builder);
            string notify_url = "http://139.155.8.217/api/Alipay/WapNotifyurl";  //商户接收异步通知的地址
            AlipayF2FPrecreateResult precreateResult = await _alipayF2FService.TradePrecreateAsync(builder, notify_url);

            //以下返回结果的处理供参考。
            //payResponse.QrCode即二维码对于的链接
            //将链接用二维码工具生成二维码打印出来，顾客可以用支付宝钱包扫码支付。

            switch (precreateResult.Status)
            {
                case ResultEnum.SUCCESS:

                    return new HbzsResult<string>(precreateResult.response.QrCode);

                case ResultEnum.FAILED:
                    return new HbzsResult<string>(HbzsResultCode.Invalid_Error, precreateResult.response.Body);

                case ResultEnum.UNKNOWN:
                default:
                    return new HbzsResult<string>(HbzsResultCode.Invalid_Error, "生成二维码失败：" +
                        (precreateResult.response == null ? "配置或网络异常，请检查后重试" : "系统异常，请更新外部订单后重新发起请求"));
            }

            AlipayTradePrecreateContentBuilder BuildPrecreateContent()
            {
                var order = _context.Orders.First(o => o.MemberId == Member.Id
                                                                && o.OrderState == OrderState.待付款
                                                                && o.Id == orderId);

                var alipayTradePrecreateContentBuilder = new AlipayTradePrecreateContentBuilder();
                //收款账号
                alipayTradePrecreateContentBuilder.seller_id = _alipayService.Options.Uid;
                //订单编号
                alipayTradePrecreateContentBuilder.out_trade_no = order.Timestamp;
                //订单总金额
                alipayTradePrecreateContentBuilder.total_amount = order.Amount.ToString("F2");
                //参与优惠计算的金额
                //builder.discountable_amount = "";
                //不参与优惠计算的金额
                //builder.undiscountable_amount = "";
                //订单名称
                alipayTradePrecreateContentBuilder.subject = order.ProducName;
                //自定义超时时间
                alipayTradePrecreateContentBuilder.timeout_express = "90m";
                //订单描述
                alipayTradePrecreateContentBuilder.body = order.ProducName;
                //门店编号，很重要的参数，可以用作之后的营销
                alipayTradePrecreateContentBuilder.store_id = "test store id";
                //操作员编号，很重要的参数，可以用作之后的营销
                alipayTradePrecreateContentBuilder.operator_id = Member.Id.ToString();

                //系统商接入可以填此参数用作返佣
                //ExtendParams exParam = new ExtendParams();
                //exParam.sysServiceProviderId = "20880000000000";
                //builder.extendParams = exParam;

                return alipayTradePrecreateContentBuilder;

            }

        }


        #endregion
    }
}