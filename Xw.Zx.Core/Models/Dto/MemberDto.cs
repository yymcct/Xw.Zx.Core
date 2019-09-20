using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Models.Dto
{
    public class MemberDto
    {
        public int Id { get; set; }
        public MemberVipType MemberVipType { get; set; } = MemberVipType.普通;

        public string Phone { get; set; }

        public int InviteId { get; set; } = 0;

        public string InvitePhone { get; set; }
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

}
