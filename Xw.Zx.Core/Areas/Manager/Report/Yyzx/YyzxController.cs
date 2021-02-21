using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npoi.Mapper;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Areas.Manager.Coupon.Dtos;
using Xw.Zx.Core.Areas.Manager.Report.Yyzx.Dtos;
using Xw.Zx.Core.Extensions;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service;

namespace Xw.Zx.Core.Areas.Manager.Coupon
{
    [ApiController]
    [Route("manager/[controller]/[action]")]
    //  [Authorize(Policy = "Admins")] TODO

    /// <summary>
    /// 导出运营中心
    /// </summary>
    public class YyzxController
    {
        private readonly ILogger<YyzxController> _logger;
        public readonly XwZxContext _context;
        public readonly AutoMapper.IMapper _mapper;
        public YyzxController(ILogger<YyzxController> logger
            , AutoMapper.IMapper mapper
           , XwZxContext context)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        private bool IsTopYyzx(int id)
        {
            var invertId = _context.Members.First(m => m.Id == id).InviteId;
            do
            {
                var invertMember = _context.Members.FirstOrDefault(m => m.Id == invertId);
                if ((int)(invertMember.MemberVipType) == 20)
                {
                    return false;
                }
                if (invertMember == null || invertMember.Id == 6)
                {
                    return true;
                }
                invertId = invertMember.InviteId;

            } while (true);
        }

        [HttpGet]
        public string Get()
        {
            var memberId = 47;
            var startTime = "2021-01-01";
            var endTime = "2021-02-01";

            var ymembers = _context.Members
                .Where(m => m.MemberVipType == MemberVipType.运营中心)
                .ProjectTo<Yyzx>(_mapper.ConfigurationProvider)
                .ToArray();

            foreach (var m in ymembers)
            {
                try
                {
                    if (IsTopYyzx(m.Id))
                    {
                        var mapper1 = new Mapper();
                        mapper1.Map<YyzxDetail>("ID", d => d.Id)
                       .Map<YyzxDetail>("上级ID", d => d.InviteId)
                       .Map<YyzxDetail>("姓名", d => d.RealName)
                       .Map<YyzxDetail>("手机", d => d.Phone)
                       .Map<YyzxDetail>("邀请层级", d => d.T)
                       .Map<YyzxDetail>("交易合计", d => d.TotalAmount);
                        var sql = $"exec GetAmount @memberId = {m.Id},@startTime = '{startTime}',@endTime = '{endTime}'";
                        var detail = _context.Database.SqlQuery<YyzxDetail>(sql);
                        if (detail.Count > 0)
                        {
                            mapper1.Save($"UpLoad\\{m.Phone}.{startTime}至{endTime}.xlsx", detail, "sheet1", overwrite: true, xlsx: true);
                        }

                        m.TotalAmount = detail.Sum(d => d.TotalAmount);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

            }
            ymembers = ymembers.Where(y => y.TotalAmount > 0).ToArray();
            var mapper = new Mapper();
            mapper.Map<Yyzx>("ID", m => m.Id)
                .Map<Yyzx>("手机", m => m.Phone)
                .Map<Yyzx>("姓名", m => m.RealName)
                .Map<Yyzx>("等级", m => m.MemberVipType)
                .Map<Yyzx>("交易合计", m => m.TotalAmount);
            mapper.Save($"UpLoad\\运营中心合计.{startTime}至{endTime}.xlsx", ymembers, "sheet1", overwrite: true, xlsx: true);

            return "Students.xlsx";
        }



        private bool IsTopXtsuoYyzx(int id)
        {
            var invertId = _context.Xtsuo_Members.First(m => m.Id == id).AgentId;
            do
            {
                var invertMember = _context.Xtsuo_Members.FirstOrDefault(m => m.Id == invertId);
        
                if (invertMember == null || invertMember.Id == 0)
                {
                    return true;
                }
                if (invertMember.Level == 7)
                {
                    return false;
                }
                invertId = invertMember.AgentId;

            } while (true);
        }

        [HttpGet]
        public string GetXtsuo()
        {
            var startTime = "2021-01-01";
            var endTime = "2021-02-01";

            var ymembers = _context.Xtsuo_Members
                .Where(m => m.Level == 7)
                .ProjectTo<Yyzx>(_mapper.ConfigurationProvider)
                .ToArray();

            foreach (var m in ymembers)
            {
                try
                {
                    if (IsTopXtsuoYyzx(m.Id))
                    {
                        var mapper1 = new Mapper();
                        mapper1.Map<YyzxDetail>("ID", d => d.Id)
                       .Map<YyzxDetail>("上级ID", d => d.InviteId)
                       .Map<YyzxDetail>("姓名", d => d.RealName)
                       .Map<YyzxDetail>("手机", d => d.Phone)
                       .Map<YyzxDetail>("邀请层级", d => d.T)
                       .Map<YyzxDetail>("交易合计", d => d.TotalAmount);
                        var sql = $"exec GetXtsuoAmount @memberId = {m.Id},@startTime = '{startTime}',@endTime = '{endTime}'";
                        var detail = _context.Database.SqlQuery<YyzxDetail>(sql);
                        if (detail.Count > 0)
                        {
                            mapper1.Save($"UpLoad\\{m.Phone.Replace(" ","")}.{startTime}至{endTime}.公众号.xlsx", detail, "sheet1", overwrite: true, xlsx: true);
                        }

                        m.TotalAmount = detail.Sum(d => d.TotalAmount);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

            }
            ymembers = ymembers.Where(y => y.TotalAmount > 0).ToArray();
            var mapper = new Mapper();
            mapper.Map<Yyzx>("ID", m => m.Id)
                .Map<Yyzx>("手机", m => m.Phone)
                .Map<Yyzx>("姓名", m => m.RealName)
                .Map<Yyzx>("等级", m => m.MemberVipType)
                .Map<Yyzx>("交易合计", m => m.TotalAmount);
            mapper.Save($"UpLoad\\运营中心合计.{startTime}至{endTime}.公众号.xlsx", ymembers, "sheet1", overwrite: true, xlsx: true);

            return "Students.xlsx";
        }


    }
}
