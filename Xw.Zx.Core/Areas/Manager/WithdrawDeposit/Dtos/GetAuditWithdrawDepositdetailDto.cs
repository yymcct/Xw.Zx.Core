using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Areas.Manager
{
    public class IncomeDetail
    {
        public int Id { get; set; }

        public string IncomeAccountTypeName { get; set; }

        public decimal Amount { get; set; }

        public string Remark { get; set; }

        public DateTime AddTime { get; set; }
    }

    public class WithdrawDepositDetail
    {
        public int Id { get; set; }

        public string WithdrawDepositStateName { get; set; }

        public decimal Amount { get; set; }

        public string Remark { get; set; }

        public DateTime AddTime { get; set; }
    }

    public class GetAuditWithdrawDepositdetailDto
    {
        public GetAuditWithdrawDepositdetailDto()
        {
            IncomeDetails = new List<IncomeDetail>();
        }

        public List<IncomeDetail> IncomeDetails { get; set; }

        public List<WithdrawDepositDetail> WithdrawDepositDetails { get; set; }


        public decimal IncomeTotal { get; set; }

        public decimal WithdrawDeposit { get; set; }

        public decimal Balance { get; set; }
    }
}
