using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Areas.Manager
{
    #region  从商城 哪个 后台 查询 订单的 Dto
    public class GetThirdQueryOrdersRequestPsDto
    { 
         public DateTime? StartTime { get; set; }
         public DateTime? EndTime { get; set; }
    }
    public class GetThirdQueryOrdersResultDto
    {
        public string data { get; set; }
        public List<GetThirdQueryOrdersDto> content { get; set; }
        public int page { get; set; }
        public int nowpage { get; set; }
    }

    public class GetThirdQueryOrdersDto
    {
        public string id { get; set; }
        public string ordersn { get; set; }
        public decimal price { get; set; }
        public string createtime { get; set; }
        public string title { get; set; }
        public string transid { get; set; }
        public string openid { get; set; }
        public string total { get; set; }
        public string status { get; set; }
        public string remark { get; set; }
        public string paytime { get; set; }
    }
    #endregion
}
