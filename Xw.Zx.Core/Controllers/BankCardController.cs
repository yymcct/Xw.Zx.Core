using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BankCardController : BaseController
    {
        private readonly ILogger<MemberController> _logger;
        public BankCardController(ILogger<MemberController> logger, XwZxContext xwZxContext)
            : base(xwZxContext)
        {
            _logger = logger;
        }

        // GET: api/BankCard
        [HttpGet]
        public IEnumerable<string> BankCard()
        {
            return new string[] { "value1", Member.Phone };
        }

        // GET: api/BankCard/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/BankCard
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/BankCard/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
