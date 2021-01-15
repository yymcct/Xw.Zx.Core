using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public class WechatSubLedgerReceivers : ModelBase
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string SubType { get; set; }

        [Column(TypeName = "varchar(50)")]

        public string Account { get; set; }
        [Column(TypeName = "varchar(50)")]

        public string Name { get; set; }
        [Column(TypeName = "varchar(256)")]
        public string Describe { get; set; }
    }
}
