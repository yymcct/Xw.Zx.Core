using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{

    /// <summary>
    /// 优惠券表领取表
    /// </summary>
    public class CouponUseLog : ModelBase_Id_CreateTime
    {
        public int CouponReceiveid { get; set; }

        public int Productid { get; set; }

        public int Orderid { get; set; }    
    }
}
