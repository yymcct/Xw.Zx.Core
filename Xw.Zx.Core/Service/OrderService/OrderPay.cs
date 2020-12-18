using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public abstract class OrderPay : IOrderPay
    {
       
        public IOrderPay _orderPay { get; private set; }

        public IOrderPay SetOrderPay(IOrderPay orderPay)
        {
            _orderPay = orderPay;
            return orderPay;
        }

        public void HandleOrderPayRequest(Order order)
        {
            Handle(order);
            if (_orderPay != null)
            {
                _orderPay.HandleOrderPayRequest(order);
            }            
        }

        abstract protected void Handle(Order order);
    }
}
