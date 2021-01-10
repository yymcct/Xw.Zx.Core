using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public class MemberService : IMemberService
    {
        private readonly ILogger<MemberService> _logger;
        public readonly XwZxContext _context;
        public readonly IMapper _mapper;
        private readonly IMemberIntegralService _memberIntegralService;

        public MemberService(ILogger<MemberService> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , IMemberIntegralService memberIntegralService)
        {
            _logger = logger;
            _context = xwZxContext;
            _mapper = mapper;
            _memberIntegralService = memberIntegralService;
        }

        public MemberDto GetMember(int memberId)
        {
            var member = _context.Members.FirstOrDefault(m => m.Id == memberId);

            return ToMemberDto(member);
        }
        public MemberDto GetMemberByUserName(string username)
        {
            var member = _context.Members.FirstOrDefault(m => m.UserName == username);

            return ToMemberDto(member);
        }
        private MemberDto ToMemberDto(Member member)
        {
            if (member == null) return null;

            var dto = _mapper.Map<MemberDto>(member);

            dto.MemberIntegral = _memberIntegralService.GetMemberIntegral(dto.Id).AvailableIntegrals;

            if (dto.InviteId != 0)
            {
                var inviteUser = _context.Members
                   .FirstOrDefault(m => m.Id == dto.InviteId && m.Disabled == false);
                if (inviteUser != null)
                {
                    dto.InvitePhone = inviteUser.Phone;
                    dto.InviteName = inviteUser.RealName;
                }
            }

            return dto;
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
