using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Dto
{

    public class HomeDto
    {
        public string BannerImg { get; set; }

        public List<ProductListDto> TopProducts { get; set; }

        public List<ProductListDto> HotProducts { get; set; }        
    }
}
