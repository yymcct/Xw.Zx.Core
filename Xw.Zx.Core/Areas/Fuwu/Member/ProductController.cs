using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Areas.Fuwu.Dtos;
using Xw.Zx.Core.Controllers;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Areas.Fuwu.Member
{
    [Route("fuwu/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly XwZxContext _context;
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sieveProcessor;
        public ProductController(XwZxContext context
            , IMapper mapper
            , ISieveProcessor sieveProcessor)
        {
            _context = context;
            _sieveProcessor = sieveProcessor;
            _mapper = mapper;
        }


        [HttpGet]
        public HbzsResult<IEnumerable<ProductRespone.Product>> Get([FromQuery] SieveModel sieveModel)
        {
            try
            {
                var db = _context.Products;

                var res = _sieveProcessor
                     .Apply(sieveModel, db)
                     .ProjectTo<ProductRespone.Product>(_mapper.ConfigurationProvider);

                var total = _sieveProcessor.Apply(sieveModel, db, null, true, true, false).Count();

                return new HbzsResult<IEnumerable<ProductRespone.Product>>(res, total);
            }
            catch (Exception ex)
            {
                return new HbzsResult<IEnumerable<ProductRespone.Product>>(HbzsResultCode.Invalid_Error, ex.Message);
            }

        }
    }
}
