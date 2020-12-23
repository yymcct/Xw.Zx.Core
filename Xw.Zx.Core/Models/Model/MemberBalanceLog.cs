using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public enum MemberMoneySource
    { 
        分润 = 0,
        提现 = 10
    }
    public class MemberBalanceLog : ModelBase_Id_CreateTime
    {
        public int Memberid { get; set; }

        public MemberMoneySource memberMoneySource { get; set; }

        public int SourceId { get; set; }

        /// <summary>
        /// 变动金额
        /// </summary>
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal OriginalMoney { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal CurMoney { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Remark { get; set; }
    }
}
