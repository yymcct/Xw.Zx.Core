using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service.ShareProfit
{
    /// <summary>
    /// 反润50%
    /// </summary>
    [ShareProfitTemplate(2)]
    public class ShareProfit2 : ShareProfitAbsract, IShareprofit
    {
        private readonly ILogger<ShareProfit2> _logger;
        private readonly XwZxContext _context;
        private readonly IMemberService _member;

        public ShareProfit2(ILogger<ShareProfit2> logger
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
                Member yyzxMember = null;
                if (member.MemberVipType == MemberVipType.运营中心)
                {
                    yyzxMember = member;
                }
                else
                {
                    yyzxMember = _member.GetYunyinzhongxinMember(member);
                }


                if (yyzxMember != null)
                {
                    var money = order.Amount * 0.5m;
                    var remark = $"{member.RealName}[{member.Phone}]购买:{order.ProducName}[{order.Id}]产生分润:{order.Amount}*50%={money}元";


                    _context.IncomeAccounts.Add(new IncomeAccount()
                    {
                        MemberId = yyzxMember.Id,
                        Amount = money,
                        SourceOrderId = order.Id,
                        SourceOrderMemberId = order.MemberId,
                        SourceOrderMemberInviteId = yyzxMember.Id,
                        IncomeAccountType = IncomeAccountType.直接收益,
                        IncomeAccountState = IncomeAccountState.待审核,
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
