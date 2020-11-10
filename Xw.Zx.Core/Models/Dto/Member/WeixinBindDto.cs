using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Dto
{
    public class WeixinBindDto
    {
        public string OpenId { get; set; }
        public string Phone { get; set; }
        public int SmsCheck { get; set; }
    }
}
