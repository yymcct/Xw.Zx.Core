using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public class WechatSubDetail: ModelBase
    {

        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string TransactionID { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Last_Out_Order_No { get; set; }

        /// <summary>
        /// 分账返回的 分账单号
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string Return_OrderID { get; set; }

        /// <summary>
        /// 分账接收人 类型
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string SubType { get; set; }   

        /// <summary>
        /// 分账接收人账户
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string SubAccount { get; set; }

        /// <summary>
        /// 分账 接收人名
        /// </summary>

        [Column(TypeName = "varchar(50)")]
        public string SubName { get; set; }

        /// <summary>
        /// 分账 金额
        /// </summary>

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubAmount { get; set; }

        /// <summary>
        /// 分账时间
        /// </summary>

        [Column(TypeName = "datetime")]
        public DateTime SubTime { get; set; }

        /// <summary>
        /// 分账 结果
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string SubState { get; set; }

        /// <summary>
        /// 分账描述
        /// </summary>

        [Column(TypeName = "varchar(256)")]
        public string PayDescription { get; set; }
    }
}
