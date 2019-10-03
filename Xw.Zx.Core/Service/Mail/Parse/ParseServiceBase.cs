using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public abstract class ParseServiceBase
    {
        protected readonly XwZxContext _xwZxContext;
        protected readonly BankCardType _ThisBankCardType;
        public ParseServiceBase(XwZxContext xwZxContext
            , BankCardType bankCardType)
        {
            _xwZxContext = xwZxContext;
            _ThisBankCardType = bankCardType;
        }

        protected void UpdateMailIsPrased(MailSrc mail)
        {
            mail.IsPrased = true;
            _xwZxContext.Entry(mail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _xwZxContext.SaveChanges();
        }

        protected void SaveBankBillDetail(List<BankBillDetail> details)
        {
            foreach (var detail in details)
            {
                _xwZxContext.BankBillDetails.Add(detail);
            }

            _xwZxContext.SaveChanges();
        }

        protected void SaveBank(string CardNum,int memberid)
        {
            if (_xwZxContext.BankCards.Any(b => b.MemberId == memberid
                    && b.Bank == _ThisBankCardType) == false)
            {
                _xwZxContext.BankCards.Add(new BankCard()
                {
                    MemberId = memberid,
                    CardNum = CardNum,
                    Bank = _ThisBankCardType,
                    LastSyncTime = DateTime.Now,
                    LastSyncIsOk = true,
                });
                _xwZxContext.SaveChanges();
            }
        }

        protected void UpdateBankCardSate(int memberid)
        {
            var bank = _xwZxContext.BankCards
                .Where(b => b.MemberId == memberid && b.Bank == _ThisBankCardType)
                .FirstOrDefault();

            if (bank != null)
            {
                bank.LastSyncIsOk = true;
                bank.LastSyncTime = DateTime.Now;
                _xwZxContext.SaveChanges();
            }
        }
    }
}
