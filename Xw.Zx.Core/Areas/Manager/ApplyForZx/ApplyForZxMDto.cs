using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Manager
{

    public class ApplyForZxMDto
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        public int MemberId { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Remark { get; set; } = "";

        [Sieve(CanFilter = true, CanSort = true)]
        public ApplyForZxState ApplyForZxState { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime AddTime { get; set; } = DateTime.Now;
        [Sieve(CanFilter = true, CanSort = true)]
        public string MemberName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string MemberPhone { get; set; }

        public string ApplyForZxStateName { get; set; }

    }
}
