using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{

    public class WithdrawDepositLog : ModelBase_Id_CreateTime
    {
        public int WithdrawDepositId { get; set; }

        public int AddUserId { get; set; }  

        public WithdrawDepositState WithdrawDepositState { get; set; } = WithdrawDepositState.申请中;

        [Column(TypeName = "nvarchar(500)")]
        public string Remark { get; set; } = "";
    }
}
