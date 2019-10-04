using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Models.Dto
{
    public class ApplyDto
    {
        public ApplyDto() {
            ApplyMember = new MemberDto();
        }

        /// <summary>
        /// 申请人
        /// </summary>
        public MemberDto ApplyMember { get; set; }

        /// <summary>
        /// 利息金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 申请备注
        /// </summary>
        public string Remaek { get; set; }

        /// <summary>
        /// 追息单状态  待处理 = 0, 处理中 = 1,已完结 = 2
        /// </summary>
        public ApplyForZxState ApplyForZxState { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
    public class PostApplyDto
    {
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
