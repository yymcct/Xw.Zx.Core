using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LxComputerController : BaseController
    {
        private readonly ILogger<LxComputerController> _logger;
        public LxComputerController(ILogger<LxComputerController> logger, XwZxContext xwZxContext) : base(xwZxContext)
        {
            _logger = logger;
        }

        [HttpPost]
        public HbzsResult PostUser([FromBody] LxComputer user)
        {
            try
            {
                _context.Add(user);
                _context.SaveChanges();
                return new HbzsResult(HbzsResultCode.Sucess);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

    }
}
