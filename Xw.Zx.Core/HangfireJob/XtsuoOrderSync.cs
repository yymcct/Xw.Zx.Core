using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Controllers;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service;

namespace Xw.Zx.Core.HangfireJob
{
    public class XtsuoOrderSync
    {
        private readonly ILogger<XtsuoOrderSync> _logger;
        private readonly IXtsuoService _xtsuoService;

        public XtsuoOrderSync(
            ILogger<XtsuoOrderSync> logger
            , IXtsuoService xtsuoService)
        {
            _logger = logger;
            _xtsuoService = xtsuoService;
        }


        public void Run()
        {
            //TODO  重构从数据库重读取最近一次同步的时间
            _xtsuoService.SyncXtsuoOrders(new XtsuoOrdersRequestDto()
            {
                StartTime = DateTime.Now.AddHours(-2),
                EndTime = DateTime.Now
            });
        }


    }
}
