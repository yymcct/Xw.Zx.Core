using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public enum MemberSex
    {
        男 = 1,
        女 = 2,
    }
    public enum MemberVipType
    {
        普通 = 0,
        Vip会员 = 1,
        合伙人 = 2,
        服务站 = 3,
        运营商 = 4,

    }
    public class Member
    {
        public int Id { get; set; }
        /// <summary>
        /// 邀请人ID, 上线
        /// </summary>
        public int InviteId { get; set; } = 0;

        public MemberVipType MemberVipType { get; set; } = MemberVipType.普通;

        public int RoleId
        { get; set; } = 0;

        public string RoleName { get; set; } = "AppUser";

        //用户名
        public string UserName { get; set; }
        //密码
        public string Password { get; set; }
        //密码加盐
        public string PasswordSalt { get; set; }
        //真实姓名
        public string RealName { get; set; }
        //昵称
        public string Nick { get; set; }
        //头像
        public string Photo { get; set; }
        //性别
        public MemberSex Sex { get; set; } = MemberSex.男;
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
        //省份ID
        public int TopRegionId { get; set; }
        //省市区ID
        public int RegionId { get; set; }

        //城市码
        public string CityCode { get; set; } = "";
        //街道地址
        public string Address { get; set; }

        //微信OpendID
        public string WxOpenId { get; set; }

        //开发平台Openid
        public string WxUnionOpenId { get; set; }
        //开发平台Unionid
        public string WxUnionId { get; set; }

        //创建日期
        public DateTime CreateDate { get; set; } = DateTime.Now;

        //是否已删除
        public bool Disabled { get; set; } = false;
        //最后登录日期
        public DateTime LastLoginDate { get; set; }
        //备注
        public string Remark { get; set; }
    }
}
