using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public class AlipayLog
    {
        public int Id { get; set; }
        public int PaymentId { get; set; } = 0;
        public string code { get; set; } = "";
        public string msg { get; set; } = "";
        public string out_biz_no { get; set; } = "";
        public string order_id { get; set; } = "";
        public string pay_date { get; set; } = "";
        public string sub_code { get; set; } = "";
        public string sub_msg { get; set; } = "";

        public DateTime AddTime { get; set; } = DateTime.Now;
    }

    public class AlipayResponse
    {
        public AlipayLog alipay_fund_trans_toaccount_transfer_response { get; set; }
    }
}
