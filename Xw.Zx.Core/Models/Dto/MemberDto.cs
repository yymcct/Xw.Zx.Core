using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Models.Dto
{
    public class LoginDto
    {
        public string Account { get; set; }

        public string Password { get; set; }
    }

    public class LoginResponeDto
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public int statusCode { get; set; }
        public string msg { get; set; }
        public MemberDto Member { get; set; }
    }

    public class MemberDto
    {
        public int Id { get; set; }

        public MemberVipType MemberVipType { get; set; }

        public string MemberVipTypeName { get; set; }

        public string Phone { get; set; }

        /// <summary>
        /// 邀请人ID
        /// </summary>
        public int InviteId { get; set; } = 0;

        public string InvitePhone { get; set; }

        public string RealName { get; set; }

        public string CityCode { get; set; }

        public string Address { get; set; }

        public string AliPayAccount { get; set; }

        public string Email { get; set; }

        public int? QueryTimes { get; set; }

        public string WxOpenId { get; set; }

        public string IdentityCardNum { get; set; }

        public string IdentityCardImgFront { get; set; }

        public string IdentityCardImgReverse { get; set; }

        public int MemberIntegral { get; set; }

    }
    public class MyTeamDto
    {
        public int UserTotal { get; set; }
        public int DayTotal { get; set; }
        public int MonthTotal { get; set; }
    }
    public class MyTeamUserDto
    {
        public int Id { get; set; }
        public MemberVipType MemberVipType { get; set; }
        public string MemberVipTypeName { get; set; }
        public string Phone { get; set; }
        public string RealName { get; set; }
        //一代
        public int FirstChildCnt { get; set; }
        public int SecondChildCnt { get; set; }
        //二代
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }

    public class RegisterUserDto
    {
        public string RealName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public int InviteId { get; set; } = 0;
        public string InvitePhone { get; set; }

        public string OpenId { get; set; }

        public int SmsCheck { get; set; }
    }


    public class PostUserDto
    {
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 支付宝账号
        /// </summary>
        public string AliAccount { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdentityCardNum { get; set; }

        /// <summary>
        /// 身份证正面照 存照片的url:形如 /UpLoad/IdentityCard/20202/1.jpg
        /// </summary>
        public string IdentityCardImgFront { get; set; } = "";

        /// <summary>
        /// 身份证反面照 
        /// </summary>
        public string IdentityCardImgReverse { get; set; } = "";

        public int SmsCheck { get; set; }
    }

    public class ChangePassWordDto
    {
        public string Phone { get; set; }
        public string NewPassword { get; set; }
        public int SmsCheck { get; set; }
    }
}
