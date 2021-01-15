using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Areas.Manager
{
    public class WechatOrdersDetailsDto
    {
        public int Id { get; set; }

        public string TransactionID { get; set; }

        public string Out_Order_No { get; set; }

        public decimal Amount { get; set; }

        public decimal SubCharge { get; set; }

        public DateTime TranTime { get; set; }

        public string PayState { get; set; }

        public string SubState { get; set; }

        public string PayDescription { get; set; }

        public List<Models.Model.WechatSubDetail> Receivers { get; set; }
    }
}
