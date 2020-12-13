using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Manager
{
    public class MemberTreeNodeDto
    {
        public class Member
        {
            public int Id { get; set; }

            public string Phone { get; set; }

            public string RealName { get; set; }

            public int InviteId { get; set; }

            public MemberVipType MemberVipType { get; set; }

            public string MemberVipTypeName { get; set; }

            public bool IsDirectLine { get; set; } = false;
        }

        public Member Members { get; set; }
        public IEnumerable<MemberTreeNodeDto> Children { get; set; }
    }
}
