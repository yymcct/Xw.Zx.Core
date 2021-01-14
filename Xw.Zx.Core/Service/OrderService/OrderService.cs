using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Helper;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service;

namespace Xw.Zx.Core.Service
{
    public class OrderService : IOrderService
    {
        private readonly XwZxContext _context;
        private readonly IAlipaySdkService _alipaySdkService;
        private readonly ShareProfitHandle _ShareProfitHandle;
        private readonly UpdateMemberTypeHandle _UpdateMemberTypeHandle;
        private readonly LogReceive _LogReceive;
        private readonly PresentedCoupons _PresentedCoupons;
        private readonly IMemberIntegralService _memberIntegralService;

        public OrderService(XwZxContext context
            , IAlipaySdkService alipaySdkService
            , ShareProfitHandle ShareProfitHandle
            , UpdateMemberTypeHandle UpdateMemberTypeHandle
            , LogReceive LogReceive
            , PresentedCoupons PresentedCoupons
            , IMemberIntegralService memberIntegralService
            )
        {
            _context = context;

            _alipaySdkService = alipaySdkService;

            _ShareProfitHandle = ShareProfitHandle;
            _UpdateMemberTypeHandle = UpdateMemberTypeHandle;
            _LogReceive = LogReceive;
            _PresentedCoupons = PresentedCoupons;
            _memberIntegralService = memberIntegralService;
        }

        public Order GetOrder(int orderId)
        {
            UpDateOrderPayState(orderId);


            return _context.Orders.FirstOrDefault(o => o.Id == orderId);
        }

        public void UpDateOrderPayState(int orderId)
        {
            try
            {
                var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);

                if (order.OrderState == OrderState.待付款)
                {
                    var alipayTradeQueryResponse = _alipaySdkService.Query(order.Timestamp);

                    if (alipayTradeQueryResponse.TradeStatus == "TRADE_SUCCESS")
                    {
                        //_wapOrderPayService.SucessHandle(order.Timestamp, OrderPaymentType.支付宝);
                        OrderPay(order.Timestamp, OrderPaymentType.支付宝);
                    }
                }

            }
            catch (Exception ex)
            {

            }

        }
        public void CouponOrderPay(int memberId, int orderId, int couponreceiveId)
        {
            var order = _context.Orders.Where(o => o.Id == orderId && o.MemberId == memberId && o.OrderState == OrderState.待付款).FirstOrDefault();
            if (order == null)
            {
                throw new Exception("订单已关闭!");
            }


            int couponReceiveId = (from coupon in _context.Coupons
                                   join couponreceive in _context.CouponReceives
                                   on coupon.Id equals couponreceive.Couponid
                                   where couponreceive.Memberid == order.MemberId
                                       && couponreceive.Memberid == memberId
                                       && coupon.ProductId == order.ProductId
                                       && couponreceive.CouponUseState == CouponUseState.未使用
                                       && coupon.Money == order.Amount
                                   select couponreceive.Id)
                   .FirstOrDefault();

            if (couponReceiveId == 0)
            {
                throw new Exception("优惠券无效!");
            }


            var couponReceive = _context.CouponReceives.First(c => c.Id == couponreceiveId);
            couponReceive.CouponUseState = CouponUseState.已使用;

            _context.CouponUseLogs.Add(new CouponUseLog()
            {
                CouponReceiveid = couponreceiveId,
                Productid = order.ProductId,
                Orderid = order.Id
            });

            order.Amount = 0;
            order.OrderState = OrderState.已付款;
            order.OrderPaymentType = OrderPaymentType.全额优惠券;
            _context.SaveChanges();

        }

        public void MemberIntegralPay(int memberId, int orderId)
        {
            var order = _context
                .Orders
                .Where(o => o.Id == orderId && o.MemberId == memberId && o.OrderState == OrderState.待付款)
                .FirstOrDefault();

            if (order == null)
            {
                throw new ZzzException("订单异常, 无法付款");
            }
            var memberIntegral = _memberIntegralService.GetMemberIntegral(order.MemberId);

            if (memberIntegral.AvailableIntegrals <  _memberIntegralService.AmountToIntegral(order.Amount))
            {
                throw new ZzzException("积分不足, 无法完成请求");
            }


            _memberIntegralService.AddMemberIntegral(new MemberIntegralRecord
            {
                MemberId = order.MemberId,
                Integral = 0- _memberIntegralService.AmountToIntegral(order.Amount),
                TypeId = MemberIntegral.IntegralType.Trader,
                Remark = $"用于单号{order.Id}的支付"
            });

            order.Amount = 0;
            order.OrderState = OrderState.已付款;
            order.OrderPaymentType = OrderPaymentType.积分;
            _context.SaveChanges();

        }

        public void OrderPay(string timestamp, OrderPaymentType paymentType)
        {
            var order = _context.Orders.Where(o => o.Timestamp == timestamp && o.OrderState == OrderState.待付款).FirstOrDefault();
            if (order != null)
            {
                var member = _context.Members.First(m => m.Id == order.MemberId);

                order.OrderState = OrderState.已付款;
                order.OrderPaymentType = paymentType;
                _context.SaveChanges();

                _LogReceive.SetOrderPay(_ShareProfitHandle)
                    .SetOrderPay(_UpdateMemberTypeHandle)
                    .SetOrderPay(_ShareProfitHandle)
                    .SetOrderPay(_PresentedCoupons);

                _LogReceive.HandleOrderPayRequest(order);
            }
        }
    }
}
