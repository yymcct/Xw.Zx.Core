using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service.ShareProfit
{
    [ShareProfitTemplate(9)]
    public class ShareProfit9_9 : ShareProfitAbsract, IShareprofit
    {
        private readonly ILogger<ShareProfit9_9> _logger;
        private readonly XwZxContext _context;
        private readonly IMemberService _member;

        public ShareProfit9_9(ILogger<ShareProfit9_9> logger
            , XwZxContext context
            , IMemberService member)
        {
            _logger = logger;
            _context = context;
            _member = member;
        }

        public void ShareProfit(int orderId)
        {
            try
            {
                var order = _context.Orders.First(o => o.Id == orderId);
                var member = _context.Members.First(o => o.Id == order.MemberId);
                var inviter = _member.GetInviterMember(order.MemberId);

                if (inviter != null)
                {
                    var remark = $"{member.RealName}[{member.Phone}]购买:{order.ProducName}[{order.Id}]产生分润:{9.0}元";

                    _context.IncomeAccounts.Add(new IncomeAccount()
                    {
                        MemberId = inviter.Id,
                        Amount = 9.0m,//order.Amount,
                        SourceOrderId = order.Id,
                        SourceOrderMemberId = order.MemberId,
                        SourceOrderMemberInviteId = inviter.Id,
                        IncomeAccountType = IncomeAccountType.直接收益,
                        Remark = remark,
                    });

                    _context.SaveChanges();
                    _logger.LogError(remark);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"分润异常,产品ID{orderId}");
            }
        }
    }
}
