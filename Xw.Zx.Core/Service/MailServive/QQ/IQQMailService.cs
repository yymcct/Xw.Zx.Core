using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public interface IQQMailService: IMailService
    {
        IMailService Init(string sid, string cookie);
    }
}
