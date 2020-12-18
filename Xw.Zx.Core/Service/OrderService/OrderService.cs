using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service;

namespace Xw.Zx.Core.Service
{
    public class OrderService : IOrderService
    {
        private readonly XwZxContext _context;
        private readonly IAlipaySdkService _alipaySdkService;
        private readonly ShareProfitHandle _ShareProfitHandle;
        private readonly UpdateMemberTypeHandle _UpdateMemberTypeHandle;
        private readonly LogReceive _LogReceive;
        private readonly PresentedCoupons _PresentedCoupons;


        public OrderService(XwZxContext context
            , IAlipaySdkService alipaySdkService
            , ShareProfitHandle ShareProfitHandle
            , UpdateMemberTypeHandle UpdateMemberTypeHandle
            , LogReceive LogReceive
            , PresentedCoupons PresentedCoupons
            )
        {
            _context = context;
        
            _alipaySdkService = alipaySdkService;

            _ShareProfitHandle =ShareProfitHandle;
            _UpdateMemberTypeHandle =UpdateMemberTypeHandle;
            _LogReceive = LogReceive;
            _PresentedCoupons = PresentedCoupons;
        }

        public Order GetOrder(int orderId)
        {
            UpDateOrderPayState(orderId);


            return _context.Orders.FirstOrDefault(o => o.Id == orderId);
        }

        public void UpDateOrderPayState(int orderId)
        {
            try
            {
                var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);

                if (order.OrderState == OrderState.待付款)
                {
                    var alipayTradeQueryResponse = _alipaySdkService.Query(order.Timestamp);

                    if (alipayTradeQueryResponse.TradeStatus == "TRADE_SUCCESS")
                    {
                        //_wapOrderPayService.SucessHandle(order.Timestamp, OrderPaymentType.支付宝);
                        OrderPay(order.Timestamp, OrderPaymentType.支付宝);
                    }
                }

            }
            catch (Exception ex)
            {

            }

        }

        public void OrderPay(string timestamp, OrderPaymentType paymentType)
        {
            var order = _context.Orders.Where(o => o.Timestamp == timestamp && o.OrderState == OrderState.待付款).FirstOrDefault();
            if (order != null )
            {
                var member = _context.Members.First(m => m.Id == order.MemberId);

                order.OrderState = OrderState.已付款;
                order.OrderPaymentType = paymentType;
                _context.SaveChanges();

                _LogReceive.SetOrderPay(_ShareProfitHandle)
                    .SetOrderPay(_UpdateMemberTypeHandle)
                    .SetOrderPay(_ShareProfitHandle)
                    .SetOrderPay(_PresentedCoupons);

                _LogReceive.HandleOrderPayRequest(order);
            }
        }
    }
}
