using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.PreWechatHelper;

namespace Xw.Zx.Core.PreWechatHelper
{

    public class WxPayApi
    {

        /// <summary>
        /// 单次 分账
        /// </summary>
        /// <param name="inputObj"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxPayData DealWithSubLedgerSigle(WxPayData inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/secapi/pay/profitsharing";

            //检测必填参数
            if (!inputObj.IsSet("out_order_no") || !inputObj.IsSet("transaction_id") || !inputObj.IsSet("receivers"))
            {
                throw new WxPayException("单次分账完结接口中，交易订单号，商户分账单号,接收人 不能为空！");
            }

            inputObj.SetValue("appid", WxPayConfig.Config.Appid);//公众账号ID
            inputObj.SetValue("mch_id", WxPayConfig.Config.Mchid);//商户号
            inputObj.SetValue("nonce_str", WxPayApi.GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign_type", WxPayData.SIGN_TYPE_HMAC_SHA256);//签名类型
            inputObj.SetValue("sign", inputObj.MakeSign());//签名

            string xml = inputObj.ToXml();

            var start = DateTime.Now;

            //Log.Debug("WxPayApi", "OrderQuery request : " + xml);
            string response = HttpService.Post(xml, url, true, timeOut);//调用HTTP通信接口提交数据
            //Log.Debug("WxPayApi", "OrderQuery response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);//获得接口耗时

            //将xml格式的数据转化为对象以返回
            WxPayData result = new WxPayData();
            result.FromXml(response);

            //ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }

        /// <summary>
        /// 单次 分账查询结果
        /// </summary>
        /// <param name="inputObj"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxPayData ProfitSharingQuery(WxPayData inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/profitsharingquery";

            //检测必填参数
            if (!inputObj.IsSet("out_order_no") || !inputObj.IsSet("transaction_id") || !inputObj.IsSet("receivers"))
            {
                throw new WxPayException("单次分账完结接口中，交易订单号，商户分账单号,接收人 不能为空！");
            }

            inputObj.SetValue("appid", WxPayConfig.Config.Appid);//公众账号ID
            inputObj.SetValue("mch_id", WxPayConfig.Config.Mchid);//商户号
            inputObj.SetValue("nonce_str", WxPayApi.GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign_type", WxPayData.SIGN_TYPE_HMAC_SHA256);//签名类型
            inputObj.SetValue("sign", inputObj.MakeSign());//签名

            string xml = inputObj.ToXml();

            var start = DateTime.Now;

            //Log.Debug("WxPayApi", "OrderQuery request : " + xml);
            string response = HttpService.Post(xml, url, false, timeOut);//调用HTTP通信接口提交数据
            //Log.Debug("WxPayApi", "OrderQuery response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);//获得接口耗时

            //将xml格式的数据转化为对象以返回
            WxPayData result = new WxPayData();
            result.FromXml(response);

            //ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }

        /// <summary>
        /// 完结分账  这里暂时不用
        /// </summary>
        /// <param name="inputObj"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxPayData ProfitsharingFinish(WxPayData inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/secapi/pay/profitsharingfinish";

            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") || !inputObj.IsSet("transaction_id") || !inputObj.IsSet("description"))
            {
                throw new WxPayException("分账完结接口中，交易订单号，商户分账单号,完结描述不能为空！");
            }

            inputObj.SetValue("appid", WxPayConfig.Config.Appid);//公众账号ID
            inputObj.SetValue("mch_id", WxPayConfig.Config.Mchid);//商户号
            inputObj.SetValue("nonce_str", WxPayApi.GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign_type", WxPayData.SIGN_TYPE_HMAC_SHA256);//签名类型
            inputObj.SetValue("sign", inputObj.MakeSign());//签名

            string xml = inputObj.ToXml();

            var start = DateTime.Now;

            //Log.Debug("WxPayApi", "OrderQuery request : " + xml);
            string response = HttpService.Post(xml, url,true, timeOut);//调用HTTP通信接口提交数据
            //Log.Debug("WxPayApi", "OrderQuery response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);//获得接口耗时

            //将xml格式的数据转化为对象以返回
            WxPayData result = new WxPayData();
            result.FromXml(response);

            //ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }        

        /**
        *    
        * 查询订单
        * @param WxPayData inputObj 提交给查询订单API的参数
        * @param int timeOut 超时时间
        * @throws WxPayException
        * @return 成功时返回订单查询结果，其他抛异常
        */
        public static WxPayData OrderQuery(WxPayData inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/orderquery";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new WxPayException("订单查询接口中，out_trade_no、transaction_id至少填一个！");
            }

            inputObj.SetValue("appid", WxPayConfig.Config.Appid);//公众账号ID
            inputObj.SetValue("mch_id", WxPayConfig.Config.Mchid);//商户号
            inputObj.SetValue("nonce_str", WxPayApi.GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign_type", WxPayData.SIGN_TYPE_HMAC_SHA256);//签名类型
            inputObj.SetValue("sign", inputObj.MakeSign());//签名


            string xml = inputObj.ToXml();

            var start = DateTime.Now;

            //Log.Debug("WxPayApi", "OrderQuery request : " + xml);
            string response = HttpService.Post(xml, url, false, timeOut);//调用HTTP通信接口提交数据
            //Log.Debug("WxPayApi", "OrderQuery response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);//获得接口耗时

            //将xml格式的数据转化为对象以返回
            WxPayData result = new WxPayData();
            result.FromXml(response);

            ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }


        /**
	    * 
	    * 测速上报
	    * @param string interface_url 接口URL
	    * @param int timeCost 接口耗时
	    * @param WxPayData inputObj参数数组
	    */
        private static void ReportCostTime(string interface_url, int timeCost, WxPayData inputObj)
        {
            //如果不需要进行上报
            return;
            //if (WxPayConfig.GetConfig().GetReportLevel() == 0)
            //{
            //    return;
            //}

            ////如果仅失败上报
            //if (WxPayConfig.GetConfig().GetReportLevel() == 1 && inputObj.IsSet("return_code") && inputObj.GetValue("return_code").ToString() == "SUCCESS" &&
            // inputObj.IsSet("result_code") && inputObj.GetValue("result_code").ToString() == "SUCCESS")
            //{
            //    return;
            //}

            //上报逻辑
            WxPayData data = new WxPayData();
            data.SetValue("interface_url", interface_url);
            data.SetValue("execute_time_", timeCost);
            //返回状态码
            if (inputObj.IsSet("return_code"))
            {
                data.SetValue("return_code", inputObj.GetValue("return_code"));
            }
            //返回信息
            if (inputObj.IsSet("return_msg"))
            {
                data.SetValue("return_msg", inputObj.GetValue("return_msg"));
            }
            //业务结果
            if (inputObj.IsSet("result_code"))
            {
                data.SetValue("result_code", inputObj.GetValue("result_code"));
            }
            //错误代码
            if (inputObj.IsSet("err_code"))
            {
                data.SetValue("err_code", inputObj.GetValue("err_code"));
            }
            //错误代码描述
            if (inputObj.IsSet("err_code_des"))
            {
                data.SetValue("err_code_des", inputObj.GetValue("err_code_des"));
            }
            //商户订单号
            if (inputObj.IsSet("out_trade_no"))
            {
                data.SetValue("out_trade_no", inputObj.GetValue("out_trade_no"));
            }
            //设备号
            if (inputObj.IsSet("device_info"))
            {
                data.SetValue("device_info", inputObj.GetValue("device_info"));
            }

            try
            {
                Report(data);
            }
            catch (WxPayException ex)
            {
                //不做任何处理
            }
        }


        /**
	    * 
	    * 测速上报接口实现
	    * @param WxPayData inputObj 提交给测速上报接口的参数
	    * @param int timeOut 测速上报接口超时时间
	    * @throws WxPayException
	    * @return 成功时返回测速上报接口返回的结果，其他抛异常
	    */
        public static WxPayData Report(WxPayData inputObj, int timeOut = 1)
        {
            string url = "https://api.mch.weixin.qq.com/payitil/report";
            //检测必填参数
            if (!inputObj.IsSet("interface_url"))
            {
                throw new WxPayException("接口URL，缺少必填参数interface_url！");
            }
            if (!inputObj.IsSet("return_code"))
            {
                throw new WxPayException("返回状态码，缺少必填参数return_code！");
            }
            if (!inputObj.IsSet("result_code"))
            {
                throw new WxPayException("业务结果，缺少必填参数result_code！");
            }
            if (!inputObj.IsSet("user_ip"))
            {
                throw new WxPayException("访问接口IP，缺少必填参数user_ip！");
            }
            if (!inputObj.IsSet("execute_time_"))
            {
                throw new WxPayException("接口耗时，缺少必填参数execute_time_！");
            }

            inputObj.SetValue("appid", WxPayConfig.Config.Appid);//公众账号ID
            inputObj.SetValue("mch_id", WxPayConfig.Config.Mchid);//商户号
            inputObj.SetValue("user_ip", "0.0.0.0");//终端ip
            inputObj.SetValue("time", DateTime.Now.ToString("yyyyMMddHHmmss"));//商户上报时间	 
            inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign_type", WxPayData.SIGN_TYPE_HMAC_SHA256);//签名类型
            inputObj.SetValue("sign", inputObj.MakeSign());//签名
            string xml = inputObj.ToXml();

            //Log.Info("WxPayApi", "Report request : " + xml);

            string response = HttpService.Post(xml, url, false, timeOut);

            //Log.Info("WxPayApi", "Report response : " + response);

            WxPayData result = new WxPayData();
            result.FromXml(response);
            return result;
        }


        /**
        * 根据当前系统时间加随机序列来生成订单号
         * @return 订单号
        */
        public static string GenerateOutTradeNo()
        {
            var ran = new Random();
            return string.Format("{0}{1}{2}", WxPayConfig.Config.Mchid, DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
        }

        /**
        * 生成时间戳，标准北京时间，时区为东八区，自1970年1月1日 0点0分0秒以来的秒数
         * @return 时间戳
        */
        public static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /**
        * 生成随机串，随机串包含字母或数字
        * @return 随机串
        */
        public static string GenerateNonceStr()
        {
            RandomGenerator randomGenerator = new RandomGenerator();
            return randomGenerator.GetRandomUInt().ToString();
        }
    }
}
