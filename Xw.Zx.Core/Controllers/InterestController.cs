using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sieve.Services;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Controllers
{
    /// <summary>
    /// 利息
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InterestController : BaseController
    {
        private readonly ILogger<InterestController> _logger;
        public InterestController(ILogger<InterestController> logger
            , XwZxContext xwZxContext
            , IMapper mapper
            , ISieveProcessor sieveProcessor) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
        }

        //一键
        //CreateAppeal DealApplal EndAppeal DelAppeal
    }

    public enum AppealState
    {
        待接单 = 0,
        已接单 = 1,
        已处理
    }

    public class Appeal
    {
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// 处理人ID
        /// </summary>
        public int OpMemberId { get; set; }

        /// <summary>
        /// 预追息金额
        /// </summary>
        public decimal PreAmount { get; set; }

        /// <summary>
        /// 实际追息金额
        /// </summary>
        public decimal Amount { get; set; }

        public AppealState State { get; set; }

        public DateTime AddTime { get; set; }
        public DateTime DealTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Remark { get; set; }
    }
}