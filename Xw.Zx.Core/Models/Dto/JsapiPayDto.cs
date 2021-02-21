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
            public string AppId { get; set; }
            public string TimeStamp { get; set; }
            public string SignType { get; set; }
            public string Package { get; set; }
            public string NonceStr { get; set; }
            public string PaySign { get; set; }
        }
    }
}
