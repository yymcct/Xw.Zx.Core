using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Helper;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public class MemberIntegralService : IMemberIntegralService
    {
        private readonly XwZxContext _context;

        public MemberIntegralService(XwZxContext xwZxContext)
        {
            _context = xwZxContext;
        }

        public void AddMemberIntegral(MemberIntegralRecord model)
        {
            if (null == model) { throw new NullReferenceException("添加会员积分记录时，会员积分Model为空."); }
            if (0 == model.MemberId) { throw new NullReferenceException("添加会员积分记录时，会员Id为空."); }
            if (!_context.Members.Any(a => a.Id == model.MemberId))
            {
                throw new ZzzException("不存在此会员");
            }
           
            if (model.Integral == 0)
            {
                return;
            }
            var userIntegral = _context.MemberIntegrals.FirstOrDefault(a => a.MemberId == model.MemberId);
            if (userIntegral == null)
            {
                userIntegral = new MemberIntegral();
                userIntegral.MemberId = model.MemberId;
                if (model.Integral > 0)
                {
                    userIntegral.HistoryIntegrals += model.Integral;
                }
                else
                {
                    throw new ZzzException("用户积分不足以扣减该积分！");
                }
                userIntegral.AvailableIntegrals += model.Integral;
                _context.MemberIntegrals.Add(userIntegral);
            }
            else
            {
                if (model.Integral > 0)
                {
                    userIntegral.HistoryIntegrals += model.Integral;
                }
                else
                {
                    if (userIntegral.AvailableIntegrals < Math.Abs(model.Integral))
                        throw new ZzzException("用户积分不足以扣减该积分！");
                }
                userIntegral.AvailableIntegrals += model.Integral;
            }
            _context.MemberIntegralRecords.Add(model);
            _context.SaveChanges();
        }

     

        public MemberIntegral GetMemberIntegral(int memberId)
        {
            var model = _context.MemberIntegrals.FirstOrDefault(a => a.MemberId == memberId);
            if (model == null)
            {
                model = new MemberIntegral();
            }
            return model;
        }

        public List<MemberIntegral> GetMemberIntegrals(IEnumerable<int> memberIds)
        {
            var model = _context.MemberIntegrals.Where(a => memberIds.Contains(a.MemberId)).ToList();
            return model;
        }

        public decimal IntegralToAmount(int integral)
        {
            throw new NotImplementedException();
        }

        public int AmountToIntegral(decimal amount)
        {
            return Convert.ToInt32(amount * 10);
        }
    }
}
