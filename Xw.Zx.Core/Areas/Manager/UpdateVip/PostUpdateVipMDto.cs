using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Manager.UpdateVip
{
    public class PostUpdateVipMDto
    {
        public int MemberId { get; set; }

        public MemberVipType MemberVipType { get; set; }
    }
}
