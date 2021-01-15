using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public partial class WechatOrders : ModelBase
    {

        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        [Sieve(CanFilter = true)]
        [Column(TypeName = "varchar(50)")]
        public string TransactionID { get; set; }

        [Sieve(CanFilter = true)]
        [Column(TypeName = "varchar(50)")]
        public string Out_Order_No { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubCharge { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        [Column(TypeName = "datetime")]
        public DateTime TranTime { get; set; }

        [Sieve(CanFilter = true)]
        [Column(TypeName = "varchar(50)")]
        public string PayState { get; set; }

        [Sieve(CanFilter = true)]
        [Column(TypeName = "varchar(50)")]
        public WechatOrderState SubState { get; set; } = WechatOrderState.待申请;

        [Column(TypeName = "varchar(256)")]
        public string PayDescription { get; set; }
    }

    public partial class WechatOrders
    {
        public enum WechatOrderState
        {
            待申请 = 0,
            申请中 = 10,
            分账完成 = 20,
            分账失败 = 30
        }
    }
}
