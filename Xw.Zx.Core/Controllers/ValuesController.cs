using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            _mailService.MailSyanc(1098);
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


    }
}
