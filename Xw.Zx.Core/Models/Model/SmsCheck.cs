using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public enum SmsCheckState
    {
        已发送 = 1,
        已验证 = 2
    }
    public class SmsCheck
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }


        /// <summary>
        /// 电话号吗
        /// </summary>
        [Sieve(CanFilter = true)]
        public string Phone { get; set; }

        /// <summary>
        /// 最后一次发送的验证码
        /// </summary>
        public int LastSendCode { get; set; }

        /// <summary>
        /// 最后一次发送时间
        /// </summary>
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime LastSendTime { get; set; }

        /// <summary>
        /// 合计发送次数
        /// </summary>
        [Sieve(CanFilter = true, CanSort = true)]
        public int SendCnt { get; set; } = 1;

        /// <summary>
        /// 验证状态
        /// </summary>
        [Sieve(CanFilter = true, CanSort = true)]
        public SmsCheckState CheckState { get; set; }
    }
}
