using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Manager
{
    public class IncomeAccountMRespone
    {
        public class Income
        {
            public int Id { get; set; }

            /// <summary>
            /// 受益人ID
            /// </summary>
            public int MemberId { get; set; }

            public string MemberName { get; set; }

            public string MemberPhone { get; set; }

            /// <summary>
            /// 收益金额
            /// </summary>
            public decimal Amount { get; set; }

            /// <summary>
            /// 产生收益的单据ID
            /// </summary>
            public int SourceOrderId { get; set; }

            public string SourceOrderTimestamp { get; set; }
            public int SourceOrderMemberId { get; set; }
            public string SourceOrderMemberPhone { get; set; }
            public string SourceOrderProducName { get; set; }
            public decimal SourceOrderProductAmount { get; set; }
            public DateTime SourceOrderAddTime { get; set; }
            public OrderPaymentType SourceOrderOrderPaymentType { get; set; }
            public string SourceOrderOrderPaymentTypeName { get; set; }

            //团队树

            //收益类型
            public IncomeAccountType IncomeAccountType { get; set; }
            public string IncomeAccountTypeName { get; set; }

            //备注
            public string Remark { get; set; }

            //收益时间
            public DateTime AddTime { get; set; } 
        }
    }
}
