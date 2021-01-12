using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public class ShareprofitService : IShareprofitService
    {
        private readonly XwZxContext _context;
        public ShareprofitService(XwZxContext context)
        {
            _context = context;
        }
        public void AddShareprofit(Member incomeMember, Order order, decimal money)
        {
            throw new NotImplementedException();
        }

        public List<IncomeAccount> GetWaitWithdraws(int memberId)
        {
            return _context.IncomeAccounts.Where(i => 
                    i.IncomeAccountState == IncomeAccountState.已发放 && i.MemberId == memberId)
                .ToList();
        }
    }
}
