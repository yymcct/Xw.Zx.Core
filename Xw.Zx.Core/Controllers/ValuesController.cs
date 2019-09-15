using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xw.Zx.Core.Helper;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Utility;

namespace Xw.Zx.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMailService _mailService = null;
        private readonly XwZxContext _xwZxContext;
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(IMailService mailService, XwZxContext xwZxContext, ILogger<ValuesController> logger)
        {
            _mailService = mailService;
            _xwZxContext = xwZxContext;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
           // _mailService.MailSyanc(1195);
            return "同步完成";
        }


        [HttpGet]
        public ActionResult<string> GetHtml()
        {
            var mails = _xwZxContext.MailSrcs.OrderByDescending(m => m.Id).Take(10).ToList();
             _logger.LogDebug($"解析开始 合计:{mails.Count}");
            foreach (var mail in mails)
            {
                try
                {
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(mail.Body);
                    var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("/html/body/span[5]/table/tr/td/table/tr/td[3]");
                    _logger.LogDebug($"解析正常, uids:{mail.Uid} {htmlBody.InnerText}");
                }
                catch (Exception ex)
                {
                    _logger.LogDebug($"解析异常 uids:{mail.Uid}");
                }

            }


          
            ///html/body/span[5]/table/tbody/tr/td/table/tbody/tr/td[3]
            return "完毕";
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetQQmailAsync()
        {
            QQMailSync qQMailSync = new QQMailSync().Init("fcc7Rkf4RmMfU1IPbqi9A52V,4,c9du-YpJvbq0.", "pgv_pvi=7372097536; RK=NbhlGJuJRz; ptcz=d1ab0013e91d678c776fa11601fc970a7c16ba4e10decb8bdb651c8f5e3efde5; webp=1; pgv_pvid=5233643110; pgv_info=ssid=s3697213894; pac_uid=0_5d4f78e505e7c; pgv_si=s4314483712; ptisp=cnc; wimrefreshrun=0&; qm_logintype=qq; qm_flag=0; qm_domain=https://mail.qq.com; edition=mail.qq.com; foxacc=68771803&1|19202490&1; newpt=2; ptui_loginuin=68771803; mcookie=0&y; FTN5K=c80dc502; qm_ptsk=68771803&@I3kQ0U6Wt|19202490&@stHtW9C4m; promote_iphone=1; CCSHOW=000000; device=iPad; qm_loginfrom=68771803&psaread|19202490&wpt; tinfo=1568463737.0000*; username=19202490&19202490; uin=o0019202490; skey=@zEoPX2s47; luin=o0019202490; lskey=0001000014ae03a92227d6962006626b2baa1f5642d1c1553e55aa2e455cf8c0a9bb0aafb85b93ee0dda5853; p_uin=o0019202490; pt4_token=sv3fRq2OcyJiF76rLcnWLSrBfY-26tHtWZVkZP98Q-s_; p_skey=B0NBtD3FiWalk7s6JCS1sh2DYevBqLkZkbeiWGeYSLk_; p_luin=o0019202490; p_lskey=000400006143d3ab3602365213939f24f2af808389afa117ab81711b5c9ad92851caa6a541375f86de767dad; qqmail_alias=xiawei1981@foxmail.com; msid=fcc7Rkf4RmMfU1IPT_69AZ2X,4,c9du-YpJvbq0.; sid=19202490&8285558e8257fb77f664672dbd08806f,c9du-YpJvbq0.; qm_username=19202490; ssl_edition=sail.qq.com; pcache=ddc9e0c35836438MTU3MTA1NTc3Ng@19202490@4; mpwd=1A01962AD3F0C5DA6F61B555E27DA21E93DDC420CA9FA5785E41DBB34EC9D915@19202490@4; qm_sk=68771803&fKLQO-Ct|19202490&bqi9A52V; new_mail_num=68771803&0|19202490&228; device=; qm_ssum=68771803&ee4eb361c3068f263f1deb80b8917911|19202490&345bed9450434ab745acb4969c4b8346");
            var mils = await qQMailSync.SearchByFrom("ccsvc%40message.cmbchina.com");
            mils = mils.Where(m => m.Subj == "信用管家消费提醒").ToList();
            foreach (var m in mils)
            {
                _logger.LogDebug(m.Subj + m.Id);
            }
            return "执行完毕";
        }



    }
}
