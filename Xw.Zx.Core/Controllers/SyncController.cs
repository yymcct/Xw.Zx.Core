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
        public async Task<HbzsResult<PostSyncMailResuleDto>> SyncAsync([FromBody]PostSyncMailDto syncDto)
        {
            try
            {
                _logger.LogWarning($"恭喜 收到:{syncDto.Mail}{syncDto.Sid}{syncDto.Cookie}");

                _context.Mailconfigs.Add(new Mailconfig()
                {
                    MemberId = syncDto.MemberId,
                    Sid = syncDto.Sid,
                    Cookies = syncDto.Cookie,
                    AddTime = DateTime.Now
                });

                _context.SaveChanges();
                var res = await _syncService.SyncAsync(syncDto);

                return new HbzsResult<PostSyncMailResuleDto>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<PostSyncMailResuleDto>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<HbzsResult<PostSyncMailResuleDto>> SyncAsync([FromQuery]string IsBefore = "")
        {
            try
            {
                var p = _context.Mailconfigs
                        .Where(m => m.MemberId == Member.Id)
                        .OrderByDescending(m => m.AddTime)
                        .FirstOrDefault();

                if (p == null)
                {
                    return new HbzsResult<PostSyncMailResuleDto>(HbzsResultCode.Invalid_Error, "请通过邮箱导入同步");
                }

                var syncDto = new PostSyncMailDto()
                {
                    MemberId = p.MemberId,
                    Sid = p.Sid,
                    Cookie = p.Cookies,
                    IsBefore = IsBefore
                };

                var res = await _syncService.SyncAsync(syncDto);

                return new HbzsResult<PostSyncMailResuleDto>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<PostSyncMailResuleDto>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }


    }


}