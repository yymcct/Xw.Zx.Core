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

        public string Phone { get; set; }

        public int InviteId { get; set; } = 0;

        public string InvitePhone { get; set; }

        public string RealName { get; set; }

        public string CityCode { get; set; }

        public string Address { get; set; }

        public string AliPayAccount { get; set; }

        public string Email { get; set; }

        public int? QueryTimes { get; set; } 

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
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public int InviteId { get; set; } = 0;
        public string InvitePhone { get; set; }
    }


    public class PostUserDto
    {
        public string RealName { get; set; }



        public string AliAccount { get; set; }



        public int SmsCheck { get; set; }

    }

    public class ChangePassWordDto
    {
        public string Phone { get; set; }
        public string NewPassword { get; set; }
        public int SmsCheck { get; set; }
    }
}
