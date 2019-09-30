using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public interface IQQMailService
    {
        void Init(string sid, string cookie);
        Task<MailSrc> GetMail(string mailid);

        Task<List<string>> SearchByZhaoshang();
        Task<List<string>> SearchByGuangfa();
        Task<List<string>> SearchByZhongxin();
    }
}
