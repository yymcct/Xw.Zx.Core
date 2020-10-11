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
        public AliPayOrderDto CreateAliPayOrder(Member member, MemberVipType toVipTyp)
        {
            var errMsg = CheckUpdateVip(member, toVipTyp);
            if (!string.IsNullOrEmpty(errMsg))
            {
                throw new Exception(errMsg);
            }

            Order order = CreateOrder(member, toVipTyp);

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



        private string CheckUpdateVip(Member member, MemberVipType toVipTyp)
        {
            string errMsg = "";
            if (member.MemberVipType >= toVipTyp) 
            {
                errMsg = $"异常:用户{member.Phone}已是{member.MemberVipType.ToString()}, 无法升级";
            }
            return errMsg;
        }

        private Order CreateOrder(Member member, MemberVipType toVipTyp)
        {
            var product = GetProductByVipType(toVipTyp);

            var order = _context.Orders.Where(o => o.MemberId == member.Id
                                    && o.ProducName == product.Name
                                    && DateTime.Now.AddMinutes(-30) < o.AddTime
                                    && o.OrderState == OrderState.待付款).FirstOrDefault();

            if (order == null)
            {               
                order = new Order()
                {
                    MemberId = member.Id,
                    Timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffffff"),
                    MemberPhone = member.Phone,
                    ProductId = product.Id,
                    ProducName = product.Name,
                    Amount = product.Price,
                    AddTime = DateTime.Now,
                    OrderState = OrderState.待付款,
                    OrderPaymentType = OrderPaymentType.支付宝
                };
                _context.Add(order);
                _context.SaveChanges();
            }

            return order;
        }

        private Models.Model.Product GetProductByVipType(MemberVipType memberVipType)
        {
            string producName = "";

            switch (memberVipType)
            {
                case MemberVipType.Vip会员:
                    producName = "升级会员"; break;
                case MemberVipType.创客:
                    producName = "升级创客"; break;
                case MemberVipType.服务站:
                    producName = "升级服务站"; break;
                case MemberVipType.运营商:
                    producName = "升级运营商"; break;
            }

            return _context.Products.First(p => p.Name == producName);
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

                PaymentedOrderHandle(order);

            }
            catch (Exception ex)
            {
                _logger.LogWarning($"支付宝订单处理失败:{ex.Message}");
            }

        }

        /// <summary>
        /// </summary>
        /// <param name="order"></param>
        public void PaymentedOrderHandle(Order order)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                ShareProfit(order);
                UpdateOrder(order);

                //计算分润


                _context.SaveChanges();
                transaction.Commit();
            }
        }

        private void UpdateOrder(Order order)
        {
            var member = _context.Members.First(m => m.Id == order.MemberId);

            //订单状态修正为已付款            
            order.OrderState = OrderState.已付款;
            order.OrderPaymentType = OrderPaymentType.支付宝;
            _context.Entry(order).State = EntityState.Modified;

            //变更会员状态
            switch (order.ProducName)
            {
                case "升级会员": member.MemberVipType = MemberVipType.Vip会员; break;
                case "升级创客": member.MemberVipType = MemberVipType.创客; break;
                case "升级服务站": member.MemberVipType = MemberVipType.服务站; break;
                case "升级运营商": member.MemberVipType = MemberVipType.运营商; break;
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

        private void ShareProfit(Order order)
        {
            var member = _context.Members.First(m => m.Id == order.MemberId);

            switch (member.MemberVipType)
            {
                case MemberVipType.普通:
                    switch (order.ProducName)
                    {
                        case "升级会员": UserToVip(order); break;
                        case "升级创客": UserToChuangke(order); break;
                        case "升级服务站": UserToFuwuzhan(order); break;
                        case "升级运营商": UserToYunyinshang(order); break;
                    }
                    break;
                case MemberVipType.Vip会员:
                    switch (order.ProducName)
                    {
                        case "升级会员": new Exception("Vip会员不能升级为Vip会员"); break;
                        case "升级创客": VipToChuangke(order); break;
                        case "升级服务站": VipToFuwuzhan(order); break;
                        case "升级运营商": VipToYunyinshang(order); break;
                    }
                    break;
                case MemberVipType.创客:
                    switch (order.ProducName)
                    {
                        case "升级会员": new Exception("创客不能升级为Vip会员"); break;
                        case "升级创客": new Exception("创客不能升级为创客"); break;
                        case "升级服务站": ChuangkeToFuwuzhan(order); break;
                        case "升级运营商": ChuangkeToYunyinshang(order); break;
                    }
                    break;
                case MemberVipType.服务站:
                    switch (order.ProducName)
                    {
                        case "升级会员": new Exception("服务站不能升级为Vip会员"); break;
                        case "升级创客": new Exception("服务站不能升级为创客"); break;
                        case "升级服务站": new Exception("服务站不能升级为服务站"); break;
                        case "升级运营商": FuwuzhanToYunyinshang(order); break;
                    }
                    break;
                default:
                    new Exception("级别异常暂不支持升级"); break;
            }
        }

        /// <summary>
        /// 普通用户升级到会员: 一代60 2代80, 创客10元,服务站20元,运营商不再分润
        /// </summary>
        /// <param name="order"></param>
        private void UserToVip(Order order)
        {
            var member = _context.Members.First(m => m.Id == order.MemberId);
            var (oneInvite, twoInvite, chuangKe, fuWuZhan, yunYinShang) = FindBigHigherMember(member);

            if (order.Amount == 0) return;
            if (oneInvite != null)
            {
                _context.IncomeAccounts.Add(new IncomeAccount()
                {
                    MemberId = oneInvite.Id,
                    Amount = 60.00m,
                    SourceOrderId = order.Id,
                    SourceOrderMemberId = member.Id,
                    SourceOrderMemberInviteId = member.InviteId,
                    IncomeAccountType = IncomeAccountType.直接收益,
                    Remark = $"{member.Phone}升级会员产生的直接收益",
                });
            }

            if (twoInvite != null)
            {
                _context.IncomeAccounts.Add(new IncomeAccount()
                {
                    MemberId = twoInvite.Id,
                    Amount = 80.00m,
                    SourceOrderId = order.Id,
                    SourceOrderMemberId = member.Id,
                    SourceOrderMemberInviteId = member.InviteId,
                    IncomeAccountType = IncomeAccountType.间接收益,
                    Remark = $"{member.Phone}升级会员产生的间接收益",
                });
            }

            if (chuangKe != null)
            {
                _context.IncomeAccounts.Add(new IncomeAccount()
                {
                    MemberId = chuangKe.Id,
                    Amount = 10.00m,
                    SourceOrderId = order.Id,
                    SourceOrderMemberId = member.Id,
                    SourceOrderMemberInviteId = member.InviteId,
                    IncomeAccountType = IncomeAccountType.级差收益,
                    Remark = $"享受{member.Phone}升级会员产生的级差收益10元",
                });
            }

            if (fuWuZhan != null)
            {
                _context.IncomeAccounts.Add(new IncomeAccount()
                {
                    MemberId = fuWuZhan.Id,
                    Amount = 20.00m,
                    SourceOrderId = order.Id,
                    SourceOrderMemberId = member.Id,
                    SourceOrderMemberInviteId = member.InviteId,
                    IncomeAccountType = IncomeAccountType.级差收益,
                    Remark = $"享受{member.Phone}升级会员产生的级差收益20元",
                });
            }
        }

        /// <summary>
        /// 用户升级创客
        /// 一代:
        ///     1. 是会员分润10% 2.会员以上分润30%
        /// 二代:
        ///     1. 是会员无分润 2.会员以上分润15%
        ///     
        /// 级差: 
        ///     服务站: 有无运营商400
        ///     运营商: 无服务站600 有服务站200
        /// </summary>
        /// <param name="order"></param>
        private void UserToChuangke(Order order)
        {
            var member = _context.Members.First(m => m.Id == order.MemberId);
            var (oneInvite, twoInvite, chuangKe, fuWuZhan, yunYinShang) = FindBigHigherMember(member);

            if (order.Amount == 0) return;

            if (oneInvite != null)
            {
                if (oneInvite.MemberVipType == MemberVipType.Vip会员)
                {
                    _context.IncomeAccounts.Add(new IncomeAccount()
                    {
                        MemberId = oneInvite.Id,
                        Amount = order.Amount * 0.1m,
                        SourceOrderId = order.Id,
                        SourceOrderMemberId = member.Id,
                        SourceOrderMemberInviteId = member.InviteId,
                        IncomeAccountType = IncomeAccountType.直接收益,
                        Remark = $"{member.Phone}升级创客,一代分润:{order.Amount}*10%={order.Amount * 0.1m}",
                    });
                }
                else if (oneInvite.MemberVipType == MemberVipType.创客
                   || oneInvite.MemberVipType == MemberVipType.服务站
                   || oneInvite.MemberVipType == MemberVipType.运营商)
                {
                    _context.IncomeAccounts.Add(new IncomeAccount()
                    {
                        MemberId = oneInvite.Id,
                        Amount = order.Amount * 0.3m,
                        SourceOrderId = order.Id,
                        SourceOrderMemberId = member.Id,
                        SourceOrderMemberInviteId = member.InviteId,
                        IncomeAccountType = IncomeAccountType.直接收益,
                        Remark = $"{member.Phone}升级创客,一代分润:{order.Amount}*30%={order.Amount * 0.3m}",
                    });
                }
            }

            if (twoInvite != null)
            {
                if (twoInvite.MemberVipType == MemberVipType.创客
                    || twoInvite.MemberVipType == MemberVipType.服务站
                    || twoInvite.MemberVipType == MemberVipType.运营商)
                {
                    _context.IncomeAccounts.Add(new IncomeAccount()
                    {
                        MemberId = twoInvite.Id,
                        Amount = order.Amount * 0.15m,
                        SourceOrderId = order.Id,
                        SourceOrderMemberId = member.Id,
                        SourceOrderMemberInviteId = member.InviteId,
                        IncomeAccountType = IncomeAccountType.间接收益,
                        Remark = $"{member.Phone}升级创客,一代分润:{order.Amount}*15%={order.Amount * 0.15m}",
                    });
                }
            }

            if (fuWuZhan != null)
            {
                _context.IncomeAccounts.Add(new IncomeAccount()
                {
                    MemberId = fuWuZhan.Id,
                    Amount = 400m,
                    SourceOrderId = order.Id,
                    SourceOrderMemberId = member.Id,
                    SourceOrderMemberInviteId = member.InviteId,
                    IncomeAccountType = IncomeAccountType.级差收益,
                    Remark = $"{member.Phone}升级创客,级差分润:400",
                });
            }

            if (yunYinShang != null)
            {
                var amount = fuWuZhan == null ? 600m : 200m;

                _context.IncomeAccounts.Add(new IncomeAccount()
                {
                    MemberId = fuWuZhan.Id,
                    Amount = amount,
                    SourceOrderId = order.Id,
                    SourceOrderMemberId = member.Id,
                    SourceOrderMemberInviteId = member.InviteId,
                    IncomeAccountType = IncomeAccountType.级差收益,
                    Remark = $"{member.Phone}升级创客,级差分润:{amount}",
                });
            }
        }
        /// <summary>
        ///  一代是服务站以上级别则30%  一代是服务站以下级别10%
        ///  二代是服务站以上级别则10% 二代是服务站以下级别则没有
        ///  级差:
        ///     服务站: 有无运营商15%
        ///     运营商: 无服务站30% 有服务站15%
        /// </summary>
        /// <param name="order"></param>
        private void UserToFuwuzhan(Order order)
        {
            var member = _context.Members.First(m => m.Id == order.MemberId);
            var (oneInvite, twoInvite, chuangKe, fuWuZhan, yunYinShang) = FindBigHigherMember(member);

            if (order.Amount == 0) return;
            if (oneInvite != null)
            {
                if (oneInvite.MemberVipType == MemberVipType.Vip会员
                    || oneInvite.MemberVipType == MemberVipType.创客)

                {
                    _context.IncomeAccounts.Add(new IncomeAccount()
                    {
                        MemberId = oneInvite.Id,
                        Amount = order.Amount * 0.1m,
                        SourceOrderId = order.Id,
                        SourceOrderMemberId = member.Id,
                        SourceOrderMemberInviteId = member.InviteId,
                        IncomeAccountType = IncomeAccountType.直接收益,
                        Remark = $"{member.Phone}升级服务站,一代分润:{order.Amount}*10%={order.Amount * 0.1m}",
                    });
                }
                else if (oneInvite.MemberVipType == MemberVipType.服务站
                   || oneInvite.MemberVipType == MemberVipType.运营商)
                {
                    _context.IncomeAccounts.Add(new IncomeAccount()
                    {
                        MemberId = oneInvite.Id,
                        Amount = order.Amount * 0.3m,
                        SourceOrderId = order.Id,
                        SourceOrderMemberId = member.Id,
                        SourceOrderMemberInviteId = member.InviteId,
                        IncomeAccountType = IncomeAccountType.直接收益,
                        Remark = $"{member.Phone}升级服务站,一代分润:{order.Amount}*30%={order.Amount * 0.3m}",
                    });
                }
            }

            if (twoInvite != null)
            {
                if (twoInvite.MemberVipType == MemberVipType.服务站
                    || twoInvite.MemberVipType == MemberVipType.运营商)
                {
                    _context.IncomeAccounts.Add(new IncomeAccount()
                    {
                        MemberId = twoInvite.Id,
                        Amount = order.Amount * 0.1m,
                        SourceOrderId = order.Id,
                        SourceOrderMemberId = member.Id,
                        SourceOrderMemberInviteId = member.InviteId,
                        IncomeAccountType = IncomeAccountType.间接收益,
                        Remark = $"{member.Phone}升级创客,一代分润:{order.Amount}*10%={order.Amount * 0.1m}",
                    });
                }
            }
            //TODO 级差
            if (fuWuZhan != null)
            {
                _context.IncomeAccounts.Add(new IncomeAccount()
                {
                    MemberId = fuWuZhan.Id,
                    Amount = order.Amount * 0.15m,
                    SourceOrderId = order.Id,
                    SourceOrderMemberId = member.Id,
                    SourceOrderMemberInviteId = member.InviteId,
                    IncomeAccountType = IncomeAccountType.级差收益,
                    Remark = $"{member.Phone}升级服务站,级差分润:{order.Amount}*15%={order.Amount * 0.15m}",
                });
            }

            if (yunYinShang != null)
            {
                var amount = fuWuZhan == null ? order.Amount * 0.3m : order.Amount * 0.15m;

                _context.IncomeAccounts.Add(new IncomeAccount()
                {
                    MemberId = fuWuZhan.Id,
                    Amount = amount,
                    SourceOrderId = order.Id,
                    SourceOrderMemberId = member.Id,
                    SourceOrderMemberInviteId = member.InviteId,
                    IncomeAccountType = IncomeAccountType.级差收益,
                    Remark = $"{member.Phone}升级服务站,级差分润:{amount}",
                });
            }
        }

        private void UserToYunyinshang(Order order)
        {
            //什么都不做

        }

        private void VipToChuangke(Order order)
        {
            UserToChuangke(order);

        }
        private void VipToFuwuzhan(Order order)
        {
            UserToFuwuzhan(order);

        }

        private void VipToYunyinshang(Order order)
        {
            //什么都不做

        }

        private void ChuangkeToFuwuzhan(Order order)
        {
            UserToFuwuzhan(order);

        }

        private void ChuangkeToYunyinshang(Order order)
        {
            //什么都不做

        }

        private void FuwuzhanToYunyinshang(Order order)
        {
            //什么都不做

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

                if (tmpMember.MemberVipType == MemberVipType.创客)
                    HeHuoRen = tmpMember;

                if (YunYinShang != null && FuWuZhan != null && HeHuoRen != null)
                    break;

            } while (tmpMember.InviteId != 0);

            return (YunYinShang, FuWuZhan, HeHuoRen);
        }

        /// <summary>
        /// 查找上级vip 依次: 一代 二代 创客 服务站 运营商
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        private (Member, Member, Member, Member, Member) FindBigHigherMember(Member member)
        {
            Member oneInvite = null;
            Member twoInvite = null;
            Member chuangKe = null;
            Member fuWuZhan = null;
            Member yunYinShang = null;

            if (member.InviteId == 0) return (null, null, null, null, null);

            oneInvite = _context.Members.First(m => m.Id == member.InviteId);

            if (oneInvite != null && oneInvite.InviteId != 0)
                twoInvite = _context.Members.First(m => m.Id == oneInvite.InviteId);

            var tmpMember = member;
            do
            {
                tmpMember = _context.Members.Find(tmpMember.InviteId);

                if (tmpMember.MemberVipType == MemberVipType.创客)
                    chuangKe = tmpMember;

                if (tmpMember.MemberVipType == MemberVipType.服务站)
                    fuWuZhan = tmpMember;

                if (tmpMember.MemberVipType == MemberVipType.运营商)
                    yunYinShang = tmpMember;

                if (yunYinShang != null && fuWuZhan != null && chuangKe != null)
                    break;

            } while (tmpMember.InviteId != 0);

            return (oneInvite, twoInvite, chuangKe, fuWuZhan, yunYinShang);
        }


    }
}
