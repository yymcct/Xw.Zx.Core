using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Manager
{
    [AutoMap(typeof(Member))]
    public class ManagerMemberDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string RealName { get; set; }

        public string Nick { get; set; }
        //头像
        public string Photo { get; set; }
        //性别
        public MemberSex Sex { get; set; }
        //生日
        public DateTime BirthDay { get; set; }
        //电话
        public string Phone { get; set; }
        //Email
        public string Email { get; set; }
        //QQ
        public string QQ { get; set; }
        //街道地址
        public string Address { get; set; }
    }
}
