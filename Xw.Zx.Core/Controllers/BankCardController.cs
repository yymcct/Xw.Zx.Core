using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BankCardController : BaseController
    {
        private readonly ILogger<MemberController> _logger;
        public BankCardController(ILogger<MemberController> logger
            , XwZxContext xwZxContext
            , IMapper mapper) : base(xwZxContext, mapper)

        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Info([FromQuery]SieveModel sieveModel)
        {
            return new string[] { "value1", Member.Phone };
        }

        // GET: api/BankCard/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// 添加修改银行卡
        /// </summary>
        /// <param name="card">ID=0添加, ID=1修改</param>
        /// <returns></returns>
        [HttpPost]
        public HbzsResult Post([FromBody] PostBankDto card)
        {
            BankCard bankCard = null;
            try
            {
                if (card.Id == 0)
                {
                    bankCard = new BankCard()
                    {
                        MemberId = Member.Id
                    };
                    bankCard = _mapper.Map(card, bankCard);
                    _context.BankCards.Add(bankCard);
                }
                else
                {
                    bankCard = _context.BankCards.First(b => b.Id == card.Id);
                    bankCard = _mapper.Map(card, bankCard);
                }
                _context.SaveChanges();
                return new HbzsResult(HbzsResultCode.Sucess);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult(HbzsResultCode.Invalid_Error, ex.Message);
            }

        }

        /// <summary>
        /// 删除自己名下银行卡
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }


}
