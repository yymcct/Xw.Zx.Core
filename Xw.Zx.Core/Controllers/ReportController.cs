using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportController : BaseController
    {
        private readonly IReportService _ReportService;
        public ReportController(IReportService ReportService
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor) : base(xwZxContext, mapper, sieveProcessor)
        {
            _ReportService = ReportService;

        }

        [HttpGet]
        public HbzsResult<ReportDto> Get()
        {
            return new HbzsResult<ReportDto>(_ReportService.GetReport());
        }

     
    }
}
