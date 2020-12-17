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
        private readonly IWapOrderPayService _wapOrderPayService;
        private readonly Func<int, IShareprofit> _shareprofitProvider;

        public OrderService(XwZxContext context
            , IWapOrderPayService wapOrderPayService
            , IAlipaySdkService alipaySdkService
            , Func<int, IShareprofit> shareprofitProvider)
        {
            _context = context;
            _wapOrderPayService = wapOrderPayService;
            _alipaySdkService = alipaySdkService;
            _shareprofitProvider = shareprofitProvider;
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

                //记录收款
                var receivables = new Receivable()
                {
                    OrderId = order.Id,
                    Amount = order.Amount,
                };
                _context.Receivables.Add(receivables);
                _context.SaveChanges();

                //TODO变更VIP状态

                //分润
                var shareProfitConfig = _context.ShareProfitConfigs.FirstOrDefault(s => s.ProductId == order.ProductId);
                if (shareProfitConfig != null)
                {
                    var shareProfit = _shareprofitProvider(shareProfitConfig.ShareProfitTemplateId);
                    if (shareProfit != null)
                    {
                        shareProfit.ShareProfit(order.Id);
                    }
                }
            }
        }
    }
}
