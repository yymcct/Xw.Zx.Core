using System;
using System.Collections.Generic;
using System.Text;

namespace Xw.Zx.Core.Service
{
    public interface ISmsService
    {
        bool Send(string phone, int code);
    }
}
