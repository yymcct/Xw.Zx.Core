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
    public class MemberController : ControllerBase
    {
        private readonly XwZxContext _context;
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sieveProcessor;
        public MemberController(XwZxContext context
            , IMapper mapper
            , ISieveProcessor sieveProcessor)
        {
            _context = context;
            _sieveProcessor = sieveProcessor;
            _mapper = mapper;
        }


        [HttpGet]
        public HbzsResult<IEnumerable<MemberRespone.Member>> Get([FromQuery] SieveModel sieveModel)
        {
            try
            {
                var db = _context.Members;

                var res = _sieveProcessor
                     .Apply(sieveModel, db)
                     .ProjectTo<MemberRespone.Member>(_mapper.ConfigurationProvider);

                var total = _sieveProcessor.Apply(sieveModel, db, null, true, true, false).Count();

                return new HbzsResult<IEnumerable<MemberRespone.Member>>(res, total);
            }
            catch (Exception ex)
            {
                return new HbzsResult<IEnumerable<MemberRespone.Member>>(HbzsResultCode.Invalid_Error, ex.Message);
            }

        }
    }
}
