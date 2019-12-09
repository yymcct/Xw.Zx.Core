using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Manager
{
    public class IncomeAccountMDto
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int MemberId { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string MemberName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string MemberPhone { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public MemberVipType MemberVipType { get; set; }

        public string MemberVipTypeName { get; set; }

        /// <summary>
        /// 直接收益
        /// </summary>
        public decimal? ZhijieTotla { get; set; }

        /// <summary>
        /// 间接收益
        /// </summary>
        public decimal? JianjieTotla { get; set; }
        /// <summary>
        /// 差级
        /// </summary>
        public decimal? ChajiTotla { get; set; }
        /// <summary>
        /// 收益合计
        /// </summary>
        [Sieve(CanFilter = true, CanSort = true)]
        public decimal? IncomeTotal { get; set; }

        /// <summary>
        /// 提现合计
        /// </summary>
        public decimal? WithdrawDepositTotal { get; set; }

        /// <summary>
        /// 待提现
        /// </summary>
        public decimal? Balance { get; set; }
    }
}
