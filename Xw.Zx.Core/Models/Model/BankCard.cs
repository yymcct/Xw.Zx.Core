using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public enum BankCardType
    {
        招商银行 = 0,
        浦发银行 = 1,
        中信银行 = 2,
        平安银行 = 3,
        广大银行 = 4,
        华夏银行 = 5,
        民生银行 = 6
    }
    public class BankCard
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        public int MemberId { get; set; }
        //卡号
        public string CardNum { get; set; } = "";
        //银行
        public BankCardType Bank { get; set; }


        //用户名
        public string Email { get; set; }
        //密码
        public string Password { get; set; }

        /// <summary>
        /// 最后同步时间
        /// </summary>
        public DateTime? LastSyncTime { get; set; }

        public bool LastSyncIsOk { get; set; } = false;

        /// <summary>
        /// 滞纳金, 同步时进行更新
        /// </summary>
        [Column(TypeName = "decimal(8, 2)")]
        public decimal OverdueFine { get; set; } = 0;

        //创建日期
        public DateTime CreateDate { get; set; } = DateTime.Now;


        //是否已删除
        public bool Disabled { get; set; } = false;

        //备注
        public string Remark { get; set; } = "";
    }
}
