using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Service
{
    public class MailInfoDto
    {
        public string Id { get; set; }
        public string Subj { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
    }
}
