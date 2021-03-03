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
    public class XtsuoOrderSyncJob
    {
        private readonly ILogger<XtsuoOrderSyncJob> _logger;
        private readonly IXtsuoService _xtsuoService;
        private readonly XwZxContext _context;

        public XtsuoOrderSyncJob(
            ILogger<XtsuoOrderSyncJob> logger
            , IXtsuoService xtsuoService
            , XwZxContext xwZxContext)
        {
            _logger = logger;
            _xtsuoService = xtsuoService;
            _context = xwZxContext;
        }


        public void Run()
        {
           // var startTime = _context.WechatOrders.Max(w => w.TranTime);

            _xtsuoService.SyncXtsuoOrders(new XtsuoOrdersRequestDto()
            {
                StartTime = DateTime.Now.AddHours(-18),
                EndTime = DateTime.Now
            });

            _xtsuoService.QuerySubLedgerResultAll();
        }


    }
}
