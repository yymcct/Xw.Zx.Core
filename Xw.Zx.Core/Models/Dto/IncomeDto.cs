using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

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

        public decimal Amount { get; set; }

        public string Remark { get; set; }

        public IncomeAccountType IncomeAccountType { get; set; }

        public string IncomeAccountTypeName { get; set; }

        /// <summary>
        /// 收益时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
