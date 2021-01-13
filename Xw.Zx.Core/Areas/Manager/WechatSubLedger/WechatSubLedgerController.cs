using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Xw.Zx.Core.PreWechatHelper;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service;
using static Sieve.Extensions.MethodInfoExtended;
using Microsoft.EntityFrameworkCore;

namespace Xw.Zx.Core.Areas.Manager
{
    [ApiController]
    [Route("manager/[controller]/[action]")]
    //[Authorize(Roles = "Admin")]
    public class WechatSubLedgerController : ManagerBaseController
    {
        private readonly ILogger<WithdrawDepositController> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        private string certpath = "";
        public WechatSubLedgerController(ILogger<WithdrawDepositController> logger
            , XwZxContext context
            , IMapper mapper
            , ISieveProcessor sieveProcessor) : base(context, mapper, sieveProcessor)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取分账接收人
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HbzsManagerResult<List<WechatSubLedgerReceivers>> GetWechatSubLedgerReceivers() {
            try {
               var result = _context.WechatSubLedgerReceivers.ToList(); 
               return new HbzsManagerResult<List<WechatSubLedgerReceivers>>(result);
            }
            catch (Exception ex)
            {
                return new HbzsManagerResult<List<WechatSubLedgerReceivers>>(HbzsManagerResultCode.Invalid_Error,ex.Message);
            }
        }

