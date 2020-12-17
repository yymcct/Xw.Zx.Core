using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public class MemberService : IMemberService
    {
        private readonly ILogger<MemberService> _logger;
        public readonly XwZxContext _context;

        public MemberService(ILogger<MemberService> logger
            , XwZxContext xwZxContext)
        {
            _logger = logger;
            _context = xwZxContext;
        }

        public Member GetInviterMember(int memberId)
        {
            var InviteId = _context.Members
                .Where(m => m.Id == memberId)
                .Select(m => m.InviteId);

            var member = _context.Members
                .FirstOrDefault(m => InviteId.Contains(m.Id));

            return member;
        }

        public Member GetYunyinzhongxinMember(Member member)
        {
            if (member.InviteId == 0) return null;

            var tmpMember = member;
            do
            {
                tmpMember = _context.Members.Find(tmpMember.InviteId);

                if (tmpMember.MemberVipType == MemberVipType.运营中心)
                {
                    return tmpMember;
                }

            } while (tmpMember.InviteId != 0);

            return null;
        }
    }
}
