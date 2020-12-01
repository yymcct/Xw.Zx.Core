using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public class LxComputer : ModelBase
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime AddTime { get; set; } = DateTime.Now;

        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Phone { get; set; } = "";
        public string BorrowCompany { get; set; } = "";
        public string BorrowAmount { get; set; } = "";
        public string Cycle { get; set; } = "";
        public string CycleAmount { get; set; } = "";
        public string RepaymentCycle { get; set; } = "";
        public string OverdueCycle { get; set; } = "";

        [Sieve(CanFilter = true, CanSort = true)]
        public string sourcePhone { get; set; }
        public string MinReduce { get; set; } = "0";
        public string MaxReduce { get; set; } = "0";
    }
}
