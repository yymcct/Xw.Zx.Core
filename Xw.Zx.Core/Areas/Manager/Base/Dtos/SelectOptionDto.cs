using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Areas.Manager
{
    public class SelectOptionDto
    {
        public string Label { get; set; }

        public string Value { get; set; }
       
    }

    public class CascaderOptionDto: SelectOptionDto
    {
        public CascaderOptionDto()
        {
            Children = new List<SelectOptionDto>();
        }
        public List<SelectOptionDto> Children { get; set; }
    }


    public class SelectOptionIntDto
    {
        public string Label { get; set; }

        public int Value { get; set; }

    }
}
