using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Dto;
using Xw.Zx.Core.Models.Model;


namespace Xw.Zx.Core.Service
{
    public class SyncService : ISyncService
    {
        private readonly ILogger<SyncService> _logger;
        private readonly XwZxContext _xwZxContext;
        private readonly IQQMailService _qqMailService;
        private readonly IMailSyncService _mailSync;
        private readonly IMailParseService _mailParseService;


        public SyncService(ILogger<SyncService> logger
            , IQQMailService qqMailService
            , XwZxContext xwZxContext
            , IMailSyncService mailSync
            , IMailParseService mailParseService)
        {
            _logger = logger;
            _qqMailService = qqMailService;
            _xwZxContext = xwZxContext;
            _mailSync = mailSync;
            _mailParseService = mailParseService;
        }

        public PostSyncMailResuleDto SyncAsync(PostSyncMailDto postSyncMailDto)
        {
            _mailSync.MemberId = postSyncMailDto.MemberId;
            _mailSync.MailService = _qqMailService.Init(postSyncMailDto.Sid, postSyncMailDto.Cookie);

            _mailSync.SyncMailDirToDb();
            _mailSync.SyncMailToDb();

            _mailParseService.Parse(postSyncMailDto.MemberId);

            return BuildSyncInfo(postSyncMailDto);

        }


        public PostSyncMailResuleDto BuildSyncInfo(PostSyncMailDto postSyncMailDto)
        {
            var res = new PostSyncMailResuleDto()
            {
                LastSyncTime = DateTime.Now,
                BankBillAmount = "0"
            };
            var mail = _xwZxContext.MailSrcs.Where(m => m.MemberId == postSyncMailDto.MemberId).OrderBy(m => m.SendTime).FirstOrDefault();
            if (mail != null)
            {
                res.LastSyncTime = mail.SendTime;
            }
            var amount = _xwZxContext.BankBillDetails.Where(b => b.MemberID == postSyncMailDto.MemberId).Sum(b => b.Amount);

            res.BankBillAmount = amount.ToString();
            return res;
        }
    }
}
