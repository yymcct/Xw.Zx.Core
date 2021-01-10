using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Helper
{
    public class ZzzException : ApplicationException
    {  
        public ZzzException()
        {

        }

        public ZzzException(string message) : base(message)
        {

        }

        public ZzzException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
