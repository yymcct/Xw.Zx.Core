using Alipay.AopSdk.AspnetCore;
using Alipay.AopSdk.Core.Domain;
using Alipay.AopSdk.Core.Request;
using Alipay.AopSdk.Core.Response;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Controllers;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;
using Microsoft.EntityFrameworkCore;
namespace Xw.Zx.Core.Service
{
    public class UpDateVip1Service : IUpDateVip1Service
    {
        private readonly ILogger<AlipayController> _logger;
        private readonly AlipayService _alipayService;
        public readonly XwZxContext _context;
        public readonly IMapper _mapper;
        public UpDateVip1Service(ILogger<AlipayController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , AlipayService alipayService)
        {
            _logger = logger;
            _alipayService = alipayService;
            _mapper = mapper;
            _context = xwZxContext;
        }
        public AliPayOrderDto CreateAliPayOrder(Member member)
        {
            if (member.MemberVipType != MemberVipType.普通)
            {
                throw new Exception($"异常:用户{member.Phone}已是VIP, 无法升级");
            }

            Order order = CreateOrder(member);

            AlipayTradeAppPayModel model = new AlipayTradeAppPayModel()
            {
                Body = order.ProducName,
                Subject = order.ProducName,
                TotalAmount = order.Amount.ToString("n"),
                ProductCode = "QUICK_MSECURITY_PAY",
                OutTradeNo = order.Timestamp,
                TimeoutExpress = "50m",
            };

            AlipayTradeAppPayRequest request = new AlipayTradeAppPayRequest();
            request.SetNotifyUrl("http://139.155.8.217/api/Alipay/Notifyurl");
            request.SetBizModel(model);

            AlipayTradeAppPayResponse response = _alipayService.SdkExecute(request);

            return new AliPayOrderDto()
            {
                ProductName = order.ProducName,
                ProductPrice = order.Amount.ToString("n"),
                AlipayTradeAppPayResponse = response.Body
            };
        }

        private Order CreateOrder(Member member)
        {
            var order = _context.Orders.Where(o => o.MemberId == member.Id
                                    && o.ProducName == "升级会员"
                                    && DateTime.Now.AddMinutes(-30) < o.AddTime
                                    && o.OrderState == OrderState.待付款).FirstOrDefault();

            if (order == null)
            {
                var product = _context.Products.First(p => p.Name == "升级会员");
                order = new Order()
                {
                    MemberId = member.Id,
                    Timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffffff"),
                    MemberPhone = member.Phone,
                    ProductId = product.Id,
                    ProducName = product.Name,
                    Amount = product.Price,
                    AddTime = DateTime.Now,
                    OrderState = OrderState.待付款
                };
                _context.Add(order);
                _context.SaveChanges();
            }

            return order;
        }

        public void AliPayMentSucessHandle(Dictionary<string, string> sArray)
        {
            try
            {
                var order = _context.Orders.Where(o => o.Timestamp == sArray["out_trade_no"]).FirstOrDefault();

                if (order == null)
                {
                    throw new Exception($"Notifyurl:异常订单 {sArray.ToString()}");
                }

                if (order.Amount != decimal.Parse(sArray["total_amount"]))
                {
                    throw new Exception($"Notifyurl:异常订单, 金额不符 {sArray.ToString()}");
                }

                UpdateVipHandle(order);

            }
            catch (Exception ex)
            {
                _logger.LogWarning($"支付宝订单处理失败:{ex.Message}");
            }

        }

