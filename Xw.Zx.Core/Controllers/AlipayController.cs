using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alipay.AopSdk.AspnetCore;
using Alipay.AopSdk.Core.Domain;
using Alipay.AopSdk.Core.Request;
using Alipay.AopSdk.Core.Response;
using AutoMapper;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sieve.Services;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlipayController : BaseController
    {
        private readonly ILogger<AlipayController> _logger;
        private readonly AlipayService _alipayService;
        public AlipayController(ILogger<AlipayController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor
            , AlipayService alipayService) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
            _alipayService = alipayService;
        }
        [HttpGet]
        public string GetPayInfo()//_amount:付款金额  
        {
            AlipayTradeAppPayModel model = new AlipayTradeAppPayModel();
            model.Body = "我是测试数据";
            model.Subject = "App支付测试DoNet";
            model.TotalAmount = "0.01";
            model.ProductCode = "QUICK_MSECURITY_PAY";
            model.OutTradeNo = "20170216555555555555555501";
            model.TimeoutExpress = "30m";



            AlipayTradeAppPayRequest request = new AlipayTradeAppPayRequest();
            request.SetNotifyUrl("http://");
            request.SetBizModel(model);

            AlipayTradeAppPayResponse response = _alipayService.SdkExecute(request);
            return response.Body;
        }

        [HttpPost]
        public async void Notifyurl()
        {
            /* 实际验证过程建议商户添加以下校验。
               1、商户需要验证该通知数据中的out_trade_no是否为商户系统中创建的订单号，
               2、判断total_amount是否确实为该订单的实际金额（即商户订单创建时的金额），
               3、校验通知中的seller_id（或者seller_email) 是否为out_trade_no这笔单据的对应的操作方（有的时候，一个商户可能有多个seller_id/seller_email）
               4、验证app_id是否为该商户本身。
               */

            Dictionary<string, string> sArray = GetRequestPost();
            if (sArray.Count != 0)
            {
                bool flag = _alipayService.RSACheckV1(sArray);
                if (flag)
                {
                    //交易状态
                    //判断该笔订单是否在商户网站中已经做过处理
                    //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                    //请务必判断请求时的total_amount与通知时获取的total_fee为一致的
                    //如果有做过处理，不执行商户的业务程序

                    //注意：
                    //退款日期超过可退款期限后（如三个月可退款），支付宝系统发送该交易状态通知
                    Console.WriteLine(Request.Form["trade_status"]);

                    await Response.WriteAsync("success");
                }
                else
                {
                    await Response.WriteAsync("fail");
                }
            }
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

    }
}