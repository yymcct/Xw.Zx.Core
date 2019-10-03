using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public interface IUpDateVip1Service
    {
        // 生成支付宝订单
        AliPayOrderDto CreateAliPayOrder(Member member);

        void AliPayMentSucessHandle(Dictionary<string, string> sArray);
    }
}
