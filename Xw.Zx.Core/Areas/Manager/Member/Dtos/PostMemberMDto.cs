using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Areas.Manager
{
    public class PostMemberMDto
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
        public DateTime BirthDay { get; set; }
        //电话
        [Sieve(CanFilter = true)]
        public string Phone { get; set; }

        //备注
        public string Remark { get; set; }
    }
}
