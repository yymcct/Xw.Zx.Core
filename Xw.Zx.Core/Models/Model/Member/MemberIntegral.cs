using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    /// <summary>
    /// 用户积分表
    /// </summary>
    public partial class MemberIntegral : ModelBase
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        /// <summary>
        /// 历史积分
        /// </summary>
        public int HistoryIntegrals { get; set; }

        /// <summary>
        /// 可用积分
        /// </summary>
        public int AvailableIntegrals { get; set; }

        public Member Member { get; set; }
    }

    public partial class MemberIntegral
    {
        public enum IntegralType
        {
            [Description("优惠券换积分")]
            FromCoupon = 1,

            [Description("积分买商品")]
            Trader = 1,
        }
    }
}
