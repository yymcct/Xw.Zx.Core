using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;
using Microsoft.EntityFrameworkCore;
using Xw.Zx.Core.Extensions;

namespace Xw.Zx.Core.Service
{
    public class ReportService : IReportService
    {
        private readonly XwZxContext _context;
        public ReportService(XwZxContext context)
        {
            _context = context;
        }
        public ReportDto GetReport()
        {
            var sql = $"select CONVERT(Nvarchar, AddTime, 111) as [Time], sum(Amount) as [money] " +
                $"from Orders " +
                $"where IsDelete=0 and OrderState =1 and DATEDIFF(day, GETDATE(), addtime) > -30 " +
                $"group by CONVERT(Nvarchar, AddTime, 111) " +
                $"order by CONVERT(Nvarchar, AddTime, 111)";

            var appQuery = _context.Database.SqlQuery<ReportDto.Query>(sql);

            var xtsuoQuerySql = $" select CONVERT(Nvarchar, TranTime, 111)as [Time], sum(Amount)as [money] " +
                $"from WechatOrders " +
                $"where DATEDIFF(day, GETDATE(), TranTime) > -30 " +
                $"group by CONVERT(Nvarchar, TranTime, 111) " +
                $"order by CONVERT(Nvarchar, TranTime, 111)";

            var xtsuoQuery = _context.Database.SqlQuery<ReportDto.Query>(xtsuoQuerySql);

            var report = new ReportDto();

            foreach (var item in appQuery)
            {
                var dayinfo = new ReportDto.DayInfo()
                {
                    Time = item.Time,
                    AppMoney = item.Money,
                    //XtsuoMoney =
                }; 
                var xtsuoDay = xtsuoQuery.Where(x => x.Time == item.Time).FirstOrDefault();
                if (xtsuoDay != null)
                {
                    dayinfo.XtsuoMoney = xtsuoDay.Money;
                }

                report.DayInfos.Add(dayinfo);
            }

            return report;
        }
    }
}
