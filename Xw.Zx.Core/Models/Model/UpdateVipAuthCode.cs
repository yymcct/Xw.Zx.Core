using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public enum UpdateVipAuthCodeState
    {
        /// <summary>
        /// 待使用 
        /// </summary>
        待使用 = 0,
        /// <summary>
        /// 已赠送
        /// </summary>
        已赠送 = 1,
        /// <summary>
        /// 已使用
        /// </summary>
        已使用 = 2,
        /// <summary>
        /// 已过期
        /// </summary>
        已过期 = 3,
    }

    /// <summary>
    /// 升级Vip用的授权码
    /// </summary>
    public class UpdateVipAuthCode
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        /// <summary>
        /// 原优惠券ID
        /// </summary>
        public int SourceId { get; set; } = 0;

        /// <summary>
        /// 所有者ID
        /// </summary>
        public int OwinId { get; set; }

        /// <summary>
        /// 原所有者ID
        /// </summary>
        public int SourceOwinId { get; set; } = 0;

        /// <summary>
        /// 使用者ID
        /// </summary>
        public int UsedMemberId { get; set; } = 0;

        public DateTime UsedTime { get; set; }

        //public string InternalCode { get; set; }

        //public string Salt { get; set; }

        /// <summary>
        /// 优惠码 = MD5()
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime ExpiesTime { get; set; } = DateTime.Now.AddDays(15);

        [Sieve(CanFilter = true, CanSort = true)]
        public UpdateVipAuthCodeState UPdateVipAuthCodeState { get; set; } = UpdateVipAuthCodeState.待使用;

        public string Remark{ get; set; }

        public DateTime AddTime { get; set; } = DateTime.Now;
    }
}
