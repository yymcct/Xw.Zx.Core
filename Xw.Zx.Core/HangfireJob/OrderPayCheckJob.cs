using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Controllers;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service;

namespace Xw.Zx.Core.HangfireJob
{
    public class OrderPayCheckJob
    {
        private readonly ILogger<OrderPayCheckJob> _logger;
        private readonly IBiqilinService _biqilinService;
        private readonly IOrderService _orderService;
        private readonly ICiticbankService _citicbankService;
        private readonly XwZxContext _context;

        public OrderPayCheckJob(
            ILogger<OrderPayCheckJob> logger
            , IBiqilinService biqilinService
            , IOrderService orderService
            , ICiticbankService citicbankService
            , XwZxContext xwZxContext)
        {
            _logger = logger;
            _biqilinService = biqilinService;
            _orderService = orderService;
            _context = xwZxContext;
            _citicbankService = citicbankService;
        }

        /// <summary>
        /// 检查已过期订单
        /// </summary>
        private void UpdateOverdue()
        {
            var waitPayOrders = _context.Orders.Where(o => o.OrderState == OrderState.待付款).ToArray();

            for (var i = 0; i < waitPayOrders.Length; i++)
            {
                var waitPayOrder = waitPayOrders[i];

                TimeSpan ts = DateTime.Now.Subtract(waitPayOrder.AddTime);
                if (ts.TotalHours > 12)
                {
                    waitPayOrder.OrderState = OrderState.已失效;
                    waitPayOrder.IsDelete = true;
                    _context.SaveChanges();
                    continue;
                }
            }
        }

        private void UpdateBiqilinPay()
        {
            var waitPayOrders = _context.Orders.Where(o => o.OrderState == OrderState.待付款).ToArray();

            for (var i = 0; i < waitPayOrders.Length; i++)
            {
                var waitPayOrder = waitPayOrders[i];

                var biqilinOrders = _context.BiqilinLogs.Where(o => o.OrderId == waitPayOrder.Id).ToArray();

                for (var j = 0; j < biqilinOrders.Length; j++)
                {
                    try
                    {
                        var biqilinOrder = biqilinOrders[j];

                        var query = _biqilinService.QueryOrder(biqilinOrder.BiqilinOrderNo);
                        if (CheckBiqilinPay(waitPayOrder, query))
                        {
                            _orderService.OrderPay(waitPayOrder.Timestamp, OrderPaymentType.碧麒麟, query.orderNo);
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }

                }

            }
        }

        /// <summary>
        /// 检查碧麒麟订单支付状态
        /// </summary>
        /// <param name="order"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private bool CheckBiqilinPay(Order order, BiqilinRespone.Query query)
        {

            if (order != null && query != null
                && query.orderStatus == "TRADE_SUCCESS"
                && query.outOrderNo == order.Timestamp
                && (decimal.Parse(query.amount)) == order.Amount)
            {
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// 检查中信订单支付状态
        /// </summary>
        private void UpdateCiticbankPay()
        {
            var timestamps =_citicbankService.GetAllCiticbankUnPayOrder();
            foreach (var timestamp in timestamps)
            {
                try
                {
                    var resHashTable = _citicbankService.QueryFull(timestamp);
                    if (resHashTable["trade_state"].ToString() == "SUCCESS")
                    {
                        _orderService.OrderPay(timestamp, OrderPaymentType.中信, resHashTable["transaction_id"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }             
            }
        }

        public void Run()
        {
            UpdateOverdue();

            UpdateCiticbankPay();

            UpdateBiqilinPay();
        }
    }
}
