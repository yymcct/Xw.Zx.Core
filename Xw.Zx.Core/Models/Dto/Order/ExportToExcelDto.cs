using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Models.Dto.Order
{
    public class ExportToExcelDto
    {
        public string Timestamp { get; set; }

        public string MemberName { get; set; }

        public string MemberPhone { get; set; }

        public string InviteName { get; set; }

        public string InvitePhone { get; set; }

        public string ProducName { get; set; }

        public int ProductCount { get; set; }

        public decimal Amount { get; set; }

        public DateTime AddTime { get; set; }

        public OrderPaymentType OrderPaymentType { get; set; }

        
    }
}
