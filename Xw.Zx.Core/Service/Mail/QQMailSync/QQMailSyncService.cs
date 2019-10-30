using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        private readonly IZhaoShangParseService _zhaoShangParseService;
        private readonly IGuangfaParseService _guangfaParseService;
        private readonly IZhongxinParseService _zhongxinParseService;
        private readonly IGuangZhouYinHangParseService _guangZhouYinHangParseService;
        private readonly IYouZhengParseService _youZhengParseService;
        private readonly IJiaoTongParseService _jiaoTongParseService;
        private readonly IGuangDaParseService _guangDaParseService;
        private readonly IMengShengParseService _mengShengParseService;
        private readonly IPingAnParseService _pingAnParseService;
        private readonly IJianSheParseService _jianSheParseService;

        public QQMailSyncService(ILogger<QQMailSyncService> logger
             , XwZxContext xwZxContext
            , IQQMailService qqMailService
            , IZhaoShangParseService zhaoShangParseService
            , IGuangfaParseService guangfaParseService
            , IZhongxinParseService zhongxinParseService
            , IGuangZhouYinHangParseService guangZhouYinHangParseService
            , IYouZhengParseService youZhengParseService
            , IJiaoTongParseService jiaoTongParseService
            , IGuangDaParseService guangDaParseService
            , IMengShengParseService mengShengParseService
            , IPingAnParseService pingAnParseService
            , IJianSheParseService jianSheParseService)
        {
            _logger = logger;
            _qqMailService = qqMailService;
            _xwZxContext = xwZxContext;
            _zhaoShangParseService = zhaoShangParseService;
            _guangfaParseService = guangfaParseService;
            _zhongxinParseService = zhongxinParseService;
            _guangZhouYinHangParseService = guangZhouYinHangParseService;
            _youZhengParseService = youZhengParseService;
            _jiaoTongParseService = jiaoTongParseService;
            _guangDaParseService = guangDaParseService;
            _mengShengParseService = mengShengParseService;
            _pingAnParseService = pingAnParseService;
            _jianSheParseService = jianSheParseService;
        }

        public void Init(int memberId, string sid, string cookie)
        {
            _memberId = memberId;
            _qqMailService.Init(sid, cookie);
        }

        public async Task SyncMailAsync()
        {
            var maiCnt = 0;

            var uids = await _qqMailService.SearchByZhaoshang();
            await SaveMails(uids);
            _zhaoShangParseService.Member = _memberId;
            _zhaoShangParseService.Parse();

            uids = await _qqMailService.SearchByGuangfa();
            await SaveMails(uids);
            _guangfaParseService.Member = _memberId;
            _guangfaParseService.Parse();

            uids = await _qqMailService.SearchByZhongxin();
            await SaveMails(uids);
            _zhongxinParseService.Member = _memberId;
            _zhongxinParseService.Parse();

            uids = await _qqMailService.SearchByGuangZhouYingHang();
            await SaveMails(uids);
            _guangZhouYinHangParseService.Member = _memberId;
            _guangZhouYinHangParseService.Parse();

            uids = await _qqMailService.SearchByYouZhengYingHang();
            await SaveMails(uids);
            _youZhengParseService.Member = _memberId;
            _youZhengParseService.Parse();

            uids = await _qqMailService.SearchByJiaoTongYingHang();
            await SaveMails(uids);
            _jiaoTongParseService.Member = _memberId;
            _jiaoTongParseService.Parse();



            uids = await _qqMailService.SearchByGuangdaYingHang();
            await SaveMails(uids);
            _guangDaParseService.Member = _memberId;
            _guangDaParseService.Parse();

            uids = await _qqMailService.SearchByMingShengYingHang();
            await SaveMails(uids);
            _mengShengParseService.Member = _memberId;
            _mengShengParseService.Parse();

            uids = await _qqMailService.SearchByJianSheYingHang();
            await SaveMails(uids);
            _jianSheParseService.Member = _memberId;
            _jianSheParseService.Parse();

            uids = await _qqMailService.SearchByPingAnYingHang();
            await SaveMails(uids);
            _pingAnParseService.Member = _memberId;
            _pingAnParseService.Parse();

        }





        private async Task SaveMails(List<string> uids)
        {
            int errCnt = 0;
            foreach (var uid in uids)
            {
                try
                {
                    if (!MailIsExist(uid))
                    {
                        var mail = await _qqMailService.GetMail(uid);
                        if (mail != null)
                        {
                            mail.MemberId = _memberId;

                            mail.From = mail.From.ToLower();
                            _xwZxContext.Add(mail);
                            _xwZxContext.SaveChanges();
                        }
                        else
                        {
                            if (++errCnt > 3)
                                break;
                            Thread.Sleep(5000);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"用户{_memberId},uid:{uid},错误信息:{ex.Message}");
                }

            }
        }

        private bool MailIsExist(string uid) => _xwZxContext.MailSrcs.Any(m => m.MemberId == _memberId
                                                                                        && m.Uid == uid);

    }
}
