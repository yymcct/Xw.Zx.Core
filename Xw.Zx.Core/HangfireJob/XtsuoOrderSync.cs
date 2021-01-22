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
        private readonly XwZxContext _context;

        public XtsuoOrderSync(
            ILogger<XtsuoOrderSync> logger
            , IXtsuoService xtsuoService
            , XwZxContext xwZxContext)
        {
            _logger = logger;
            _xtsuoService = xtsuoService;
            _context = xwZxContext;
        }


        public void Run()
        {
            var startTime = _context.WechatOrders.Max(w => w.TranTime);

            _xtsuoService.SyncXtsuoOrders(new XtsuoOrdersRequestDto()
            {
                StartTime = startTime,
                EndTime = DateTime.Now
            });
        }


    }
}
