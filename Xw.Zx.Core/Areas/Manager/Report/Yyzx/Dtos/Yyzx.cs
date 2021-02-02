using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Manager.Report.Yyzx.Dtos
{
    [AutoMap(typeof(Xtsuo_Member))]
    [AutoMap(typeof(Member))]
    public class Yyzx
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string RealName { get; set; }

        public string MemberVipType { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
