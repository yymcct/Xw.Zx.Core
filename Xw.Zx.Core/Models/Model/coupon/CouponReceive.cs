using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{

    public enum CouponUseState
    {
        未使用 = 0,
        已使用 = 1
    }

    /// <summary>
    /// 优惠券表领取表
    /// </summary>
    public class CouponReceive : ModelBase_Id_CreateTime
    {
        public int Couponid { get; set; }

        public int Memberid { get; set; }       

        [Column(TypeName = "varchar(50)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Remark { get; set; } = "";

        [Sieve(CanFilter = true, CanSort = true)]
        public CouponUseState CouponUseState { get; set; }
    }
}
