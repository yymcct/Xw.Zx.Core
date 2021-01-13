using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public class UpdateMemberTypeHandle : OrderPay
    {
        private readonly XwZxContext _context;
        private readonly ILogger<UpdateMemberTypeHandle> _logger;
        public UpdateMemberTypeHandle(XwZxContext context
            , ILogger<UpdateMemberTypeHandle> logger)
        {
            _context = context;
            _logger = logger;
        }

        protected override void Handle(Order order)
        {
            try
            {
                //运营中心代理权 49800
                if (order.ProductId == 7)
                {
                    var member = _context.Members.FirstOrDefault(m => m.Id == order.MemberId);
                    if (member != null)
                    {
                        member.MemberVipType = MemberVipType.运营中心;
                        _context.SaveChanges();
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
