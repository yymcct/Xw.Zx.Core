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
        public readonly XwZxContext _context;
        public readonly IAlipaySdkService _alipaySdkService;
        public readonly IWapOrderPayService _wapOrderPayService;

        public OrderService(XwZxContext context
            , IWapOrderPayService wapOrderPayService
            , IAlipaySdkService alipaySdkService)
        {
            _context = context;
            _wapOrderPayService = wapOrderPayService;
            _alipaySdkService = alipaySdkService;
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
                        _wapOrderPayService.SucessHandle(order.Timestamp, OrderPaymentType.支付宝);
                    }
                }

            }
            catch (Exception ex)
            { 
            
            }
      
        }
    }
}
