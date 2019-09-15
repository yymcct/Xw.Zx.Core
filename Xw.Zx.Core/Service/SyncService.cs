using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service.Parse;

namespace Xw.Zx.Core.Service
{
    public class SyncService : ISyncService
    {
        private readonly ILogger<SyncService> _logger;
        private readonly XwZxContext _xwZxContext;
        private readonly IQQMailService _qqMailService;
        private PostSyncMailDto _postSyncMailDto;
        private IMailService _mailService;
        private string ZhaoShangMailUrl = "ccsvc@message.cmbchina.com";
        public SyncService(ILogger<SyncService> logger
            , IQQMailService qqMailService
            , XwZxContext xwZxContext)
        {
            _logger = logger;
            _qqMailService = qqMailService;
            _xwZxContext = xwZxContext;
        }

        public async Task<(bool, string)> SyncAsync(PostSyncMailDto postSyncMailDto)
        {
            _postSyncMailDto = postSyncMailDto;
            if (postSyncMailDto.Mail.Contains("@qq.com"))
            {
                _mailService = _qqMailService.Init(_postSyncMailDto.Sid, _postSyncMailDto.Cookie);
            }
            await ZhaoShangSyncAsync();
            return (true, "");
        }

        private async Task ZhaoShangSyncAsync()
        {

            //同步招商银行
            var mils = await _mailService.SearchByFrom(ZhaoShangMailUrl);

            //只取信用消费清单
            mils = mils.Where(m => m.Subj == "信用管家消费提醒").OrderBy(m=>m.Date).ToList();

            //检查最后一次同步的邮件时间
            var lastSyncDate = GetDbLastMail(ZhaoShangMailUrl, _postSyncMailDto.Mail);

            if (lastSyncDate != null)
            {
                mils = mils.Where(m => m.Date > lastSyncDate).ToList();
            }


            foreach (var m in mils)
            {
                var mail = await _mailService.GetMail(m.Id);
                IMailParse mailParse = new ZhaoShangParse();
                mailParse.Parse(mail.BodyText);
                _xwZxContext.MailSrcs.Add(mail);
                _xwZxContext.SaveChanges();               
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// 获取最后一次同步的ID
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        private DateTime? GetDbLastMail(string fromMail, string toMail)
        {
            var mail = _xwZxContext
                            .MailSrcs
                            .Where(m => m.To == toMail && m.From == fromMail)
                            .OrderByDescending(m => m.SendTime)
                            .FirstOrDefault();

            return mail?.SendTime;
        }
    }
}
