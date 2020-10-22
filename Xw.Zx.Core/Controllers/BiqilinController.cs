using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service;

namespace Xw.Zx.Core.Controllers
{
    /// <summary>
    /// 碧麒麟支付
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BiqilinController : BaseController
    {
        private readonly ILogger<BiqilinController> _logger;
        private readonly IBiqilinService _biqilinService;

        public BiqilinController(ILogger<BiqilinController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor
            , IBiqilinService biqilinService) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
            _biqilinService = biqilinService;
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [Authorize]
        public HbzsResult<string> CreateQrcodePayUrl([FromQuery]int orderId)
        {
            try
            {
                var order = _context.Orders.First(o => o.MemberId == Member.Id
                                                && o.OrderState == OrderState.待付款
                                                && o.Id == orderId);

                var url = _biqilinService.CreateQrcodePayUrl(new Biqilin_Product()
                {
                    Name = order.ProducName,
                    Timestamp = order.Timestamp,
                    Amount = order.Amount
                });

                return new HbzsResult<string>(url);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new HbzsResult<string>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }
    }
}
