using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public enum ApplyForZxState
    {
        待处理 = 0,
        处理中 = 1,
        已完结 = 2
    }
    /// <summary>
    /// 追息申请表
    /// </summary>
    public class ApplyForZx
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        public int MemberId { get; set; }

        public string Remark { get; set; } = "";

        [Sieve(CanFilter = true, CanSort = true)]
        public ApplyForZxState ApplyForZxState { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime AddTime { get; set; } = DateTime.Now;

    }
}
