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

        //密码
        public string Password { get; set; }

        //昵称
        [Sieve(CanFilter = true)]
        public string Nick { get; set; }

        //头像
        public string Photo { get; set; }

        //生日
        public DateTime BirthDay { get; set; } = DateTime.Now;
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

        [Sieve(CanFilter = true)]
        public int? QueryTimes { get; set; }
    }
}
