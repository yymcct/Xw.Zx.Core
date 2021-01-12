using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public class WithdrawService : IWithdrawService
    {
        private readonly XwZxContext _context;

        public WithdrawService(XwZxContext context)
        {
            _context = context;
        }

        public void AddWithdrawByShareprofits(Member member, List<int> shareprofitIds)
        {
            var incomes = _context.IncomeAccounts
                .Where(i => i.IncomeAccountState == IncomeAccountState.已发放
                && i.MemberId == member.Id
                && shareprofitIds.Contains(i.Id)).ToArray();

            if (incomes.Length == 0) return;

            var amount = incomes.Sum(i => i.Amount);
            var charge = decimal.Parse((amount * 0.15m / 100).ToString("#0.00"));
            var withdrawDeposit = new WithdrawDeposit()
            {
                MemberId = member.Id,
                Amount = amount,
                WithdrawCharge = charge,
                RealityAmount = amount - charge
            };

            _context.Entry<WithdrawDeposit>(withdrawDeposit).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _context.SaveChanges();
            _context.Entry(withdrawDeposit);

            for (var i = 0; i < incomes.Length; i++)
            {
                var income = incomes[i];
                income.IncomeAccountState = IncomeAccountState.提现中;
                income.WithdrawDepositId = withdrawDeposit.Id;
            }
            _context.SaveChanges();
        }

       
    }
}
