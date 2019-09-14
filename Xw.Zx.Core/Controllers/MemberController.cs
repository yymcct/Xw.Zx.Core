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
            DateTime dtToday = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));//今天
            DateTime dtNexDay = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));//明天
            DateTime dtWeekDay = Convert.ToDateTime(DateTime.Now.AddDays(7).ToString("yyyy-MM-dd"));//一周
            DateTime dtMonthFirstday = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month, 1).ToString("yyyy-MM-dd"));//本月
            DateTime dtThreeMonth = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-3).Month, 1).ToString("yyyy-MM-dd"));//3个月
            DateTime dtYearFirstday = Convert.ToDateTime(new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd"));//今年

            try
            {
                MyTeamDto myTeamDto = new MyTeamDto();
                myTeamDto.UserTotal = _context.Members
                    .Where(m => m.Disabled == false && m.InviteId == Member.Id)
                    .Count();

                myTeamDto.DayTotal = _context.Members
                              .Where(m => m.Disabled == false && m.InviteId == Member.Id && m.CreateDate >= dtToday && m.CreateDate < dtNexDay)
                              .Count();

                myTeamDto.MonthTotal = _context.Members
                          .Where(m => m.Disabled == false && m.InviteId == Member.Id && m.CreateDate >= dtToday && m.CreateDate < dtMonthFirstday)
                          .Count();

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
        /// filter:0 全部 1:待注册
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public HbzsResult<List<MyTeamUserDto>> GetMyTeamNoCardUser(int filter)
        {
            try
            {
                var db = _context.Members
                          .Where(m => m.Disabled == false && m.InviteId == Member.Id);

                if (filter == 1)
                {
                    db = db.Where(m => _context.BankCards.Any(b => b.Disabled == false && b.MemberId == m.Id) == false);
                }

                var users = _mapper.Map<List<MyTeamUserDto>>(db.ToList());

                return new HbzsResult<List<MyTeamUserDto>>(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<List<MyTeamUserDto>>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }
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
