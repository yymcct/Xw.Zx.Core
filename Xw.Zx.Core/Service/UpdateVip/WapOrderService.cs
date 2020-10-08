using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Controllers;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public class WapOrderService : IWapOrderService
    {
        private readonly ILogger<AlipayController> _logger;
        public readonly XwZxContext _context;
        public void SucessHandle(Dictionary<string, string> sArray)
        {
            var order = _context.Orders.Where(o => o.Timestamp == sArray["out_trade_no"]).FirstOrDefault();
            if (order.Amount != decimal.Parse(sArray["total_amount"]))
            {
                throw new Exception($"Notifyurl:异常订单, 金额不符 {sArray.ToString()}");
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                ShareProfit(order);
                UpdateOrder(order);
                _context.SaveChanges();
                transaction.Commit();
            }
        }

        private void ShareProfit(Order order)
        {
            var member = _context.Members.First(m => m.Id == order.MemberId);
            //检查当前会员级别和购买的级别
            //找到运营中心
            var (oneInvite, twoInvite, yunYinShang) = FindBigHigherMember(member);

            if (order.ProductId == 10)
            {
                //9.9的大礼包
                if (oneInvite != null)
                {
                    _context.IncomeAccounts.Add(new IncomeAccount()
                    {
                        MemberId = oneInvite.Id,
                        Amount = order.Amount,
                        SourceOrderId = order.Id,
                        SourceOrderMemberId = member.Id,
                        SourceOrderMemberInviteId = member.InviteId,
                        IncomeAccountType = IncomeAccountType.直接收益,
                        Remark = $"{member.Phone}升级,{oneInvite.Phone}收到分润:{order.Amount}*100%={order.Amount}",
                    });
                }
            }
            else
            {
                if (yunYinShang != null)
                {
                    _context.IncomeAccounts.Add(new IncomeAccount()
                    {
                        MemberId = yunYinShang.Id,
                        Amount = order.Amount * 0.5m,
                        SourceOrderId = order.Id,
                        SourceOrderMemberId = member.Id,
                        SourceOrderMemberInviteId = member.InviteId,
                        IncomeAccountType = IncomeAccountType.间接收益,
                        Remark = $"{member.Phone}升级,{yunYinShang.Phone}收到分润:{order.Amount}*50%={order.Amount * 0.5m}",
                    });
                }
            }

            //分润

        }

        private void UpdateOrder(Order order)
        {
            var member = _context.Members.First(m => m.Id == order.MemberId);

            //订单状态修正为已付款            
            order.OrderState = OrderState.已付款;
            _context.Entry(order).State = EntityState.Modified;

            //变更会员状态
            switch (order.ProductId)
            {
                case 7: member.MemberVipType = MemberVipType.运营中心; break;
                case 8: member.MemberVipType = MemberVipType.合伙人; break;
            }
            _context.Entry(member).State = EntityState.Modified;

            // 生成收款单
            var receivables = new Receivable()
            {
                OrderId = order.Id,
                Amount = order.Amount,
            };
            _context.Receivables.Add(receivables);
        }

        /// <summary>
        /// 查找上级vip 依次: 一代 二代  运营中心
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        private (Member, Member, Member) FindBigHigherMember(Member member)
        {
            Member oneInvite = null;
            Member twoInvite = null;
            Member yunYinZhongXin = null;

            if (member.InviteId == 0) return (null, null, null);

            oneInvite = _context.Members.First(m => m.Id == member.InviteId);

            if (oneInvite != null && oneInvite.InviteId != 0)
                twoInvite = _context.Members.First(m => m.Id == oneInvite.InviteId);

            var tmpMember = member;
            do
            {
                tmpMember = _context.Members.Find(tmpMember.InviteId);

                if (tmpMember.MemberVipType == MemberVipType.运营中心)
                    yunYinZhongXin = tmpMember;

                if (yunYinZhongXin != null)
                    break;

            } while (tmpMember.InviteId != 0);

            return (oneInvite, twoInvite, yunYinZhongXin);
        }


    }
}
