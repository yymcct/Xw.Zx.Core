using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service.Parse
{

    public class MailParseService : IMailParseService
    {
        private readonly IZhaoShangParseService _zhaoShangParseService;
        public MailParseService(IZhaoShangParseService zhaoShangParseService)
        {
            _zhaoShangParseService = zhaoShangParseService;
        }
        public void Parse(int member)
        {
            _zhaoShangParseService.Member = member;
            _zhaoShangParseService.Parse();
        }
    }
}
