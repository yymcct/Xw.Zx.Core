using Alipay.AopSdk.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Service
{
    public interface IAlipaySdkService
    {
        /// <summary>
        /// 查询支付宝订单
        /// </summary>
        /// <param name="tradeNo">商家自定义的订单号</param>
        /// <returns></returns>
        AlipayTradeQueryResponse Query(string tradeNo);
    }
}
