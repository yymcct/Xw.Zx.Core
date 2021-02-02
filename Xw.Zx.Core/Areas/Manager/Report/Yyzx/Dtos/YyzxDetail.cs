using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Manager.Report.Yyzx.Dtos
{

    public class YyzxDetail
    {
        public int Id { get; set; }
        public int InviteId { get; set; }
        public string RealName { get; set; }

        public string Phone { get; set; }

        public int T { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
