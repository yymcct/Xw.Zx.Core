using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public interface ICiticbankService
    {
        CiticbankDto.Qrcode CreateQrcodePayUrl(CiticbankDto.Product product);

        string GetMemberMchid(int memberid);

        bool Query(string Timestamp);

        /// <summary>
        /// 获取所有付款为中信,但未付款的订单
        /// </summary>
        /// <returns>订单timestamp</returns>
        string[] GetAllCiticbankUnPayOrder();

        void AddCiticbankLog(CiticbankLog citicbankLog);
    }
}
