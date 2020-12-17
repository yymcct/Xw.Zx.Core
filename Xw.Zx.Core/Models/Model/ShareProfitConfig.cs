using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public class ShareProfitConfig : ModelBase
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int ShareProfitTemplateId { get; set; }
    }
}
