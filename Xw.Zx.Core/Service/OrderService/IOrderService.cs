using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public interface IOrderService
    {
        /// <summary>
        /// 获取订单详情, 会检查订单状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Order GetOrder(int orderId);


        /// <summary>
        /// 查询支付宝和碧麒麟订单是否已支付, 如果支付则更新订单状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        void UpDateOrderPayState(int orderId);

    }
}
