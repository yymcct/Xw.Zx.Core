using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Service
{
    public partial class ReportDto
    {
        public ReportDto()
        {
            DayInfos = new List<DayInfo>();
        }
        public List<DayInfo> DayInfos;
    }

    public partial class ReportDto
    {
        public class DayInfo : IEquatable<DayInfo>
        {
            public string Time { get; set; }

            public decimal AppMoney { get; set; }

            public decimal XtsuoMoney { get; set; }

            public decimal TotalMoney
            {
                get
                {
                    return AppMoney + XtsuoMoney;
                }
            }
            public int Count { get; set; }

            public bool Equals(DayInfo other)
            {
                if (other is null)
                    return false;

                return this.Time == other.Time && this.TotalMoney == other.TotalMoney;
            }

            public override bool Equals(object obj) => Equals(obj as Query);
            public override int GetHashCode() => (Time, TotalMoney).GetHashCode();

        }

        public class Query
        {
            public string Time { get; set; }

            public decimal Money { get; set; }

            public int Count { get; set; }
          
        }
    }
}