        private void UpdateVipHandle(Order order)
        {
            var member = _context.Members.First(m => m.Id == order.MemberId);
            var InviteId = member.InviteId;
            var InviteInviteId = InviteId == 0 ? 0 : _context.Members.First(m => m.Id == InviteId).InviteId;

            (var YunYinShang, var FuWuZhan, var HeHuoRen) = FindBigVip(member);

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //订单状态修正为已付款
                    _context.Entry(order).State = EntityState.Modified;
                    order.OrderState = OrderState.已付款;


                    // 生成收款单
                    var receivables = new Receivable()
                    {
                        OrderId = order.Id,
                        Amount = order.Amount,
                    };

                    _context.Receivables.Add(receivables);

                    //变更会员状态

                    if (member.MemberVipType == MemberVipType.普通)
                    {
                        member.MemberVipType = MemberVipType.Vip会员;
                    }

                    //生成一代直接收益单
                    _context.IncomeAccounts.Add(new IncomeAccount()
                    {
                        MemberId = InviteId,
                        Amount = 60.00m,
                        SourceOrderId = order.Id,
                        SourceOrderMemberId = member.Id,
                        SourceOrderMemberInviteId = member.InviteId,
                        IncomeAccountType = IncomeAccountType.直接收益,
                        Remark = $"{member.Phone}升级会员产生的直接收益",
                    });

                    // 生成二代间接收益单
                    if (InviteInviteId != 0)
                    {                       
                        _context.IncomeAccounts.Add(new IncomeAccount()
                        {
                            MemberId = InviteInviteId,
                            Amount = 80.00m,
                            SourceOrderId = order.Id,
                            SourceOrderMemberId = member.Id,
                            SourceOrderMemberInviteId = member.InviteId,
                            IncomeAccountType = IncomeAccountType.间接收益,
                            Remark = $"{member.Phone}升级会员产生的间接收益",
                        });                        
                    }

                    //合伙人 分润
                    if (HeHuoRen != null)
                    {
                        _context.IncomeAccounts.Add(new IncomeAccount()
                        {
                            MemberId = HeHuoRen.Id,
                            Amount = 5.00m,
                            SourceOrderId = order.Id,
                            SourceOrderMemberId = member.Id,
                            SourceOrderMemberInviteId = member.InviteId,
                            IncomeAccountType = IncomeAccountType.级差收益,
                            Remark = $"{member.Phone}升级会员产生的间接收益",
                        });
                    }
                    //服务站 分润
                    if (FuWuZhan != null)
                    {
                        var tmpAmount = HeHuoRen != null ? 5.00m : 10.00m;
                        _context.IncomeAccounts.Add(new IncomeAccount()
                        {
                            MemberId = FuWuZhan.Id,
                            Amount = 5.00m,
                            SourceOrderId = order.Id,
                            SourceOrderMemberId = member.Id,
                            SourceOrderMemberInviteId = member.InviteId,
                            IncomeAccountType = IncomeAccountType.级差收益,
                            Remark = $"{member.Phone}升级会员产生的级差收益",
                        });                      
                    }
                    //运营商 分润
                    if (YunYinShang != null)
                    {
                        decimal tmpAmount = 15;
                        if (FuWuZhan != null && HeHuoRen != null)
                        {
                            tmpAmount = 5.00m;
                        }
                        else if (FuWuZhan == null && HeHuoRen != null)
                        {
                            tmpAmount = 10.00m;
                        }
                        else if (FuWuZhan != null && HeHuoRen == null)
                        {
                            tmpAmount = 5.00m;
                        }
                        else
                        {
                            tmpAmount = 15;
                        }
                        _context.IncomeAccounts.Add(new IncomeAccount()
                        {
                            MemberId = YunYinShang.Id,
                            Amount = tmpAmount,
                            SourceOrderId = order.Id,
                            SourceOrderMemberId = member.Id,
                            SourceOrderMemberInviteId = member.InviteId,
                            IncomeAccountType = IncomeAccountType.级差收益,
                            Remark = $"{member.Phone}升级会员产生的级差收益",
                        });
                    }

                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"事务处理失败:{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 查找用户链上的 运营商, 服务站, 合伙人
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        private (Member, Member, Member) FindBigVip(Member member)
        {
            Member YunYinShang = null;
            Member FuWuZhan = null;
            Member HeHuoRen = null;

            if (member.InviteId == 0) return (null, null, null);
            var tmpMember = member;
            do
            {
                tmpMember = _context.Members.Find(tmpMember.InviteId);
                if (tmpMember.MemberVipType == MemberVipType.运营商)
                    YunYinShang = tmpMember;

                if (tmpMember.MemberVipType == MemberVipType.服务站)
                    FuWuZhan = tmpMember;

                if (tmpMember.MemberVipType == MemberVipType.合伙人)
                    HeHuoRen = tmpMember;

                if (YunYinShang != null && FuWuZhan != null && HeHuoRen != null)
                    break;

            } while (tmpMember.InviteId != 0);

            return (YunYinShang, FuWuZhan, HeHuoRen);
        }

    }
}
