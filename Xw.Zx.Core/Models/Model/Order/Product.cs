using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public class Product : ModelBase
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        public string Name { get; set; } = "";

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; } = 0;

        public string Images { get; set; } = "";

        public string Content { get; set; } = "";

        public bool CanUseMemberIntegral { get; set; }

        /// <summary>
        /// 销量
        /// </summary>
        public int SalesVolume { get; set; } = 0;

        public bool Check { get; set; } = true;
    }
}
