using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Areas.Manager.Receivables.Dtos
{
    public class ReceivablesMDto
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public decimal Amount { get; set; }

        public DateTime AddTime { get; set; } = DateTime.Now;
    }
}
