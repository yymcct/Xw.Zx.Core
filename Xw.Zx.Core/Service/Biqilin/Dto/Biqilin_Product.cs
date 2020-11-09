using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Service
{
    public enum Biqilin_PayType
    { 
        微信 = 0,
        支付宝 =1
    }
    public class Biqilin_Product
    {
        public string Name { get; set; }
        public string Timestamp { get; set; }
        public decimal Amount { get; set; }
        public Biqilin_PayType Biqilin_PayType { get; set; }
        public string OpenId { get; set; }
    }
}
