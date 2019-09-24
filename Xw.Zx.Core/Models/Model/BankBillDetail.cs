using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public class BankBillDetail
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string CardNum { get; set; }

        /// <summary>
        /// 交易时间
        /// </summary>
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime? TreadTime { get; set; }

        /// <summary>
        /// 商户名
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// 交易额
        /// </summary>
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Amount { get; set; }
        /// <summary>
        /// 币别
        /// </summary>
        public string Unit { get; set; }


        #region 辅助字段
        [Sieve(CanFilter = true, CanSort = true)]
        public int  MemberID { get; set; }

        /// <summary>
        /// 从哪封邮件解析出来的
        /// </summary>
        public int MailId { get; set; }
        #endregion

    }
}
