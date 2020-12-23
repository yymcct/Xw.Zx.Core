using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service.ShareProfit
{
    /// <summary>
    /// 反润7000
    /// </summary>
    [ShareProfitTemplate(3)]
    public class ShareProfit3 : ShareProfitAbsract, IShareprofit
    {
        private readonly ILogger<ShareProfit3> _logger;
        private readonly XwZxContext _context;
        private readonly IMemberService _member;

        public ShareProfit3(ILogger<ShareProfit3> logger
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
                var yyzxMember = _member.GetYunyinzhongxinMember(member);

                if (yyzxMember != null)
                {
                    var money = 7000m;
                    if (order.Amount > money)
                    {
                        var remark = $"{member.RealName}[{member.Phone}]购买:{order.ProducName}[{order.Id}]产生分润:7000元";
                        using (var transaction = _context.Database.BeginTransaction())
                        {
                            _context.IncomeAccounts.Add(new IncomeAccount()
                            {
                                MemberId = yyzxMember.Id,
                                Amount = money,
                                SourceOrderId = order.Id,
                                SourceOrderMemberId = order.MemberId,
                                SourceOrderMemberInviteId = yyzxMember.Id,
                                IncomeAccountType = IncomeAccountType.直接收益,
                                Remark = remark,
                            });

                            _context.MemberBalanceLogs.Add(new MemberBalanceLog()
                            {
                                Memberid = yyzxMember.Id,
                                memberMoneySource = MemberMoneySource.分润,
                                SourceId = order.Id,
                                Amount = money,
                                OriginalMoney = yyzxMember.Money,
                                CurMoney = yyzxMember.Money += money,
                                Remark = remark
                            });

                            yyzxMember.Money += money;
                            _context.SaveChanges();
                            transaction.Commit();
                        }
                        _logger.LogError(remark);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"分润异常,产品ID{orderId}");
            }
        }
    }
}
