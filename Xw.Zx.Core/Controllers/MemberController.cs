using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xw.Zx.Core.Helper;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberController : BaseController
    {
        private readonly ILogger<MemberController> _logger;
        public MemberController(ILogger<MemberController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public HbzsResult PostRegisterUser([FromBody]RegisterUserDto user)
        {
            try
            {
                if (!ValidateHelper.IsMobile(user.Phone)) throw new Exception("请填写手机号");
                if (string.IsNullOrEmpty(user.Password)) throw new Exception("请填写密码");
                if (_context.Members.Any(p => p.Phone == user.Phone)) throw new Exception("请勿重复注册");
                if (_context.Members.Any(m => m.Email == user.Mail)) throw new Exception("邮箱已被占用");

                var InviteUser = _context.Members.FirstOrDefault(m => m.Id == user.InviteId && m.Disabled == false);
                if (InviteUser == null)
                {
                    InviteUser = _context.Members.FirstOrDefault(m => m.Phone == user.InvitePhone && m.Disabled == false);
                }
                if (InviteUser == null) throw new Exception("邀请人无效!");

                var member = _context.Members.Add(new Member()
                {
                    Phone = user.Phone,
                    Password = user.Password,
                    UserName = user.Phone,
                    Nick = user.Phone,
                    InviteId = InviteUser.Id,
                    Photo = CommonHelper.GetMemberPhoto()
                });
                _context.SaveChanges();

                return new HbzsResult(HbzsResultCode.Sucess);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// 获取邀请人电话
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult<string> GetInviteUserPhone([FromQuery]int id)
        {
            try
            {
                var user = _context.Members
                    .FirstOrDefault(m => m.Id == id && m.Disabled == false);
                if (user == null) throw new Exception("邀请人无效");

                var phone = Regex.Replace(user.Phone, "(\\d{3})\\d{4}(\\d{4})", "$1****$2");

                return new HbzsResult<string>(phone);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<string>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// 获取我的团队
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public HbzsResult<MyTeamDto> GetMyTeam()
        {


            try
            {
                MyTeamDto myTeamDto = new MyTeamDto();
                myTeamDto.UserTotal = GetFirstChildCnt(Member.Id) + GetSecondChildCnt(Member.Id);

                myTeamDto.DayTotal = GetFirstChildCnt(Member.Id, ChildCntDay.今天) + GetSecondChildCnt(Member.Id, ChildCntDay.今天);

                myTeamDto.MonthTotal = GetFirstChildCnt(Member.Id, ChildCntDay.本月) + GetSecondChildCnt(Member.Id, ChildCntDay.本月);

                return new HbzsResult<MyTeamDto>(myTeamDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<MyTeamDto>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// 获取未绑定银行卡的用户
        /// filter:0 全部 1:待绑卡
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public HbzsResult<List<MyTeamUserDto>> GetMyFirstTeamUser(int memberId, int filter)
        {
            try
            {
                var db = _context.Members
                          .Where(m => m.Disabled == false && m.InviteId == memberId);

                if (filter == 1)
                {
                    db = db.Where(m => _context.BankCards.Any(b => b.Disabled == false && b.MemberId == m.Id) == false);
                }

                var users = _mapper.Map<List<MyTeamUserDto>>(db.ToList());

                users = users.Select(u =>
                {
                    u.FirstChildCnt = GetFirstChildCnt(u.Id);
                    u.SecondChildCnt = GetSecondChildCnt(u.Id);
                    return u;
                }).ToList();

                return new HbzsResult<List<MyTeamUserDto>>(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<List<MyTeamUserDto>>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }




        private enum ChildCntDay
        {
            所有 = 0,
            今天 = 1,
            本月 = 2
        }
        private int GetFirstChildCnt(int memberID, ChildCntDay childCntDay = ChildCntDay.所有)
        {
            DateTime dtToday = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));//今天
            DateTime dtNexDay = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));//明天
            DateTime dtWeekDay = Convert.ToDateTime(DateTime.Now.AddDays(7).ToString("yyyy-MM-dd"));//一周
            DateTime dtMonthFirstday = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month, 1).ToString("yyyy-MM-dd"));//本月
            DateTime dtThreeMonth = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-3).Month, 1).ToString("yyyy-MM-dd"));//3个月
            DateTime dtYearFirstday = Convert.ToDateTime(new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd"));//今年

            var db = _context.Members.Where(m => m.Disabled == false && m.InviteId == memberID);
            if (childCntDay == ChildCntDay.今天)
            {
                db = db.Where(m => m.CreateDate >= dtToday && m.CreateDate < dtNexDay);
            }

            if (childCntDay == ChildCntDay.本月)
            {
                db = db.Where(m => m.CreateDate >= dtToday && m.CreateDate < dtMonthFirstday);
            }


            return db.Count();
        }

        private int GetSecondChildCnt(int memberId, ChildCntDay childCntDay = ChildCntDay.所有)
        {
            DateTime dtToday = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));//今天
            DateTime dtNexDay = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));//明天
            DateTime dtWeekDay = Convert.ToDateTime(DateTime.Now.AddDays(7).ToString("yyyy-MM-dd"));//一周
            DateTime dtMonthFirstday = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month, 1).ToString("yyyy-MM-dd"));//本月
            DateTime dtThreeMonth = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-3).Month, 1).ToString("yyyy-MM-dd"));//3个月
            DateTime dtYearFirstday = Convert.ToDateTime(new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd"));//今年

            var cnt = 0;
            var first = _context.Members.Where(m => m.Disabled == false && m.InviteId == memberId).ToList();
            foreach (var f in first)
            {
                cnt += GetFirstChildCnt(f.Id, childCntDay);
            }
            return cnt;
        }

        /// <summary>
        /// 获取个人信息, 需要认证
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public HbzsResult<MemberDto> GetSelf()
        {
            try
            {
                var user = _context.Members
                    .First(m => m.Id == Member.Id && m.Disabled == false);
                var memberDto = new MemberDto()
                {
                    Id = user.Id,
                    MemberVipType = user.MemberVipType,
                    Phone = user.Phone,
                    InviteId = user.InviteId
                };

                if (user.InviteId != 0)
                {
                    var inviteUser = _context.Members
                       .FirstOrDefault(m => m.Id == user.InviteId && m.Disabled == false);
                    if (inviteUser != null)
                    {
                        memberDto.InvitePhone = user.Phone;
                    }
                }
                return new HbzsResult<MemberDto>(memberDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<MemberDto>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

    }


}
