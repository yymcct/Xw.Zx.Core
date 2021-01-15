using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Fuwu.Dtos
{
    public class ProductRespone
    {
        public class Product
        {
            public int Id { get; set; }

            public string Name { get; set; } 

            public decimal Price { get; set; }

            public bool Check { get; set; }


        }
    }
}
