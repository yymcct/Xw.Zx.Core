using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    /// <summary>
    /// 中信商户号
    /// </summary>
    //[Table("OrderCiticbankMchId")]
    public class CiticbankMchId : ModelBase_Id_CreateTime
    {
        public int MemberId { get; set; }

        [Column(TypeName = "varchar(12)")]
        public string MchId { get; set; }

    }
}
