using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Service
{
    
    public interface IBiqilinService
    {    
        string CreateQrcodePayUrl(Biqilin_Product biqilin_Product);

        string CreateWeixinJsApi(Biqilin_Product biqilin_Product);
    }
}
