using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    /// <summary>
    /// 优惠券表
    /// </summary>
    public class Coupon : ModelBase_Id_CreateTime
    {

        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Money { get; set; } = 0;

        [Column(TypeName = "nvarchar(500)")]
        public string Remark { get; set; }

        /// <summary>
        /// 合计数量
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 当前数量
        /// </summary>
        public int CurCount { get; set; }
    }
}
