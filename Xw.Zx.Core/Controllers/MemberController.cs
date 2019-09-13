using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public MemberController(ILogger<MemberController> logger, XwZxContext xwZxContext) : base(xwZxContext)
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
