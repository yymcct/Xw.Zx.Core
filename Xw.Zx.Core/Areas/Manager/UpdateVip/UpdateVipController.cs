using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sieve.Services;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service;

namespace Xw.Zx.Core.Areas.Manager.UpdateVip
{
    [ApiController]
    [Route("manager/[controller]/[action]")]
    [Authorize(Policy = "Admins")]
    public class UpdateVipController : ManagerBaseController
    {
        private readonly ILogger<UpdateVipController> _logger;
        private readonly IUpDateVip1Service _upDateVip1Service;

        public UpdateVipController(ILogger<UpdateVipController> logger
            , XwZxContext context
            , IMapper mapper
            , ISieveProcessor sieveProcessor
            , IUpDateVip1Service upDateVip1Service) : base(context, mapper, sieveProcessor)
        {
            _logger = logger;
            _upDateVip1Service = upDateVip1Service;
        }


        /// <summary>
        /// 升级VIP
        /// </summary>
        /// <param name="postUpdateVipMDto"></param>
        [HttpPost]
        public HbzsManagerResult UpdateVip([FromBody]PostUpdateVipMDto postUpdateVipMDto)
        {
            try
            {
                if (postUpdateVipMDto.Amount < 0m)
                { 
                    return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, "金额不能为0");
                }
                var order = CreateOrder(postUpdateVipMDto);

                _upDateVip1Service.PaymentedOrderHandle(order);

                return new HbzsManagerResult(HbzsManagerResultCode.Sucess, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new HbzsManagerResult(HbzsManagerResultCode.Invalid_Error, ex.Message);
            }
        }



        private Order CreateOrder(PostUpdateVipMDto postUpdateVipMDto)
        {
            var member = _context.Members.First(m => m.Id == postUpdateVipMDto.MemberId);

            var product = GetProductByVipType(postUpdateVipMDto.MemberVipType);

            var order = new Order()
            {
                MemberId = member.Id,
                Timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffffff"),
                MemberPhone = member.Phone,
                ProductId = product.Id,
                ProducName = product.Name,
                Amount = postUpdateVipMDto.Amount,
                AddTime = DateTime.Now,
                OrderState = OrderState.待付款,
                OrderPaymentType = OrderPaymentType.线下,
            };
            _context.Add(order);
            _context.SaveChanges();


            return order;
        }

        private Product GetProductByVipType(MemberVipType memberVipType)
        {
            string producName = "";

            switch (memberVipType)
            {
                case MemberVipType.客户:
                    producName = "升级客户"; break;
                case MemberVipType.大区经理:
                    producName = "升级大区经理"; break;
                case MemberVipType.运营中心:
                    producName = "升级运营中心"; break;
                case MemberVipType.分公司:
                    producName = "升级分公司"; break;
            }

            return _context.Products.First(p => p.Name == producName);
        }

    }
}