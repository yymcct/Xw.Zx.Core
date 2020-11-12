using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Controllers;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public class WapOrderPayService : IWapOrderPayService
    {
        private readonly ILogger<WapOrderPayService> _logger;
        public readonly XwZxContext _context;

        public WapOrderPayService(ILogger<WapOrderPayService> logger
            , XwZxContext xwZxContext)
        {
            _logger = logger;
            _context = xwZxContext;
        }

        public void SucessHandle(string timestamp, OrderPaymentType paymentType)
        {
            var order = _context.Orders.Where(o => o.Timestamp == timestamp).FirstOrDefault();            

            using (var transaction = _context.Database.BeginTransaction())
            {
                ShareProfit(order);
                UpdateOrder();
                _context.SaveChanges();
                transaction.Commit();
            }

            void UpdateOrder()
            {
                var member = _context.Members.First(m => m.Id == order.MemberId);

                //订单状态修正为已付款            
                order.OrderState = OrderState.已付款;
                order.OrderPaymentType = paymentType;
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
