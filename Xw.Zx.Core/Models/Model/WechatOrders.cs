using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public class WechatOrders:ModelBase
    {

        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string TransactionID { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Out_Order_No { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubCharge { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime TranTime { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string PayState { get; set; }

        [Column(TypeName = "varchar(256)")]
        public string PayDescription { get; set; }
    }
}
