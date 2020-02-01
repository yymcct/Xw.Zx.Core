using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public interface IUpDateVip1Service
    {
        // 生成支付宝订单 
        AliPayOrderDto CreateAliPayOrder(Member member, MemberVipType toVipTyp);

        // 生成支付宝订单
        AliPayOrderDto CreateH5AliPayOrder(Member member, MemberVipType toVipTyp);




        /// <summary>
        /// 支付宝支付成功处理
        /// </summary>
        /// <param name="sArray"></param>
        void AliPayMentSucessHandle(Dictionary<string, string> sArray);

        /// <summary>
        /// order 支付成功处理
        /// </summary>
        /// <param name="order"></param>
        void PaymentedOrderHandle(Order order);
    }
}
