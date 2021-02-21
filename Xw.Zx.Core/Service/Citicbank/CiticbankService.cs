using swiftpass.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Helper;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    /// <summary>
    /// 中信支付平台
    /// 开发文档 https://open.swiftpass.cn/openapi 
    /// </summary>
    public class CiticbankService : ICiticbankService
    {
        private readonly XwZxContext _context;

        public CiticbankService(XwZxContext xwZxContext)
        {
            _context = xwZxContext;
        }

        public string GetMemberMchid(int memberid)
        {
            var mach = _context.CiticbankMchIds.FirstOrDefault(c => c.MemberId == memberid);
            if (mach == null)
            {
                //TODO 公共的商户号
                return "102576520476";
            }
            return mach.MchId;
        }


        public CiticbankDto.Qrcode CreateQrcodePayUrl(CiticbankDto.Product product)
        {
            var pay = new PayHttpClient();
            ClientResponseHandler resHandler = new ClientResponseHandler();
            RequestHandler reqHandler = new RequestHandler();

            var mchid = GetMemberMchid(product.MemberId);

            var cfg = Utils.loadCfg();
            //初始化数据 
            reqHandler.setGateUrl(cfg["req_url"].ToString());
            //reqHandler.setKey(this.cfg["key"].ToString());
            reqHandler.setParameter("out_trade_no", product.Timestamp);//商户订单号
            reqHandler.setParameter("body", product.Name);//商品描述
            reqHandler.setParameter("sign_type", "RSA_1_1");//签名方式
            reqHandler.setParameter("attach", product.Name);//附加信息
            reqHandler.setParameter("total_fee", Decimal.ToInt32((product.Amount * 100)).ToString());//总金额
            reqHandler.setParameter("mch_create_ip", "139.155.8.217");//终端IP
            reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss")); //订单生成时间
            reqHandler.setParameter("time_expire", DateTime.Now.AddHours(12).ToString("yyyyMMddHHmmss"));//订单超时时间
            reqHandler.setParameter("service", "unified.trade.native");//接口类型： 
            reqHandler.setParameter("mch_id", mchid); //102576520476
            reqHandler.setParameter("groupno", cfg["groupno"]);//必填项，商户号，由平台分配
            reqHandler.setParameter("version", "1.0");//接口版本号
            reqHandler.setParameter("notify_url", "http://jsq.lawss360.com/api/Citicbank/Notifyurl");//通知回调地址，商户需改为自己的，且保证外网能POST发送数据请求成功.
                                                                                                     //通知地址，必填项，接收平台通知的URL，需给绝对路径，255字符内;此URL要保证外网能访问   
            reqHandler.setParameter("nonce_str", Utils.random());//随机字符串，必填项，不长于 32 位
            reqHandler.createSign();//创建签名
                                    //以上参数进行签名
            string data = Utils.toXml(reqHandler.getAllParameters());//生成XML报文
            Dictionary<string, string> reqContent = new Dictionary<string, string>();
            reqContent.Add("url", reqHandler.getGateUrl());
            reqContent.Add("data", data);
            pay.setReqContent(reqContent);

            if (pay.call())
            {
                resHandler.setContent(pay.getResContent());
                //resHandler.setKey(this.cfg["key"].ToString());
                Hashtable param = resHandler.getAllParameters();
                if (int.Parse(param["status"].ToString()) == 0)
                {
                    if (resHandler.isTenpaySign())
                    {
                        //当返回状态与业务结果都为0时才返回支付二维码，其它结果请查看接口文档
                        if (int.Parse(param["status"].ToString()) == 0 && int.Parse(param["result_code"].ToString()) == 0)
                        {
                            AddCiticbankLog(new CiticbankLog()
                            {
                                Timestamp = product.Timestamp,
                                UUID = param["uuid"].ToString(),
                                MchId = mchid,
                            });
                            return new CiticbankDto.Qrcode()
                            {
                                CodeUrl = param["code_url"].ToString(),
                                uuid = param["uuid"].ToString(),
                            };
                        }
                        else
                        {
                            throw new Exception("错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"]);

                        }
                    }
                    else
                    {

                        throw new Exception("返回结果校验签名错误，请检查签名算法及核查结果内容是否正确！");
                    }
                }
                else
                {

                    throw new Exception("错误代码：" + param["status"] + ",错误信息：" + param["message"]);
                }
            }
            else
            {
                throw new Exception("错误代码：" + pay.getResponseCode() + ",错误信息：" + pay.getErrInfo());
            }
        }


        public string CreateJSApiPayInfo(CiticbankDto.Product product)
        {
            var pay = new PayHttpClient();
            ClientResponseHandler resHandler = new ClientResponseHandler();
            RequestHandler reqHandler = new RequestHandler();

            var mchid = GetMemberMchid(product.MemberId);

            var cfg = Utils.loadCfg();
            //初始化数据 
            reqHandler.setGateUrl(cfg["req_url"].ToString());
            //reqHandler.setKey(this.cfg["key"].ToString());
            reqHandler.setParameter("out_trade_no", product.Timestamp);//商户订单号
            reqHandler.setParameter("body", product.Name);//商品描述
            reqHandler.setParameter("sign_type", "RSA_1_1");//签名方式
            reqHandler.setParameter("attach", product.Name);//附加信息
            reqHandler.setParameter("total_fee", Decimal.ToInt32((product.Amount * 100)).ToString());//总金额
            reqHandler.setParameter("mch_create_ip", "139.155.8.217");//终端IP
            reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss")); //订单生成时间
            reqHandler.setParameter("time_expire", DateTime.Now.AddHours(12).ToString("yyyyMMddHHmmss"));//订单超时时间
            reqHandler.setParameter("service", "pay.weixin.jspay");//接口类型： 
            reqHandler.setParameter("mch_id", mchid); //102576520476
            reqHandler.setParameter("groupno", cfg["groupno"]);//必填项，商户号，由平台分配
            reqHandler.setParameter("version", "1.0");//接口版本号
            //TODO
            reqHandler.setParameter("notify_url", "http://www.baidu.cn/notify.aspx");//通知回调地址，商户需改为自己的，且保证外网能POST发送数据请求成功.
                                                                                     //通知地址，必填项，接收平台通知的URL，需给绝对路径，255字符内;此URL要保证外网能访问   
            reqHandler.setParameter("nonce_str", Utils.random());//随机字符串，必填项，不长于 32 位
            reqHandler.setParameter("is_raw", "1");//原生JS值
            reqHandler.setParameter("device_info", "苹果iPhone 6");//终端设备号
            //TODO
            reqHandler.setParameter("sub_openid", "o9N8qxPb2akHpob2N5qXOUvtXCkw");//测试账号不传值,此处默认给空值
            //TODO
            reqHandler.setParameter("sub_appid", "wx87734a5a656fc8cb");
            reqHandler.createSign();//创建签名
                                    //以上参数进行签名
            string data = Utils.toXml(reqHandler.getAllParameters());//生成XML报文
            Dictionary<string, string> reqContent = new Dictionary<string, string>();
            reqContent.Add("url", reqHandler.getGateUrl());
            reqContent.Add("data", data);
            pay.setReqContent(reqContent);

            if (pay.call())
            {
                resHandler.setContent(pay.getResContent());
                //resHandler.setKey(this.cfg["key"].ToString());
                Hashtable param = resHandler.getAllParameters();
                if (int.Parse(param["status"].ToString()) == 0)
                {
                    if (resHandler.isTenpaySign())
                    {
                        //当返回状态与业务结果都为0时才返回支付二维码，其它结果请查看接口文档
                        if (int.Parse(param["status"].ToString()) == 0 && int.Parse(param["result_code"].ToString()) == 0)
                        {
                            AddCiticbankLog(new CiticbankLog()
                            {
                                Timestamp = product.Timestamp,
                              //  UUID = param["uuid"].ToString(),
                                MchId = mchid,
                            });
                            return param["pay_info"].ToString();
                            //return new CiticbankDto.Qrcode()
                            //{
                            //    CodeUrl = param["code_url"].ToString(),
                            //    uuid = param["uuid"].ToString(),
                            //};
                        }
                        else
                        {
                            throw new Exception("错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"]);

                        }
                    }
                    else
                    {

                        throw new Exception("返回结果校验签名错误，请检查签名算法及核查结果内容是否正确！");
                    }
                }
                else
                {

                    throw new Exception("错误代码：" + param["status"] + ",错误信息：" + param["message"]);
                }
            }
            else
            {
                throw new Exception("错误代码：" + pay.getResponseCode() + ",错误信息：" + pay.getErrInfo());
            }
        }

        private string GetProductMchid(string timestamp)
        {
            var cl = _context.CiticbankLogs.FirstOrDefault(c => c.Timestamp == timestamp);
            if (cl == null)
            {
                return null;
            }
            return cl.MchId;
        }

        public bool Query(string timestamp)
        {
            var pay = new PayHttpClient();
            var reqHandler = new RequestHandler();
            var resHandler = new ClientResponseHandler();
            //加载配置数据
            var cfg = Utils.loadCfg();
            var mchid = GetProductMchid(timestamp);
            //初始化数据 
            reqHandler.setGateUrl(cfg["req_url"].ToString());
            //this.reqHandler.setKey(this.cfg["key"].ToString());
            reqHandler.setParameter("out_trade_no", timestamp);//商户订单号           
            reqHandler.setParameter("service", "unified.trade.query");//接口 unified.trade.query 
            reqHandler.setParameter("mch_id", mchid);//必填项，商户号，由平台分配
            reqHandler.setParameter("groupno", cfg["groupno"]);//必填项，商户号，由平台分配
            reqHandler.setParameter("version", "1.0");//接口版本号
            reqHandler.setParameter("sign_type", "RSA_1_1");//签名方式
            reqHandler.setParameter("nonce_str", Utils.random());//随机字符串，必填项，不长于 32 位
            reqHandler.createSign();//创建签名
                                    //以上参数进行签名
            string data = Utils.toXml(reqHandler.getAllParameters());//生成XML报文
            Dictionary<string, string> reqContent = new Dictionary<string, string>();
            reqContent.Add("url", reqHandler.getGateUrl());
            reqContent.Add("data", data);
            pay.setReqContent(reqContent);

            if (pay.call())
            {
                resHandler.setContent(pay.getResContent());
                //this.resHandler.setKey(this.cfg["key"].ToString());
                Hashtable param = resHandler.getAllParameters();
                if (resHandler.isTenpaySign())
                {
                    //当返回状态与业务结果都为0时才返回结果，其它结果请查看接口文档
                    if (int.Parse(param["status"].ToString()) == 0 && int.Parse(param["result_code"].ToString()) == 0)
                    {

                        return param["trade_state"].ToString() == "SUCCESS";

                    }
                }
                throw new Exception("错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"]);
            }

            throw new Exception("错误代码：" + pay.getResponseCode() + ",错误信息：" + pay.getErrInfo());
        }

        public void AddCiticbankLog(CiticbankLog citicbankLog)
        {
            if (string.IsNullOrWhiteSpace(citicbankLog.MchId))
            {
                throw new ZzzException("MchId不能为空");
            }
            if (string.IsNullOrWhiteSpace(citicbankLog.Timestamp))
            {
                throw new ZzzException("Timestamp不能为空");
            }

            var orderExists = _context.Orders.Any(o => o.Timestamp == citicbankLog.Timestamp && o.OrderState == OrderState.待付款);

            if (!orderExists)
            {
                throw new ZzzException($"订单{citicbankLog.Timestamp}为在待付款状态");
            }

            var isAny = _context.CiticbankLogs.Any(c => c.MchId == citicbankLog.MchId && c.Timestamp == citicbankLog.Timestamp && c.UUID == citicbankLog.UUID);
            if (!isAny)
            {
                _context.CiticbankLogs.Add(citicbankLog);
                _context.SaveChanges();
            }
        }

        public string[] GetAllCiticbankUnPayOrder()
        {
            var timestamps = from citicbanklog in _context.CiticbankLogs
                             join order in _context.Orders on citicbanklog.Timestamp equals order.Timestamp
                             where order.OrderState == OrderState.待付款
                             select citicbanklog.Timestamp;

            return timestamps.ToArray();
        }
    }
}
