using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Models.Dto
{
    public class UpdaeVipAuthCodeDto
    {

        /// <summary>
        /// 赠送人ID
        /// </summary>
        public int SourceOwinId { get; set; }

        /// <summary>
        /// 赠送人电话
        /// </summary>
        public string SourceOwinPhone { get; set; }

        /// <summary>
        /// 升级码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime ExpiesTime { get; set; }

        /// <summary>
        /// 升级码状态 0 待使用, 1 已赠送, 2 已使用, 3 已过期
        /// </summary>
        public UpdateVipAuthCodeState UPdateVipAuthCodeState { get; set; }
    }

}
