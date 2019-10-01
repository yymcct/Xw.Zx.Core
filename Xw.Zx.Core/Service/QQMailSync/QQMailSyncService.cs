using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public class QQMailSyncService : IQQMailSyncService
    {
        private int _memberId = 0;

        private readonly ILogger<QQMailSyncService> _logger;
        private readonly XwZxContext _xwZxContext;
        private readonly IQQMailService _qqMailService;
        private readonly IZhaoShangParseService  _zhaoShangParseService;
        private readonly IGuangfaParseService  _guangfaParseService;
        private readonly IZhongxinParseService  _zhongxinParseService;
        public QQMailSyncService(ILogger<QQMailSyncService> logger
             , XwZxContext xwZxContext
            , IQQMailService qqMailService
            , IZhaoShangParseService zhaoShangParseService
            , IGuangfaParseService guangfaParseService
            , IZhongxinParseService zhongxinParseService)
        {
            _logger = logger;
            _qqMailService = qqMailService;
            _xwZxContext = xwZxContext;
            _zhaoShangParseService = zhaoShangParseService;
            _guangfaParseService = guangfaParseService;
            _zhongxinParseService = zhongxinParseService;
        }

        public void Init(int memberId, string sid, string cookie)
        {
            _memberId = memberId;
            _qqMailService.Init(sid, cookie);
        }

        public async Task SyncMailAsync()
        {
            var uids = await _qqMailService.SearchByZhaoshang();
            await SaveMails(uids);

            uids = await _qqMailService.SearchByGuangfa();
            await SaveMails(uids);

            uids = await _qqMailService.SearchByZhongxin();
            await SaveMails(uids);

            uids = await _qqMailService.SearchByGuangZhouYingHang();
            await SaveMails(uids);

            _zhaoShangParseService.Member = _memberId;
            _zhaoShangParseService.Parse();

            _guangfaParseService.Member = _memberId;
            _guangfaParseService.Parse();

            _zhongxinParseService.Member = _memberId;
            _zhongxinParseService.Parse();
        }





        private async Task SaveMails(List<string> uids)
        {
            foreach (var uid in uids)
            {
                if (!MailIsExist(uid))
                {
                    var mail = await _qqMailService.GetMail(uid);
                    mail.MemberId = _memberId;

                    _xwZxContext.Add(mail);
                    _xwZxContext.SaveChanges();
                }
            }
        }

        private bool MailIsExist(string uid) => _xwZxContext.MailSrcs.Any(m => m.MemberId == _memberId
                                                                                        && m.Uid == uid);

    }
}
