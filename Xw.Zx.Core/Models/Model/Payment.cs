using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    /// <summary>
    /// 付款表
    /// </summary>
    public class Payment
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int MemberId { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Amount { get; set; }

        public DateTime AddTime { get; set; } = DateTime.Now;

    }
}
