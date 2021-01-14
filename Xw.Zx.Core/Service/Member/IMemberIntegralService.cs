using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public interface IMemberIntegralService
    {
        /// <summary>
        /// 用户积分记录增加
        /// </summary>
        /// <param name="model"></param>
        void AddMemberIntegral(MemberIntegralRecord model);

        /// <summary>
        /// 根据用户ID获取用户的积分信息
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        MemberIntegral GetMemberIntegral(int memberId);

        /// <summary>
        /// 根据用户ID获取用户的积分信息
        /// </summary>
        /// <param name="memberIds"></param>
        /// <returns></returns>
        List<MemberIntegral> GetMemberIntegrals(IEnumerable<int> memberIds);

        int AmountToIntegral(decimal amount);
        decimal IntegralToAmount(int amount);


    }
}
