using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Dto
{
    public class ProductListDto
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Images { get; set; }

        /// <summary>
        /// 销量
        /// </summary>
        public string SalesVolume { get; set; }

        public decimal Price { get; set; }

    }

    public class ProductDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Images { get; set; }

        /// <summary>
        /// 销量
        /// </summary>
        public string SalesVolume { get; set; }

        public decimal Price { get; set; }

        public string Content { get; set; }

    }
}
