using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Service
{
    
    public interface ITonglianService
    {
        BiqilinRespone.QrcodePay CreateQrcodePayUrl(Biqilin_Product biqilin_Product);

        BiqilinRespone.JsapiPay CreateWeixinJsApi(Biqilin_Product biqilin_Product);

        BiqilinRespone.Query QueryOrder(string biqilinOrderNo);

    }
}
