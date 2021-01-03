using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Areas.Manager
{
    public class OrderMRespone
    {
        public class Item
        {
            public int Count { get; set; }
            public decimal Amount { get; set; }
        }

        public class Info
        {
            public Info()
            {
                OrderWaitPay = new Item();
                OrderSucess = new Item();
                IncomeWaitAudit = new Item();
                IncomeSucess = new Item();
                IncomeFail = new Item();
                WithdrawApplyFor = new Item();
                WithdrawTongjibuAudit = new Item();
                WithdrawCaiwubuAudit = new Item();
                WithdrawSucess = new Item();
                WithdrawFail = new Item();
            }

            /// <summary>
            /// 待付款订单
            /// </summary>
            public Item OrderWaitPay { get; set; }

            /// <summary>
            /// 支付成功订单
            /// </summary>
            public Item OrderSucess { get; set; }

            /// <summary>
            /// 分润待审核
            /// </summary>
            public Item IncomeWaitAudit { get; set; }

            /// <summary>
            /// 分润成功
            /// </summary>
            public Item IncomeSucess { get; set; }

            /// <summary>
            /// 分润失败
            /// </summary>
            public Item IncomeFail { get; set; }

            /// <summary>
            /// 申请提现
            /// </summary>
            public Item WithdrawApplyFor { get; set; }
            public Item WithdrawTongjibuAudit { get; set; }
            public Item WithdrawCaiwubuAudit { get; set; }
            public Item WithdrawSucess { get; set; }
            public Item WithdrawFail { get; set; }
        }
    }
}
