using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        /// <param name="sieveModel">可空</param>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult<List<BankInfoDto>> Gets([FromQuery]SieveModel sieveModel)
        {
            try
            {
                var db = _context.BankCards
                    .AsNoTracking()
                    .Where(b => b.MemberId == Member.Id && b.Disabled == false);

                var cards = _sieveProcessor
                    .Apply(sieveModel, db)
                    .ToList();

                var res = _mapper.Map<List<BankInfoDto>>(cards);

                for (var i = 0; i < res.Count; i++)
                {
                    res[i].OverdueFine = _context.BankBillDetails
                            .Where(b => b.MemberID == Member.Id && b.Bank == res[i].Bank)
                            .Sum(b => b.Amount);
                    res[i].BankName = res[i].Bank.ToString();
                }

                return new HbzsResult<List<BankInfoDto>>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<List<BankInfoDto>>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// 获取自己名下欠息合计
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult<CardTotalInfo> GetCardTotal()
        {
            try
            {
                var OverdueFine = _context.BankBillDetails
                    .Where(b => b.MemberID == Member.Id)
                    .Sum(b => b.Amount);

                var res = new CardTotalInfo()
                {
                    OverdueFine = OverdueFine
                };

                return new HbzsResult<CardTotalInfo>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<CardTotalInfo>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// 获取所请求的银行卡的欠息账单详情
        /// </summary>
        /// <param name="id">银行卡ID</param>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult<CardContentDto> GetCardContent([FromQuery]int id)
        {
            try
            {
                var dto = new CardContentDto();
                var card = _context.BankCards
                        .AsNoTracking()
                        .Where(b => b.MemberId == Member.Id && b.Disabled == false && b.Id == id).First();

                dto.bankInfo = _mapper.Map<BankInfoDto>(card);

                dto.cardBills = _context.BankBills
                        .AsNoTracking()
                        .Where(b => b.BankCardId == card.Id).ToList();

                return new HbzsResult<CardContentDto>(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<CardContentDto>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

        /// <summary>
        /// [弃用] 添加修改银行卡
        /// </summary>
        /// <param name="card">ID=0添加, ID=1修改</param>
        /// <returns></returns>
        [HttpPost]
        public HbzsResult Post([FromBody] PostBankDto card)
        {
            BankCard bankCard = null;
            try
            {
                if (card.Id == 0)//添加卡片
                {
                    if (_context.BankCards.Any(c => c.CardNum == card.CardNum))
                    {
                        throw new Exception("卡号已存在, 请勿重复添加!");
                    }

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
        /// [弃用]删除自己名下银行卡
        /// </summary>
        /// <param name="id">银行卡ID</param>
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


        /// <summary>
        /// 获取自己名下的银行卡信息, 可检索, 排序, 分页      
        /// </summary>
        /// <param name="sieveModel">可空</param>
        /// <returns></returns>
        [HttpGet]
        public HbzsResult<List<BankBillDetailDto>> GetBankBillDetail([FromQuery]SieveModel sieveModel)
        {
            try
            {
                var db = _context.BankBillDetails
                    .AsNoTracking()
                    .Where(b => b.MemberID == Member.Id);

                var cards = _sieveProcessor
                    .Apply(sieveModel, db)
                    .ToList();

                var res = _mapper.Map<List<BankBillDetailDto>>(cards);

                return new HbzsResult<List<BankBillDetailDto>>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<List<BankBillDetailDto>>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }





    }
}
