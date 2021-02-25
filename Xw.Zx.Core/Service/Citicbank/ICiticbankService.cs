using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public interface ICiticbankService
    {
        /// <summary>
        /// 生成当面付二维码
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        CiticbankDto.Qrcode CreateQrcodePayUrl(CiticbankDto.Product product);

        /// <summary>
        /// 生成jsapi支付的原生js字符串
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        string CreateJSApiPayInfo(CiticbankDto.Product product);

        string GetMemberMchid(int memberid);

        bool Query(string Timestamp);

        Hashtable QueryFull(string Timestamp);

        /// <summary>
        /// 获取所有付款为中信,但未付款的订单
        /// </summary>
        /// <returns>订单timestamp</returns>
        string[] GetAllCiticbankUnPayOrder();

        void AddCiticbankLog(CiticbankLog citicbankLog);


    }
}
