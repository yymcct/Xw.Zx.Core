using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public enum MailSrcBank
    {
        招商银行 = 0,
    }
    public class MailSrc
    {
        public int Id { get; set; }

        public string Uid { get; set; }

        public string Sublic { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Body { get; set; }

        public string BodyEncoding { get; set; }

        public DateTime SendTime { get; set; }

        public DateTime AddTime { get; set; }
    }
}
