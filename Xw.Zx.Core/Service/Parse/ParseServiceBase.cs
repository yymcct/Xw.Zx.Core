using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service.Parse
{
    public abstract class ParseServiceBase
    {
        protected readonly XwZxContext _xwZxContext;
        protected readonly BankCardType _ThisBankCardType;
        public ParseServiceBase(XwZxContext xwZxContext
            , BankCardType bankCardType)
        {
            _xwZxContext = xwZxContext;
            _ThisBankCardType = bankCardType;
        }
    }
}
