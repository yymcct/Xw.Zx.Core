using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger
         , XwZxContext xwZxContext
         , IMapper mapper
         , ISieveProcessor sieveProcessor) : base(xwZxContext, mapper, sieveProcessor)
        {
            _logger = logger;

        }

        [HttpGet]
        public HbzsResult<List<OrderListDto>> Get([FromQuery] SieveModel sieveModel)
        {
            try
            {
                var db = _context.Orders.Where(o => o.MemberId == Member.Id)
                    .AsNoTracking();

                var res = _sieveProcessor
                    .Apply(sieveModel, db)
                    .ProjectTo<OrderListDto>(_mapper.ConfigurationProvider)
                    .ToList();

                for (var i = 0; i < res.Count; i++)
                {
                    res[i].ProductDto = _mapper.Map<ProductDto>(_context.Products.FirstOrDefault(p => p.Id == res[i].ProductId));
                }

                var total = _sieveProcessor
                    .Apply(sieveModel, db, applyPagination: false)
                    .Count();
                return new HbzsResult<List<OrderListDto>>(res, total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<List<OrderListDto>>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public HbzsResult<OrderDto> Get(int id)
        {
            try
            {
                var order = _context.Orders.FirstOrDefault(o => o.Id == id && o.MemberId == Member.Id);

                //  OrderDto dto = null;
                //  if (order != null)
                //  {
                //     dto = _mapper.Map<OrderDto>(order);
                // }
                var dto = _mapper.Map<OrderDto>(order);

                return new HbzsResult<OrderDto>(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<OrderDto>(HbzsResultCode.Invalid_Error, ex.Message);
            }
        }


        [HttpPost("{productId}")]
        public HbzsResult<OrderDto> Post(PostOrderDto postOrderDto)
        {
            try
            {
                var product = _context.Products.First(p => p.Check == true && p.Id == postOrderDto.ProductId);
                var order = new Order()
                {
                    MemberId = Member.Id,
                    Timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffffff"),
                    MemberPhone = Member.Phone,
                    ProductId = product.Id,
                    ProducName = product.Name,
                    CustomerName = postOrderDto.CustomerName,
                    CustomerPhone = postOrderDto.CustomerPhone,
                    Amount = product.Price,
                    AddTime = DateTime.Now,
                    OrderState = OrderState.待付款,
                    OrderPaymentType = OrderPaymentType.支付宝
                };
                _context.Add(order);
                _context.SaveChanges();


                var dto = _mapper.Map<OrderDto>(order);

                return new HbzsResult<OrderDto>(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<OrderDto>(HbzsResultCode.Invalid_Error, "产品不存在");
            }
        }

        [HttpPost("[action]")]
        public HbzsResult<bool> Delete(int id)
        {
            try
            {
                var order = _context.Orders
                            .Where(o => o.OrderState == OrderState.待付款 && Member.Id == o.MemberId && o.Id == id)
                            .First();
                _context.Orders.Remove(order);
                _context.SaveChanges();
                return new HbzsResult<bool>(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsResult<bool>(HbzsResultCode.Invalid_Error, "订单删除失败");
            }

        }
    }
}
