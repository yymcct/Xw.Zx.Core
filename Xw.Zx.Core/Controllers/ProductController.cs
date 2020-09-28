using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger
         , XwZxContext xwZxContext
         , IMapper mapper
         , ISieveProcessor sieveProcessor) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;

        }
        [HttpGet]
        public HbzsResult<List<ProductListDto>> Gets([FromQuery] SieveModel sieveModel)
        {
            try
            {
                var db = _context.Products
                    .AsNoTracking()
                    .Where(p => p.Check == true);

                var res = _sieveProcessor
                    .Apply(sieveModel, db)
                    .ProjectTo<ProductListDto>(_mapper.ConfigurationProvider)
                    .ToList();

                var total = _sieveProcessor
                    .Apply(sieveModel, db, applyPagination: false)
                    .Count();
                return new HbzsResult<List<ProductListDto>>(res, total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<List<ProductListDto>>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

    }
}
