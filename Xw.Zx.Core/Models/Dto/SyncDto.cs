using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Dto
{
    public class SyncDto
    {
    }

    public class PostSyncMailDto
    {
        public int MemberId { get; set; }
        public string Mail { get; set; }
        public string Sid { get; set; }
        public string Cookie { get; set; }
    }
}
