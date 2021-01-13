using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public interface IShareprofitService
    {
        void AddShareprofit(Member incomeMember, Order order, decimal money);


        List<IncomeAccount> GetWaitWithdraws(int memberId);

        void SetPaySucess(int withdrawId);
    }
}
