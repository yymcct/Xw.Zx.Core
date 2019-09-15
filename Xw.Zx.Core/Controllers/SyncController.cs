using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class SyncController : BaseController
    {
        private readonly ILogger<SyncController> _logger;
        private readonly ISyncService _syncService;
        public SyncController(ILogger<SyncController> logger
            , ISyncService syncService
            , XwZxContext xwZxContext) : base(xwZxContext)
        {
            _logger = logger;
            _syncService = syncService;
        }

        [HttpPost]
        public async Task<HbzsResult> SyncAsync([FromBody]PostSyncMailDto syncDto)
        {
            try
            {
                (var isOK, var msg) = await _syncService.SyncAsync(syncDto);

                return isOK ? new HbzsResult(HbzsResultCode.Sucess)
                    : new HbzsResult(HbzsResultCode.Invalid_Error, msg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }
    }


}