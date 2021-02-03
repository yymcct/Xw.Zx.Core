using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Xw.Zx.Core.Service
{
    public class CiticbankDto
    {
        public class Product
        {
            public string Name { get; set; }
            public string Timestamp { get; set; }
            public decimal Amount { get; set; }
        }
        public class Qrcode
        {
            public string CodeUrl { get; set; }
            //平台唯一单号
            public string uuid { get; set; }
        }

}
}
