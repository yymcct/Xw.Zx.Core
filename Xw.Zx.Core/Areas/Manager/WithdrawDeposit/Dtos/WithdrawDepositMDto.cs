using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Manager
{
    public class WithdrawDepositMDto
    {
        [Sieve(CanFilter = true)]
        public int Id { get; set; }

        public int MemberId { get; set; }

        [Sieve(CanFilter = true)]
        public decimal Amount { get; set; }

        public DateTime AddTime { get; set; } = DateTime.Now;

        [Sieve(CanFilter = true)]
        public WithdrawDepositState WithdrawDepositState { get; set; }
        
        [Sieve(CanFilter = true)]
        public string Remark { get; set; } 

        [Sieve(CanFilter = true)]
        public string RealName { get; set; }

        [Sieve(CanFilter = true)]
        public string Phone { get; set; }

        [Sieve(CanFilter = true)]
        public string AliPayAccount { get; set; }

        public string WithdrawDepositStateName { get; set; }
    }
}
