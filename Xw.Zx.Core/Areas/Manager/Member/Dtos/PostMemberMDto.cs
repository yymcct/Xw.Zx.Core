using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Manager
{
    public class PostMemberMDto
    {
        public string RealName { get; set; }

        public string BusinessCode { get; set; }

        public string IdentityCardNum { get; set; }

        public string Address { get; set; }
        //备注
        public string Remark { get; set; }

        public MemberVipType MemberVipType { get; set; }
    }
}
