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
            var sql = $"select CONVERT(Nvarchar, AddTime, 111) as [Time], sum(Amount) as [money] , count(*) as [count] " +
                $"from Orders " +
                $"where IsDelete=0 and OrderState =1 and DATEDIFF(day, GETDATE(), addtime) > -15 " +
                $"group by CONVERT(Nvarchar, AddTime, 111) " +
                $"order by CONVERT(Nvarchar, AddTime, 111)";

            var appQuery = _context.Database.SqlQuery<ReportDto.Query>(sql);

            var xtsuoQuerySql = $" select CONVERT(Nvarchar, TranTime, 111)as [Time], sum(Amount)as [money] , count(*) as [count] " +
                $"from WechatOrders " +
                $"where TransactionID is not null and  DATEDIFF(day, GETDATE(), TranTime) > -15 " +
                $"group by CONVERT(Nvarchar, TranTime, 111) " +
                $"order by CONVERT(Nvarchar, TranTime, 111)";

            var xtsuoQuery = _context.Database.SqlQuery<ReportDto.Query>(xtsuoQuerySql);


            var left = (from app in appQuery
                       join xtsuo in xtsuoQuery
                       on app.Time equals xtsuo.Time
                       into joinApps
                       from joinApp in joinApps.DefaultIfEmpty(new ReportDto.Query {Time = app.Time, Money=0 })
                       select new ReportDto.DayInfo
                       {
                           Time = app.Time,
                           AppMoney = app.Money,
                           XtsuoMoney = joinApp.Money
                       }).ToList();

            var right = (from xtsuo in xtsuoQuery
                        join app in appQuery
                        on  xtsuo.Time equals app.Time 
                        into joinxtsuos
                        from joinxtsuo in joinxtsuos.DefaultIfEmpty(new ReportDto.Query { Time = xtsuo.Time, Money = 0 })
                        select new ReportDto.DayInfo
                        {
                            Time = xtsuo.Time,
                            AppMoney = joinxtsuo.Money,
                            XtsuoMoney = xtsuo.Money
                        }).ToList();


            var full = left.Union(right).OrderBy(s=>s.Time).ToList();

            var report = new ReportDto();

            report.DayInfos = full;

            //if (appQuery.Count >= xtsuoQuery.Count)
            //{
            //    foreach (var item in appQuery)
            //    {
            //        var dayinfo = new ReportDto.DayInfo()
            //        {
            //            Time = item.Time,
            //            AppMoney = item.Money,
            //            Count = item.Count
            //        };
            //        var xtsuoDay = xtsuoQuery.Where(x => x.Time == item.Time).FirstOrDefault();
            //        if (xtsuoDay != null)
            //        {
            //            dayinfo.XtsuoMoney = xtsuoDay.Money;
            //        }

            //        report.DayInfos.Add(dayinfo);
            //    }
            //}
            //else
            //{
            //    foreach (var item in xtsuoQuery)
            //    {
            //        var dayinfo = new ReportDto.DayInfo()
            //        {
            //            Time = item.Time,
            //            XtsuoMoney = item.Money,
            //            Count = item.Count
            //        };
            //        var appDay = appQuery.Where(x => x.Time == item.Time).FirstOrDefault();
            //        if (appDay != null)
            //        {
            //            dayinfo.AppMoney = appDay.Money;
            //        }

            //        report.DayInfos.Add(dayinfo);
            //    }

            //}


            return report;
        }
    }
}
