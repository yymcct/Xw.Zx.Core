using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    /// <summary>
    /// 中信订单日志表
    /// </summary>
    public class CiticbankLog : ModelBase_Id_CreateTime
    {     
        [Column(TypeName = "varchar(21)")]
        public string Timestamp { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string UUID { get; set; }

        [Column(TypeName = "varchar(12)")]
        public string MchId { get; set; }       
    }
}
