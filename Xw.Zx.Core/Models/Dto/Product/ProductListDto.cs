using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Dto
{
    public class ProductListDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Images { get; set; }

        /// <summary>
        /// 销量
        /// </summary>
        public string SalesVolume { get; set; }

        public decimal Price { get; set; }

    }
}
