using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    /// <summary>
    /// 存放的原始邮件
    /// </summary>
    public enum MailSrcBank
    {
        招商银行 = 0,
    }
    public class MailSrc
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public string Uid { get; set; }

        public string Sublic { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Body { get; set; }

        public string BodyText { get; set; }

        public DateTime SendTime { get; set; }

        public DateTime AddTime { get; set; } = DateTime.Now;

        public bool IsPrased { get; set; } = false;

    }
}
