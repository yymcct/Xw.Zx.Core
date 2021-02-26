using Sieve.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        void OrderPay(string timestamp, OrderPaymentType paymentType);

        void OrderPay(string timestamp, OrderPaymentType paymentType, string outOrderNo);

        void CouponOrderPay(int memberId, int orderId, int couponreceiveId);

        void MemberIntegralPay(int memberId, int orderId);

        MemoryStream ExportToExcel(SieveModel sieveModel);
    }
}
