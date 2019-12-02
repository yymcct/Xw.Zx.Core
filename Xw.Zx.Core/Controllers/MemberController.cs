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
using Xw.Zx.Core.Service;
using Xw.Zx.Core.Utility;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberController : BaseController
    {
        private readonly ILogger<MemberController> _logger;
        private readonly ISmsService _sms;
        public MemberController(ILogger<MemberController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor
            , ISmsService sms) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
            _sms = sms;
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

                var InviteUser = _context.Members.FirstOrDefault(m => m.Id == user.InviteId && m.Disabled == false);
                if (InviteUser == null)
                {
                    InviteUser = _context.Members.FirstOrDefault(m => m.Phone == user.InvitePhone && m.Disabled == false);
                }
                if (InviteUser == null) throw new Exception("邀请人无效!");

                var model = new Member()
                {
                    Phone = user.Phone,
                    Password = user.Password,
                    UserName = user.Phone,
                    Nick = user.Phone,
                    InviteId = InviteUser.Id,
                    Email = "",
                    QueryTimes = 0,
                    Photo = CommonHelper.GetMemberPhoto()
                };

                var member = _context.Members.Add(model);
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
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public HbzsResult<MemberDto> GetSelf()
        {
            try
            {
                var user = _context.Members
                    .First(m => m.Id == Member.Id && m.Disabled == false);

                var memberDto = _mapper.Map<MemberDto>(user);

                if (user.InviteId != 0)
                {
                    var inviteUser = _context.Members
                       .FirstOrDefault(m => m.Id == user.InviteId && m.Disabled == false);
                    if (inviteUser != null)
                    {
                        memberDto.InvitePhone = inviteUser.Phone;
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

        /// <summary>
        /// 修改用户基本信息
        /// </summary>
        /// <param name="postUserDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public HbzsResult<MemberDto> PostMember([FromBody]PostUserDto postUserDto)
        {
            try
            {
                if (_context.SmsCheck.Where(p => p.Phone == postUserDto.Phone && p.CheckState == SmsCheckState.已发送).FirstOrDefault()?.LastSendCode == postUserDto.SmsCheck)
                {
                    var smsCheck = _context.SmsCheck.Find(_context.SmsCheck.Where(p => p.Phone == postUserDto.Phone).FirstOrDefault()?.Id);
                    smsCheck.CheckState = SmsCheckState.已验证;

                    if (_context.Members.Any(m => m.Phone == postUserDto.Phone))
                    {
                        var member = _mapper.Map(postUserDto, Member);
                        if (string.IsNullOrEmpty(member.AliPayAccount))
                        {
                            member.AliPayAccount = postUserDto.AliAccount.Trim();
                        }

                _context.Entry(member).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                var res = _mapper.Map<MemberDto>(member);

                        return new HbzsResult<MemberDto>(res);
                    }
                    else
                        throw new Exception("账号不存在");
                }
                else
                {
                    throw new Exception("验证码错误");
                }
            }
            catch (Exception ex)
            {
                return new HbzsResult<MemberDto>(HbzsResultCode.Invalid_Error, ex.Message);
            }

        }

        /// <summary>
        /// 检查是否是白名单用户
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public HbzsResult IsWhite()
        {
            if (AppsettingsUtility.CanCreateUpdateVipCodePhone.Any(p => p == Member.Phone))
            {
                return new HbzsResult(HbzsResultCode.Sucess);
            }

            return new HbzsResult(HbzsResultCode.Invalid_Error);
        }


        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult GetSmsCode([FromQuery]string phone)
        {
            try
            {
                if (!ValidateHelper.IsMobile(phone))
                {
                    throw new Exception("请填写手机号");
                }
                var CheckCode = new Random().Next(1000, 9999);
                _sms.Send(phone, CheckCode);
                try
                {
                    var lastcode = _context.SmsCheck.Find(_context.SmsCheck.Where(s => s.Phone == phone).FirstOrDefault().Id);
                    if (lastcode.CheckState == SmsCheckState.已验证)
                    {
                        lastcode.CheckState = SmsCheckState.已发送;
                    }
                    lastcode.LastSendCode = CheckCode;
                    lastcode.LastSendTime = DateTime.Now;
                    lastcode.SendCnt = lastcode.SendCnt + 1;
                }
                catch
                {
                    SmsCheck smsCheck = new SmsCheck();
                    smsCheck.LastSendCode = CheckCode;
                    smsCheck.LastSendTime = DateTime.Now;
                    smsCheck.CheckState = SmsCheckState.已发送;
                    smsCheck.Phone = phone;
                    _context.SmsCheck.Add(smsCheck);
                }
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
        /// 发送验证码重置密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HbzsResult PostChangePasswordBySmsCode([FromBody]ChangePassWordDto pass)
        {
            try
            { //TODO 重构
                if (!ValidateHelper.IsMobile(pass.Phone))
                {
                    throw new Exception("请填写正确手机号");
                }
                if (string.IsNullOrEmpty(pass.NewPassword))
                {
                    throw new Exception("请填写新密码");
                }

                if (_context.SmsCheck.Where(p => p.Phone == pass.Phone && p.CheckState == SmsCheckState.已发送).FirstOrDefault()?.LastSendCode == pass.SmsCheck)
                {
                    var smsCheck = _context.SmsCheck.Find(_context.SmsCheck.Where(p => p.Phone == pass.Phone).FirstOrDefault()?.Id);
                    smsCheck.CheckState = SmsCheckState.已验证;
                    if (_context.Members.Any(m => m.Phone == pass.Phone))
                    {
                        Member member = _context.Members.Where(m => m.Phone == pass.Phone).FirstOrDefault();
                        member.Password = pass.NewPassword;
                        _context.SaveChanges();
                    }
                    else
                        throw new Exception("账号不存在");
                }
                else
                {
                    throw new Exception("验证码错误");
                }
                return new HbzsResult(HbzsResultCode.Sucess);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult(HbzsResultCode.Remote_Service_Error, "修改失败");
            }
        }

    }


}
