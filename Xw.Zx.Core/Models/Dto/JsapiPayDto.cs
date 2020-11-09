using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Dto
{
    public class JsapiPayResponeDto
    {
        public class JsapiPay
        {
            public string jsAppId { get; set; }
            public string jsTimeStamp { get; set; }
            public string jsSignType { get; set; }
            public string jsPackages { get; set; }
            public string jsNonceStr { get; set; }
            public string jsPaySign { get; set; }
        }
    }
}
