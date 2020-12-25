


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;
using Microsoft.EntityFrameworkCore;
using Xw.Zx.Core.Models.Model;
using AutoMapper.QueryableExtensions;

namespace Xw.Zx.Core.Areas.Manager
{

    [ApiController]
    [Route("manager/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class MemberController : ManagerBaseController
    {
        private readonly ILogger<MemberController> _logger;

        public MemberController(ILogger<MemberController> logger
            , XwZxContext context
            , IMapper mapper
            , ISieveProcessor sieveProcessor) : base(context, mapper, sieveProcessor)
        {
            _logger = logger;
        }
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HbzsManagerResult<ManagerMemberDto> GetUser()
        {
            try
            {
                var member = _mapper.Map<ManagerMemberDto>(Member);

                return new HbzsManagerResult<ManagerMemberDto>(member);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult<ManagerMemberDto>(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsManagerResult<MemberMDto> GetMember([FromQuery] int memberId)
        {
            try
            {
                var member = _context.Members.First(m => m.Id == memberId);

                var memberDto = _mapper.Map<MemberMDto>(member);

                var inviteMemer = _context.Members.First(m => m.Id == member.InviteId);

                memberDto.InviteName = inviteMemer.RealName;
                memberDto.InvitePhone = inviteMemer.Phone;

                return new HbzsManagerResult<MemberMDto>(memberDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult<MemberMDto>(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HbzsManagerResult<List<MemberMDto>> GetMembers([FromQuery] SieveModel sieveModel)
        {
            try
            {
                var db = from member in _context.Members
                         select new MemberMDto
                         {
                             Id = member.Id,
                             RoleName = member.RoleName,
                             Phone = member.Phone,
                             WxOpenId = member.WxOpenId,
                             CreateDate = member.CreateDate,
                             Remark = member.Remark,
                             RealName = member.RealName,
                             MemberVipType = member.MemberVipType,
                             MemberVipTypeName = member.MemberVipType.ToString(),
                             AliPayAccount = member.AliPayAccount,
                             BusinessCode = member.BusinessCode,
                             Address = member.Address,
                             InviteId = member.InviteId,
                             InviteName = _context.Members.FirstOrDefault(m => m.Id == member.InviteId).RealName,
                             InvitePhone = _context.Members.FirstOrDefault(m => m.Id == member.InviteId).Phone
                         };

                var list = _sieveProcessor.Apply(sieveModel, db).ToList();
                var total = _sieveProcessor.Apply(sieveModel, db, null, true, true, false).Count();
                return new HbzsManagerResult<List<MemberMDto>>(list, total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult<List<MemberMDto>>(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// 修改会员信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="membermdto"></param>
        /// <returns></returns>
        [HttpPost]
        public HbzsManagerResult PostMember([FromQuery] int id, [FromBody] PostMemberMDto membermdto)
        {
            var member = _context.Members.First(m => m.Id == id);

            member.RealName = membermdto.RealName;
            member.BusinessCode = membermdto.BusinessCode;
            member.IdentityCardNum = membermdto.IdentityCardNum;
            member.Remark = membermdto.Remark;
            member.MemberVipType = membermdto.MemberVipType;
            member.Address = membermdto.Address;

            _context.Entry(member).State = EntityState.Modified;

            _context.SaveChanges();

            //TODO 数据库记录变更

            return new HbzsManagerResult(HbzsManagerResultCode.Sucess, "");
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsManagerResult DeleteMember([FromQuery] int id)
        {
            try
            {
                var meetingarea = _context.Members.Find(id);
                if (meetingarea != null)
                {
                    meetingarea.IsDelete = true;
                    _context.SaveChanges();
                }
                return new HbzsManagerResult(HbzsManagerResultCode.Sucess, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public HbzsManagerResult PostChangeInvite([FromBody] PostChangeInviteDto dto)
        {
            try
            {
                if (!_context.Members.Any(m => m.Id == dto.InviteId))
                {
                    return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, "邀请人不存在!");
                }

                var member = _context.Members.First(m => m.Id == dto.MemberId);
                member.InviteId = dto.InviteId;
                _context.SaveChanges();

                return new HbzsManagerResult(HbzsManagerResultCode.Sucess, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }


        /// <summary>
        /// 获取直属上级,和团队长
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HbzsManagerResult<IEnumerable<MemberMDto>> GetParent(int memberId)
        {
            try
            {
                var parentMembers = new List<MemberMDto>();

                var member = _context.Members.FirstOrDefault(m => m.Id == memberId);

                if (member != null)
                {
                    parentMembers.Add(_mapper.Map<MemberMDto>(member));

                    while (true)
                    {
                        var tmpMember = _context.Members.FirstOrDefault(m => m.Id == member.InviteId && m.Id != 6);

                        if (tmpMember == null) break;

                        parentMembers.Add(_mapper.Map<MemberMDto>(tmpMember));

                        member = tmpMember;
                    }
                }

                parentMembers.Reverse();

                return new HbzsManagerResult<IEnumerable<MemberMDto>>(parentMembers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult<IEnumerable<MemberMDto>>(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }

        public IEnumerable<MemberTreeNodeDto> GetBrother(int memberId)
        {
            if (memberId == 0)
                return null;

            var inviteId = _context.Members
                .Where(m => m.Id == memberId)
                .Select(m => m.InviteId)
                .FirstOrDefault();

            var member = _context
                .Members
                .Where(m => m.InviteId == inviteId)
                .ProjectTo<MemberTreeNodeDto.Member>(_mapper.ConfigurationProvider)
                .ToArray();

            var nodes = member.Select(m =>
            {
                var node = new MemberTreeNodeDto()
                {
                    Members = m
                };
                if (node.Members.Id == memberId)
                {
                    node.Members.IsDirectLine = true;
                }
                node.Members.MemberVipTypeName = node.Members.MemberVipType.ToString();

                return node;
            }).OrderByDescending(n => n.Members.IsDirectLine).ToArray();

            return nodes;
        }

        [HttpGet]
        public HbzsManagerResult<IEnumerable<MemberTreeNodeDto>> ParentTree(int memberId)
        {
            IEnumerable<MemberTreeNodeDto> tree = null;
            while (true)
            {
                IEnumerable<MemberTreeNodeDto> nodes = GetBrother(memberId);
                if (nodes == null)
                    break;

                if (tree == null)
                {
                    tree = nodes;
                }
                else
                {
                    var diectLineNode = nodes.First(m => m.Members.IsDirectLine);
                    diectLineNode.Children = tree;
                    diectLineNode.Members.IsDirectLine = true;
                    tree = nodes;
                }
                memberId = tree.First().Members.InviteId;
            }

            return new HbzsManagerResult<IEnumerable<MemberTreeNodeDto>>(tree);
        }

        [HttpGet]
        public HbzsManagerResult<IEnumerable<MemberTreeNodeDto>> ChildrenTree(int memberId)
        {
            var member = _context
                 .Members
                 .Where(m => m.InviteId == memberId)
                 .ProjectTo<MemberTreeNodeDto.Member>(_mapper.ConfigurationProvider)
                 .ToArray();

            var nodes = member.Select(m =>
            {
                var node = new MemberTreeNodeDto()
                {
                    Members = m
                };

                node.Members.MemberVipTypeName = node.Members.MemberVipType.ToString();

                return node;
            }).OrderByDescending(n => n.Members.IsDirectLine).ToArray();

            return new HbzsManagerResult<IEnumerable<MemberTreeNodeDto>>(nodes);
        }

        [HttpGet]
        public HbzsManagerResult<IEnumerable<QueryMemberDto>> QueryMember(string key)
        {
            var result = _context.Members
                  .Where(m => (m.Disabled == false) && (m.Phone.Contains(key) || m.RealName.Contains(key)))
                  .ProjectTo<QueryMemberDto>(_mapper.ConfigurationProvider)
                  .ToArray();

            return new HbzsManagerResult<IEnumerable<QueryMemberDto>>(result);
        }
    }
}