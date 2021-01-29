using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Service.Xtsuo.Dtos;

namespace Xw.Zx.Core.Service
{
    public interface IXtsuoService
    {
        /// <summary>
        /// 同步Xtsuo系统的订单
        /// </summary>
        /// <param name="getThirdQueryOrdersRequestPsDto"></param>
        /// <returns></returns>
        bool SyncXtsuoOrders(XtsuoOrdersRequestDto getThirdQueryOrdersRequestPsDto);

        /// <summary>
        /// 查询分账状态
        /// </summary>
        /// <param name="out_order_no"></param>
        /// <returns></returns>
        void QuerySubLedgerResult(string out_order_no);

        /// <summary>
        /// 查询所有分账的订单状态
        /// </summary>
        void QuerySubLedgerResultAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sieveModel"></param>
        /// <returns></returns>
        (WechatSubLedgerDetail, int) GetWechatOrderDetails(SieveModel sieveModel);
    }
}
