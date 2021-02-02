using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{


    public partial class Xtsuo_Member
    {
        public int Id { get; set; }


        //0 默认等级
        //8 测试
        //5 VIP会员
        //6 城市合伙人
        //7 运营中心

        public int Level { get; set; }

        public int AgentId { get; set; }

        [Column("openid")]
        public string Phone { get; set; }

        public string RealName { get; set; }

        public int createtime { get; set; }

        public string NickName { get; set; }

    }

}
