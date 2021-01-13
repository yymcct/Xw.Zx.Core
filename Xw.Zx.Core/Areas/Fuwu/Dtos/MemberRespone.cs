using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Fuwu.Dtos
{
    public class MemberRespone
    {
        public class Member
        {
            public int Id { get; set; }

            public int InviteId { get; set; } = 0;

            public MemberVipType MemberVipType { get; set; }

            public string MemberVipTypeName { get; set; }

            //public int RoleId { get; set; }

            //public string RoleName { get; set; }

            //用户名
            public string UserName { get; set; }
            //密码
            //public string Password { get; set; }
            //密码加盐
            //public string PasswordSalt { get; set; }
            //真实姓名
            public string RealName { get; set; }
            //昵称
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
            //职业
            public string Occupation { get; set; }

            //城市码
            public string CityCode { get; set; }
            //街道地址
            public string Address { get; set; }

            //微信OpendID
            public string WxOpenId { get; set; }


            //创建日期
            public DateTime CreateDate { get; set; }

            public DateTime UpdateTime { get; set; }

            //是否已删除
            public bool Disabled { get; set; } = false;

            //备注
            public string Remark { get; set; } = "";

        }
    }
}
