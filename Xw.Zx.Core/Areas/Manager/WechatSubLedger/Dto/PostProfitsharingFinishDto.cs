using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Areas
{
    public class PostProfitsharingFinishDto
    {
        /// <summary>
        /// 交易订单号
        /// </summary>
        public string transaction_id { get; set; }
        /// <summary>
        /// 商户分账单号
        /// </summary>
        public string last_out_order_no { get; set; }
        /// <summary>
        /// 分账完结描述
        /// </summary>
        public string description{get;set;}
    }
}
