using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Config
{
    public class IdentityServer4UserValidator : IResourceOwnerPasswordValidator
    {
        private readonly XwZxContext _context;
        public IdentityServer4UserValidator(XwZxContext context)
        {
            _context = context;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                //var password = CreateMD5(context.Password).Substring(10,14);
                var member = _context.Members.Where(m => (m.UserName == context.UserName || m.Phone==context.UserName) && m.Password == context.Password).FirstOrDefault();
                if (member != null)
                {
                    var t = new Dictionary<string, object>();
                    t.Add("statusCode", 200);
                    t.Add("msg", "登录成功");
                    t.Add("id", member.Id);
                    context.Result = new GrantValidationResult(subject: member.Id.ToString()
                        , authenticationMethod: "custom",
                        claims: new List<Claim>() { new Claim(JwtClaimTypes.Role, member.RoleName) }, "", t);
                }
                else
                {
                    var t = new Dictionary<string, object>();
                    t.Add("statuscode", "401");
                    t.Add("msg", "密码错误");
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential~~~", t);
                }
            }
            catch (Exception ex)
            {
                var t = new Dictionary<string, object>();
                t.Add("statuscode", "401");
                t.Add("msg", ex.Message);
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential~~~", t);
            }
            return Task.FromResult(0);
        }

        private string CreateMD5(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.Default.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
    }
}
