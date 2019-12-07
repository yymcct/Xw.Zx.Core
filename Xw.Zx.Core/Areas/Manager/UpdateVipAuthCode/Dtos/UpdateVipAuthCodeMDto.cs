using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Manager
{
    public class UpdateVipAuthCodeMDto
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        public int UsedMemberId { get; set; } = 0;

        public DateTime UsedTime { get; set; }

        public string Code { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime ExpiesTime { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public UpdateVipAuthCodeState UPdateVipAuthCodeState { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Remark { get; set; }

        public DateTime AddTime { get; set; } = DateTime.Now;

        [Sieve(CanFilter = true, CanSort = true)]
        public string UsedMemberName { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string UsedMemberPhone { get; set; }

        public string UPdateVipAuthCodeStateName { get; set; }
    }
}
