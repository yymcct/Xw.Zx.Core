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
    public class AlipayController : BaseController
    {
        private readonly ILogger<AlipayController> _logger;
        private readonly AlipayService _alipayService;
        private readonly IUpDateVip1Service _upDateVip1Service;
        public AlipayController(ILogger<AlipayController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor
            , AlipayService alipayService
            , IUpDateVip1Service upDateVip1Service) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
            _alipayService = alipayService;
            _upDateVip1Service = upDateVip1Service;
        }

        /// <summary>
        /// 获取升级VIP1订单信息, 用户支付宝支付
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public HbzsResult<AliPayOrderDto> GetUpdateVip1Order()
        {
            try
            {               
                return new HbzsResult<AliPayOrderDto>(_upDateVip1Service.CreateAliPayOrder(Member));
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

        /// <summary>
        /// 模拟支付宝
        /// </summary>
        [HttpPost]
        public async void NotifyurlTest()
        {

            throw new Exception("已停用");
            Dictionary<string, string> sArray = GetRequestPost();
            _upDateVip1Service.AliPayMentSucessHandle(sArray);

            await Response.WriteAsync("success");
        }


        private void LogsArray(Dictionary<string, string> dict)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var kv in dict)//如果是.NET2.0 var换成KeyValuePair<string,string>
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

    }
}