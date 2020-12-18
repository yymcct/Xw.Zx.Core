using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public class ShareProfitHandle : OrderPay
    {
        private readonly XwZxContext _context;
        private readonly Func<int, IShareprofit> _shareprofitProvider;
        private readonly ILogger<ShareProfitHandle> _logger;
        public ShareProfitHandle(XwZxContext context
            , Func<int, IShareprofit> shareprofitProvider
            , ILogger<ShareProfitHandle> logger)
        {
            _context = context;
            _shareprofitProvider = shareprofitProvider;
            _logger = logger;
        }
        protected override void Handle(Order order)
        {
            try
            {
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
            catch (Exception ex)
            {
                _logger.LogError($"产品ID{order.Id},{nameof(UpdateMemberTypeHandle)}处理异常", ex);
            }
        }
    }
}
