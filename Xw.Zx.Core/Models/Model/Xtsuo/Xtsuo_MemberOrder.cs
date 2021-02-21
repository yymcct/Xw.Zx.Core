using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{


    public partial class Xtsuo_MemberOrder
    {
        //id, openid, agentid, ordersn,price,status,paytype,paytime,createtime
        public int Id { get; set; }


        //0 默认等级
        //8 测试
        //5 VIP会员
        //6 城市合伙人
        //7 运营中心

        public string OpenId { get; set; }

        public int AgentId { get; set; }

     
        public string ordersn { get; set; }

        public decimal price { get; set; }

        public string status { get; set; }

        public string paytime { get; set; }

        public string createtime { get; set; }

        public string paytype { get; set; }

    }

}
