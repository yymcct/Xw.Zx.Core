using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public class SysParam
    {        
        public int Id { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Value { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Remark { get; set; }
    }
}