        /// <summary>
        ///获取微信支付订单
        /// </summary>
        /// <param name="ordernum"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsManagerResult<WechatOrders> GetWechatPayOrder([FromQuery] WechatQueryOrderPsDto ordernums)
        {
            try
            {
                string tranid = "";
                string outorderno = "";

                if (ordernums == null || (string.IsNullOrWhiteSpace(ordernums.out_order_no) && string.IsNullOrWhiteSpace(ordernums.transaction_id)))
                    throw new Exception("请输入微信订单号或商户订单号");
                if (!string.IsNullOrWhiteSpace(ordernums.transaction_id))
                {
                    tranid = ordernums.transaction_id;
                    List<WechatOrders> dd = _context.WechatOrders.Where<WechatOrders>(c => c.TransactionID == tranid).ToList<WechatOrders>();
                    if (dd != null && dd.Count > 0)
                        return new HbzsManagerResult<WechatOrders>(dd[0]);
                }
                if (!string.IsNullOrWhiteSpace(ordernums.out_order_no))
                {
                    outorderno = ordernums.out_order_no;
                    List<WechatOrders> dd = _context.WechatOrders.Where<WechatOrders>(c => c.Out_Order_No == outorderno).ToList<WechatOrders>();
                    if (dd != null && dd.Count > 0)
                        return new HbzsManagerResult<WechatOrders>(dd[0]);
                }

                WxPayData data = new WxPayData();
                //优先
                if (!string.IsNullOrEmpty(tranid))
                {
                    data.SetValue("transaction_id", tranid);
                }
                else
                {
                    data.SetValue("out_trade_no", outorderno);
                }

                WxPayData result = WxPayApi.OrderQuery(data);//提交订单查询请求给API，接收返回数据
                if (result == null)
                    throw new Exception("未查询到订单信息");
                if (result.GetValue("result_code").ToString().ToUpper() != "SUCCESS")                
                    throw new Exception(result.GetValue("return_msg").ToString());
                if (result.GetValue("result_code").ToString().ToUpper() != "SUCCESS")
                        throw new Exception(result.GetValue("err_code_des").ToString()); 

                WechatOrders od = new WechatOrders();
                od.Amount = decimal.Parse(result.GetValue("total_fee").ToString())/100;
                od.Out_Order_No = result.GetValue("out_trade_no").ToString();
                od.TransactionID = result.GetValue("transaction_id").ToString();
                od.PayState = result.GetValue("trade_state").ToString();
                od.PayDescription = result.GetValue("trade_state_desc").ToString();

                string tranovertime = result.GetValue("time_end").ToString();
                DateTime dt = DateTime.ParseExact(tranovertime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                od.TranTime = dt;
                od.SubCharge =Math.Floor(od.Amount * (1-WxPayConfig.Config.Rate)*WxPayConfig.Config.MaxRate*100)/100;

                _context.WechatOrders.Add(od);
                _context.SaveChanges();
                return new HbzsManagerResult<WechatOrders>(_context.WechatOrders.Where<WechatOrders>(c => c.TransactionID == od.TransactionID).FirstOrDefault());                
            }
            catch (Exception ex)
            {
                return new HbzsManagerResult<WechatOrders>(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// 处理分账
        /// </summary>
        /// <param name="postRequestDealWithSubLedgerApiPsDto"></param>
        /// <returns></returns>
        [HttpPost]
        public HbzsManagerResult<string> DealWithSubLedger([FromBody] PostRequestDealWithSubLedgerApiPsDto postRequestDealWithSubLedgerApiPsDto) {
            try
            {
                List<WechatSubLedgerRequestReceivePsDto> receivers = CheckSubLederRequestPs(postRequestDealWithSubLedgerApiPsDto);
                //拼接 信息
                WxPayData data = new WxPayData();
                data.SetValue("transaction_id", postRequestDealWithSubLedgerApiPsDto.transaction_id);
                data.SetValue("out_order_no", postRequestDealWithSubLedgerApiPsDto.out_order_no);
                List<WxDataSubLedgerReceivePsDto> receiversInfo = new List<WxDataSubLedgerReceivePsDto>();
                foreach (WechatSubLedgerRequestReceivePsDto r in receivers) {
                    WxDataSubLedgerReceivePsDto dd = new WxDataSubLedgerReceivePsDto();
                    dd.account = r.account;
                    dd.amount = (int)(r.amount * 100);
                    dd.description = r.description;
                    dd.type = r.type;
                    receiversInfo.Add(dd);
                }
                data.SetValue("receivers",JsonConvert.SerializeObject(receiversInfo));

                #region 调用分账
                /*----------------调用接口-------------------------*/
                WxPayData result = WxPayApi.DealWithSubLedgerSigle(data);
                //WxPayData result = new WxPayData();

                if (result == null)
                    throw new Exception("获取分账结果异常");
                if (result.GetValue("result_code").ToString().ToUpper() != "SUCCESS")
                    throw new Exception(result.GetValue("return_msg").ToString());
                if (result.GetValue("result_code").ToString().ToUpper() != "SUCCESS")
                    throw new Exception(result.GetValue("err_code_des").ToString());

                //获取分账单号
                string fzdh = result.GetValue("order_id").ToString();
                foreach (WechatSubLedgerRequestReceivePsDto r in receivers)
                {
                    WechatSubDetail rr = new WechatSubDetail();
                    rr.Last_Out_Order_No = postRequestDealWithSubLedgerApiPsDto.out_order_no;
                    rr.TransactionID = postRequestDealWithSubLedgerApiPsDto.transaction_id;
                    rr.Return_OrderID = fzdh;
                    rr.SubAccount = r.account;
                    rr.SubAmount = (decimal)r.amount / 100;//分 需要 变成元
                    rr.SubName = _context.WechatSubLedgerReceivers.FirstOrDefault(c => c.Account == r.account).Name;
                    rr.PayDescription = r.description;
                    rr.SubType = r.type;
                    rr.SubState = "";
                    rr.SubTime = DateTime.Now;
                    _context.WechatSubDetail.Add(rr);
                }
                _context.SaveChanges();
                #endregion

                #region 查询分账结果
                WxPayData dataquery = new WxPayData();
                data.SetValue("transaction_id", postRequestDealWithSubLedgerApiPsDto.transaction_id);
                data.SetValue("out_order_no", postRequestDealWithSubLedgerApiPsDto.out_order_no);
                /*----------------调用接口-------------------------*/
                WxPayData queryresult = WxPayApi.ProfitSharingQuery(dataquery);
                if (queryresult == null)
                    throw new Exception("分账已申请通过，未查到分账结果");
                if(queryresult.GetValue("return_code").ToString().ToUpper() != "SUCCESS")
                    throw new Exception(result.GetValue("return_msg").ToString());
                if (result.GetValue("result_code").ToString().ToUpper() != "SUCCESS")
                    throw new Exception(result.GetValue("err_code_des").ToString());

                //分账申请接收成功
                string receiversStr = queryresult.GetValue("receivers").ToString();
                List<WechatSubLedgerQueryResultReceiverPsDto> ps = JsonConvert.DeserializeObject<List<WechatSubLedgerQueryResultReceiverPsDto>>(receiversStr);
                foreach (var r in ps) {
                    WechatSubDetail rr = _context.WechatSubDetail
                        .FirstOrDefault(c=>c.Last_Out_Order_No== postRequestDealWithSubLedgerApiPsDto.out_order_no && c.TransactionID== postRequestDealWithSubLedgerApiPsDto.transaction_id && c.SubAccount==r.account);
                    rr.SubState = r.result;
                    rr.SubTime= DateTime.ParseExact(r.finish_time, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                    _context.Entry(rr).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                #endregion

                return new HbzsManagerResult<string>(HbzsManagerResultCode.Sucess, "已处理");
            }
            catch (Exception ex) {
                return new HbzsManagerResult<string>(HbzsManagerResultCode.Invalid_Error,ex.Message);
            }
        }

        List<WechatSubLedgerRequestReceivePsDto> CheckSubLederRequestPs(PostRequestDealWithSubLedgerApiPsDto ps) {
            if (ps == null)
                throw new Exception("请求参数 无效");
            if (string.IsNullOrWhiteSpace(ps.transaction_id)
                || string.IsNullOrWhiteSpace(ps.out_order_no))
                throw new Exception("微信订单号 或 商户分账单号 不能为空");
            string tranid = ps.transaction_id;
            string outorderid = ps.out_order_no;

            if (ps.SubLedgerListInfo == null
                || ps.SubLedgerListInfo.Count <= 0)
                throw new Exception("接收人分账信息无效");

            List<WechatSubLedgerRequestReceivePsDto> receiverPs = new List<WechatSubLedgerRequestReceivePsDto>();
            foreach (PostWechatSubLedgerListInfoDto r in ps.SubLedgerListInfo)
            {
                if (string.IsNullOrWhiteSpace(r.SubType) || string.IsNullOrWhiteSpace(r.Account)
                    || r.Amount <= 0 || string.IsNullOrWhiteSpace(r.Describe))
                    continue;
                WechatSubLedgerRequestReceivePsDto rr = new WechatSubLedgerRequestReceivePsDto();
                rr.account = r.Account;
                rr.amount = r.Amount;
                rr.description = r.Describe;
                rr.type = r.SubType;
                receiverPs.Add(rr);
            }
            if (receiverPs.Count <= 0)
                throw new Exception("分账人信息 有误");
            return receiverPs;
        }

        public ActionResult PayNotifyUrl() {
            return null;//不用
        }


        #region  完结分账 这里不用 先不删（这里用单次分账 分账后 就结账）
        ///// <summary>
        ///// 完结分账 Over 把还没分完的 有剩余的 钱解冻
        ///// 
        ///// </summary>
        ///// <param name="postProfitsharingFinishDto"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public HbzsManagerResult<string> ProfitsharingFinish([FromBody] PostProfitsharingFinishDto postProfitsharingFinishDto) {
        //    try
        //    {
        //        if (postProfitsharingFinishDto == null) {
        //            throw new Exception("请输入完结分账 请求参数！");
        //        }
        //        if(!string.IsNullOrWhiteSpace(postProfitsharingFinishDto.transaction_id))
        //            throw new Exception("交易订单号不能为空！");
        //        if (!string.IsNullOrWhiteSpace(postProfitsharingFinishDto.last_out_order_no))
        //            throw new Exception("商户分账单号不能为空！");
        //        if (!string.IsNullOrWhiteSpace(postProfitsharingFinishDto.description))
        //            throw new Exception("分账完结描述不能不空！");
        //        //获取最后的 out_order_no
        //        WxPayData data = new WxPayData();
        //        data.SetValue("transaction_id", postProfitsharingFinishDto.transaction_id);
        //        data.SetValue("out_order_no", postProfitsharingFinishDto.last_out_order_no);
        //        data.SetValue("description", postProfitsharingFinishDto.description);

        //        WxPayApi.ProfitsharingFinish(data);

        //        return new HbzsManagerResult<string>("处理完成！");

        //    } catch (Exception ex) {
        //        return new HbzsManagerResult<string>(HbzsManagerResultCode.Invalid_Error,ex.Message);
        //    }
        //}
        #endregion
    }
}
