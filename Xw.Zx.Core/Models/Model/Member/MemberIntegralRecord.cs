using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    /// <summary>
    /// 用户积分明细表
    /// </summary>
    public  class MemberIntegralRecord : ModelBase
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public int Integral { get; set; }

        public MemberIntegral.IntegralType TypeId { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string Remark { get; set; } = "";

        public virtual Member Member { get; set; }
    }
}
