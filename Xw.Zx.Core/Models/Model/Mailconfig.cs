using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public class Mailconfig
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public string Cookies { get; set; }

        public string Sid { get; set; }

        public DateTime AddTime { get; set; }

    }
}
