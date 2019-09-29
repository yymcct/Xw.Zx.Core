using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public enum UpdateVipAuthCodeState
    {
        待使用 = 0,
        已赠送 = 1,
        已使用 = 2,
        已过期 = 3,
    }

    /// <summary>
    /// 升级Vip用的授权码
    /// </summary>
    public class UpdateVipAuthCode
    {
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

        //public string InternalCode { get; set; }

        //public string Salt { get; set; }

        /// <summary>
        /// 优惠码 = MD5()
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime ExpiesTime { get; set; } = DateTime.Now.AddDays(15);

        public UpdateVipAuthCodeState pdateVipAuthCodeState { get; set; } = UpdateVipAuthCodeState.待使用;

        public string Remark{ get; set; }

        public DateTime dateTime { get; set; } = DateTime.Now;
    }
}
