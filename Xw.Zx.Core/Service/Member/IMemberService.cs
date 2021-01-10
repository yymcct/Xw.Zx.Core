using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public interface IMemberService
    {
        MemberDto GetMember(int memberId);

        MemberDto GetMemberByUserName(string username);

        Member GetInviterMember(int memberId);

        Member GetYunyinzhongxinMember(Member member);
    }
}
