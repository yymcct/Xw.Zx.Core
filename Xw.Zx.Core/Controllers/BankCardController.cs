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
using Sieve.Services;
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
            , IMapper mapper
            , ISieveProcessor sieveProcessor) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取自己名下的银行卡信息, 可检索, 排序, 分页      
        /// </summary>
        /// <param name="sieveModel"></param>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult<List<PostBankDto>> Gets([FromQuery]SieveModel sieveModel)
        {
            try
            {
                var db = _context.BankCards
                    .AsNoTracking()
                    .Where(b => b.MemberId == Member.Id && b.Disabled == false);

                var cards = _sieveProcessor
                    .Apply(sieveModel, db)
                    .ToList();

                var res = _mapper.Map<List<PostBankDto>>(cards);

                return new HbzsResult<List<PostBankDto>>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<List<PostBankDto>>(HbzsResultCode.Invalid_Error, ex.Message);
            }
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
        /// 注:只能删除自己名下的银行卡
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        public HbzsResult Delete([FromQuery]int id)
        {
            try
            {
                var bankCard = _context.BankCards.FirstOrDefault(b => b.Id == id && b.MemberId == Member.Id);

                if (bankCard == null) throw new Exception("不存在卡片!");

                if (bankCard.Disabled == true) throw new Exception("不能重复删除!");

                bankCard.Disabled = true;

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
