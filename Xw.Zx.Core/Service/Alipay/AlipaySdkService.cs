using Alipay.AopSdk.Core.Domain;
using Alipay.AopSdk.Core.Request;
using Alipay.AopSdk.Core.Response;
using Alipay.AopSdk.AspnetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Service
{
    public class AlipaySdkService : IAlipaySdkService
    {
        private readonly AlipayService _alipayService;
        public AlipaySdkService(AlipayService alipayService)
        {
            _alipayService = alipayService;
        }

        public AlipayTradeQueryResponse Query(string tradeNo)
        {
            AlipayTradeQueryModel model = new AlipayTradeQueryModel();
            model.OutTradeNo = tradeNo;

            AlipayTradeQueryRequest request = new AlipayTradeQueryRequest();

            request.SetBizModel(model);

            AlipayTradeQueryResponse response = _alipayService.Execute<AlipayTradeQueryResponse>(request);

            return response;
        }
    }
}
