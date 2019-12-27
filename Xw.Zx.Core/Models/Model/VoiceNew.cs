using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public class VoiceNew
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Source { get; set; } = "";

        public string Duration { get; set; } = "";

        public bool NeedVip { get; set; } = false;

        public DateTime AddTime { get; set; } = DateTime.Now;
    }
}
