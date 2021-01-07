using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    /// <summary>
    /// 碧麒麟订单日志表
    /// </summary>
    public class BiqilinLog : ModelBase_Id_CreateTime
    {
        public int OrderId { get; set; }

        public string BiqilinOrderNo { get; set; }

    }
}
