using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public class LxComputer
    {
        public int Id { get; set; }
        public DateTime AddTime { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string Phone { get; set; }
        public string BorrowCompany { get; set; }
        public string BorrowAmount { get; set; }
        public string Cycle { get; set; }
        public string CycleAmount { get; set; }
        public string RepaymentCycle { get; set; }
        public string OverdueCycle { get; set; }
        public string sourcePhone { get; set; }

    }
}
