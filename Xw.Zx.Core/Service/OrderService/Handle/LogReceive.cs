using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public class LogReceive : OrderPay
    {
        private readonly XwZxContext _context;
        private readonly ILogger<LogReceive> _logger;
        public LogReceive(XwZxContext context
            , ILogger<LogReceive> logger)
        {
            _context = context;
            _logger = logger;
        }
        protected override void Handle(Order order)
        {
            //记录收款
            try
            {
                var receivables = new Receivable()
                {
                    OrderId = order.Id,
                    Amount = order.Amount,
                };
                _context.Receivables.Add(receivables);
                _context.SaveChanges();
               
            }
            catch (Exception ex)
            {
                _logger.LogError($"产品ID{order.Id},{nameof(UpdateMemberTypeHandle)}处理异常", ex);
            }
        }
    }
}
