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

        //电话
        public string Phone { get; set; }

        //街道地址
        public string Address { get; set; }

        public string RoleName { get; set; }

        public string BusinessCode { get; set; }
    }
}
