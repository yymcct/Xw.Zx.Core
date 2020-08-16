using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;
using Xw.Zx.Core.Areas.Manager;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Manager
{
    [ApiController]
    [Route("manager/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class LxComputerController : ManagerBaseController
    {
        private readonly ILogger<MemberController> _logger;

        public LxComputerController(ILogger<MemberController> logger
            , XwZxContext context
            , IMapper mapper
            , ISieveProcessor sieveProcessor) : base(context, mapper, sieveProcessor)
        {
            _logger = logger;
        }

        [HttpGet]
        public HbzsManagerResult<List<LxComputerMDto>> GetLxComputers([FromQuery] SieveModel sieveModel)
        {
            try
            {
                var db = from lxcomputer in _context.LxComputers
                         select lxcomputer;

                var list = _sieveProcessor.Apply(sieveModel, db).ToList();
                var total = _sieveProcessor.Apply(sieveModel, db, null, true, true, false).Count();
                return new HbzsManagerResult<List<LxComputerMDto>>(_mapper.Map<List<LxComputerMDto>>(list), total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult<List<LxComputerMDto>>(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }

    }
}
