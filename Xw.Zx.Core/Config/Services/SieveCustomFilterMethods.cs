
using Sieve.Services;

using System.Linq;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Config
{
    public class SieveCustomFilterMethods : ISieveCustomFilterMethods
    {
        private readonly XwZxContext _context;
        //public IQueryable<Products> HomeRecommendProduct(IQueryable<Products> source, string op, string[] values)
        //    => source.Where(p => true).OrderByDescending(p => p.ProCount);
        public SieveCustomFilterMethods(XwZxContext context)
        {
            _context = context;
        }

        public IQueryable<CouponReceive> MemberName(IQueryable<CouponReceive> source, string op, string[] values)
        {
            var member = _context.Members.FirstOrDefault(m => m.RealName.Contains(values[0]) || m.Phone.Contains(values[0]));
            if (member == null)
            {
                return source.Where(c => 1 != 1);
            }

            var res = source.Where(c => c.Memberid == member.Id);

            return res;
        }

        public IQueryable<IncomeAccount> MemberName(IQueryable<IncomeAccount> source, string op, string[] values)
        {
            var member = _context.Members.FirstOrDefault(m => m.RealName.Contains(values[0]) || m.Phone.Contains(values[0]));
            if (member == null)
            {
                return source.Where(c => 1 != 1);
            }

            var res = source.Where(c => c.SourceOrderMemberId == member.Id);

            return res;
        }
    }
}
