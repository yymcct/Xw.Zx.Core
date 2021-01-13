using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Manager
{
    public class MemberMDto
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        [Sieve(CanFilter = true)]
        public string RoleName { get; set; }

        //电话
        [Sieve(CanFilter = true)]
        public string Phone { get; set; }

        //微信OpendID
        public string WxOpenId { get; set; }

        //创建日期
        [Sieve(CanFilter = true)]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        //备注
        [Sieve(CanFilter = true)]
        public string Remark { get; set; }

        [Sieve(CanFilter = true)]
        public string RealName { get; set; }
        [Sieve(CanFilter = true)]
        public MemberVipType MemberVipType { get; set; }

        public string MemberVipTypeName { get; set; }

        [Sieve(CanFilter = true)]
        public string AliPayAccount { get; set; }

        public int InviteId { get; set; }

        [Sieve(CanFilter = true)]
        public string InviteName { get; set; }
        [Sieve(CanFilter = true)]
        public string InvitePhone { get; set; }

        [Sieve(CanFilter = true)]
        public string BusinessCode { get; set; }

        [Sieve(CanFilter = true)]
        public string Address { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityCardNum { get; set; }
    }
}
