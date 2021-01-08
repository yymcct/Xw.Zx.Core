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
    public class BiqilinOrderSync
    {
        private readonly ILogger<BiqilinOrderSync> _logger;
        private readonly IBiqilinService _biqilinService;
        private readonly IOrderService _orderService;
        private readonly XwZxContext _context;

        public BiqilinOrderSync(
            ILogger<BiqilinOrderSync> logger
            , IBiqilinService biqilinService
            , IOrderService orderService
            , XwZxContext xwZxContext)
        {
            _logger = logger;
            _biqilinService = biqilinService;
            _orderService = orderService;
            _context = xwZxContext;
        }


        public void Run()
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

                var biqilinOrders = _context.BiqilinLogs.Where(o => o.OrderId == waitPayOrder.Id).ToArray();

                for (var j = 0; j < biqilinOrders.Length; j++)
                {
                    try
                    {
                        var biqilinOrder = biqilinOrders[j];

                        var query = _biqilinService.QueryOrder(biqilinOrder.BiqilinOrderNo);
                        if (CheckBiqilinOrder(waitPayOrder, query))
                        {
                            _orderService.OrderPay(waitPayOrder.Timestamp, OrderPaymentType.碧麒麟);
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }

                }

            }

            _logger.LogWarning(DateTime.Now.ToString());
        }

        private bool CheckBiqilinOrder(Order order, BiqilinRespone.Query query)
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
    }
}
