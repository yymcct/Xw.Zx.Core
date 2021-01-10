using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
        private readonly IMemberIntegralService _memberIntegralService;
        public MemberController(ILogger<MemberController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor
            , ISmsService sms
            , IMemberIntegralService memberIntegralService) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
            _sms = sms;
            _memberIntegralService = memberIntegralService;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public HbzsResult PostRegisterUser([FromBody] RegisterUserDto user)
        {
            try
            {
                if (!CheckSms(user.Phone, user.SmsCheck))
                {
                    throw new Exception($"验证码错误!");
                }

                if (string.IsNullOrEmpty(user.RealName)) throw new Exception("请填写姓名");
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
                    RealName = user.RealName,
                    Phone = user.Phone,
                    Password = user.Password,
                    UserName = user.Phone,
                    Nick = user.Phone,
                    InviteId = InviteUser.Id,
                    Email = "",
                    QueryTimes = 0,
                    WxOpenId = user.OpenId
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
        /// 登录
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost]
        public HbzsResult<LoginResponeDto> H5([FromBody] LoginDto loginDto)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDto.Account) || string.IsNullOrEmpty(loginDto.Password))
                {
                    throw new Exception("账号或密码为空");
                }

                var result = SimulationLogin(loginDto);
                if (result.statusCode == 200)
                {
                    var member = _context.Members.FirstOrDefault(m => m.UserName == loginDto.Account);
                    result.Member = _mapper.Map<MemberDto>(member);
                }
                else
                {
                    result.msg = "账号或密码错误";
                }

                return new HbzsResult<LoginResponeDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("账号或密码错误" + ex.Message);
                return new HbzsResult<LoginResponeDto>(HbzsResultCode.Invalid_Error, "账号或密码错误");
            }
        }

        /// <summary>
        /// 检查用户是否已存在
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult<bool> AnyUserName([FromQuery] string phone)
        {
            try
            {
                var memberany = _context.Members.Any(m => m.UserName == phone && m.Disabled == false);

                return new HbzsResult<bool>(memberany);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<bool>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }
        /// <summary>
        /// 绑定微信
        /// </summary>
        /// <param name="weixinBindDto"></param>
        /// <returns></returns>
        [HttpPost]
        public HbzsResult<LoginResponeDto> WeixinBind([FromBody] WeixinBindDto weixinBindDto)
        {
            try
            {
                var memberDb = _context.Members.Where(m => m.Disabled == false);

                if (!CheckSms(weixinBindDto.Phone, weixinBindDto.SmsCheck))
                {
                    throw new Exception($"验证码错误!");
                }

                var member = memberDb.FirstOrDefault(s => s.Phone == weixinBindDto.Phone || s.WxOpenId == weixinBindDto.OpenId);
                if (member != null)
                {
                    CheckBind(member);
                    //绑定
                    member.WxOpenId = weixinBindDto.OpenId;
                    _context.SaveChanges();
                }

                var result = SimulationLogin(new LoginDto()
                {
                    Account = member.UserName,
                    Password = member.Password
                });

                if (result.statusCode != 200)
                {
                    return new HbzsResult<LoginResponeDto>(HbzsResultCode.Invalid_Error, "账号或密码错误");
                }

                result.Member = _mapper.Map<MemberDto>(member);
                return new HbzsResult<LoginResponeDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<LoginResponeDto>(HbzsResultCode.Invalid_Error, ex.Message);
            }

            void CheckBind(Member member)
            {
                if (member.WxOpenId == weixinBindDto.OpenId)
                {
                    throw new Exception($"该微信{weixinBindDto.OpenId}已绑定有用户，请先解绑. ");
                }
                else if ((!string.IsNullOrEmpty(member.WxOpenId)) && member.WxOpenId != weixinBindDto.OpenId && member.Phone == weixinBindDto.Phone)
                {
                    throw new Exception($"该手机{weixinBindDto.Phone}已绑定有用户，请先解绑.");
                }
            }
        }

        /// <summary>
        /// 微信通过code登录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public HbzsResult<LoginResponeDto> WeixinLogin([FromQuery] string code)
        {
            try
            {
                Member member = null;
                string appId = "wx87734a5a656fc8cb";//TODO重构放在配置文件中
                string secret = "43e89dc6518e9247a6e6cdc607de78af";
                var wxhelper = new WxHelper(appId, secret);
                var openId = wxhelper.Code2Accesstoken(code).openid;
                if (openId == null)
                    throw new Exception("无法找到该微信用户,请联系管理员");

                member = _context.Members.FirstOrDefault(m => m.WxOpenId == openId && m.Disabled == false);
                if (member == null)
                {
                    return new HbzsResult<LoginResponeDto>(HbzsResultCode.Sucess, openId);//把openID返回去
                }

                var result = SimulationLogin(new LoginDto()
                {
                    Account = member.UserName,
                    Password = member.Password
                });

                if (result.statusCode != 200)
                {
                    return new HbzsResult<LoginResponeDto>(HbzsResultCode.Invalid_Error, "账号或密码错误");
                }

                result.Member = _mapper.Map<MemberDto>(member);
                return new HbzsResult<LoginResponeDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("账号或密码错误" + ex.Message);
                return new HbzsResult<LoginResponeDto>(HbzsResultCode.Invalid_Error, "账号或密码错误");
            }
        }

        private LoginResponeDto SimulationLogin(LoginDto dto)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{AppsettingsUtility.AppHost}/connect/token");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            string strContent = $"username={dto.Account}&password={dto.Password}&grant_type=password&client_id=App.Manager.Ro&client_secret=DEsjpJFtokIOhMKuE6BVMczYUEEyPGTOLrur3PXw26VMLNwKOfAKFZZgR2vVJDKG";
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(strContent);
                dataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            return Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponeDto>(reader.ReadToEnd());
        }









        /// <summary>
        /// 获取邀请人电话
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult<string> GetInviteUserPhone([FromQuery] int id)
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
        public HbzsResult<List<MyTeamUserDto>> GetMyFirstTeamUser()
        {
            try
            {
                var db = _context.Members
                          .Where(m => m.Disabled == false && m.InviteId == Member.Id);

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
        /// 获取其他人的个人详情. 需Admin权限
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public HbzsResult<MemberDto> GetMember([FromQuery] int memberId)
        {
            try
            {
                var user = _context.Members
                    .First(m => m.Id == memberId && m.Disabled == false);

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
        public HbzsResult<MemberDto> PostMember([FromBody] PostUserDto postUserDto)
        {
            try
            {
                if (_context.SmsCheck.Where(p => p.Phone == Member.Phone && p.CheckState == SmsCheckState.已发送).FirstOrDefault()?.LastSendCode == postUserDto.SmsCheck)
                {
                    var smsCheck = _context.SmsCheck.Find(_context.SmsCheck.Where(p => p.Phone == Member.Phone).FirstOrDefault()?.Id);
                    smsCheck.CheckState = SmsCheckState.已验证;

                    if (_context.Members.Any(m => m.Phone == Member.Phone))
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
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public HbzsResult<bool> IsWhite()
        {
            try
            {
                if (AppsettingsUtility.CanCreateUpdateVipCodePhone.Any(p => p == Member.Phone))
                {
                    return new HbzsResult<bool>(true);
                }

                return new HbzsResult<bool>(false);
            }
            catch (Exception ex)
            {
                return new HbzsResult<bool>(HbzsResultCode.Invalid_Error, ex.Message);
            }

        }



        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult GetSmsCode([FromQuery] string phone)
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
        public HbzsResult PostChangePasswordBySmsCode([FromBody] ChangePassWordDto pass)
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

        /// <summary>
        /// 认证客户(需Admin 权限)
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public HbzsResult Identity([FromQuery] int memberId)
        {
            try
            {
                var member = _context.Members.First(m => m.Id == memberId);
                member.MemberIdentityState = MemberIdentityState.已认证;
                _context.SaveChanges();
                return new HbzsResult("已认证");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult(HbzsResultCode.Invalid_Error, "未找到用户");
            }
        }

        /// <summary>
        /// 对已认证的客户, 取消认证(需Admin 权限)
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public HbzsResult UnIdentity([FromQuery] int memberId)
        {
            try
            {
                var member = _context.Members.First(m => m.Id == memberId);
                member.MemberIdentityState = MemberIdentityState.未认证;
                _context.SaveChanges();
                return new HbzsResult("已取消认证");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult(HbzsResultCode.Invalid_Error, "未找到用户");
            }
        }




        private bool CheckSms(string phone, int code)
        {
            var smscheckInfo = _context
                .SmsCheck
                .Where(p => p.Phone == phone && p.CheckState == SmsCheckState.已发送 && p.LastSendCode == code)
                .FirstOrDefault();
            if (smscheckInfo != null)
            {
                smscheckInfo.CheckState = SmsCheckState.已验证;
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取用户积分
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public HbzsResult<MemberIntegral> GetMemberIntegral()
        {
            return new HbzsResult<MemberIntegral>(_memberIntegralService.GetMemberIntegral(Member.Id));
        }
    }


}
