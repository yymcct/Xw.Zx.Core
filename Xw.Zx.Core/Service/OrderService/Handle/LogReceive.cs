using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public class PresentedCoupons : OrderPay
    {
        private readonly XwZxContext _context;
        private readonly ILogger<PresentedCoupons> _logger;
        public PresentedCoupons(XwZxContext context
            , ILogger<PresentedCoupons> logger)
        {
            _context = context;
            _logger = logger;
        }
        protected override void Handle(Order order)
        {         
            //赠送优惠券
            try
            { 
                //运营中心代理权
                if (order.Id == 7)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        var code = new UpdateVipAuthCode()
                        {
                            OwinId = order.Id,
                            Code = Guid.NewGuid().ToString(),                 
                        };
                        _context.UpdateVipAuthCodes.Add(code);
                    }
                    _context.SaveChanges();
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError($"产品ID{order.Id},{nameof(UpdateMemberTypeHandle)}处理异常", ex);
            }
        }
    }
}
