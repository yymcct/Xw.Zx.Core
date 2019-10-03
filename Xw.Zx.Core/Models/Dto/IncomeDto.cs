using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Dto
{
    public class IncomeDto
    {
    }

    public class IncomInfo
    {
        /// <summary>
        /// 累计收入
        /// </summary>
        public decimal IncomTotal { get; set; }

        /// <summary>
        /// 可提现余额
        /// </summary>
        public decimal CanGet { get; set; }
    }

    public class IncomeDetailDto
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string Remark { get; set; }

        /// <summary>
        /// 收益时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
