using Microsoft.Extensions.Logging;
using NLog;
using S22.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Utility
{
    public class MailService : IMailService
    {
        private string _hostName;
        private int _port;
        bool _ssl;
        string _username;
        string _password;
        private readonly XwZxContext _xwZxContext;
        private readonly ILogger<MailService> _logger;

        public MailService(XwZxContext xwZxContext, ILogger<MailService> logger)
        {
            _xwZxContext = xwZxContext;
            _logger = logger;
        }

        //public MailService(string username, string pwd, string hostname, int port, bool ssl = true)
        //{
        //    _hostName = hostname;
        //    _port = port;
        //    _username = username;
        //    _password = pwd;
        //    _ssl = ssl;
        //}



        public string GetBankMailUrl(MailSrcBank mailSrcBank)
        {
            switch (mailSrcBank)
            {
                case MailSrcBank.招商银行:
                    return "ccsvc@message.cmbchina.com";
                default:
                    return "";
            }
        }
        public void MailSyanc()
        {
            List<uint> bankmailId = new List<uint>();
            using (ImapClient client = new ImapClient("imap.qq.com", 993, true))
            {
                client.Login("yymcct@qq.com", "kkwuwrmwizdubjdg", AuthMethod.Login);
                var uids = client.Search(SearchCondition.All()).ToList();
                for (var i = 0; i < uids.Count(); i++)
                {
                    try
                    {
                        var msg = client.GetMessage(uids[i], FetchOptions.HeadersOnly);
                        if (msg.From.Address == "ccsvc@message.cmbchina.com" && msg.Subject.IndexOf("电子账单") > 0)
                        {
                            msg = client.GetMessage(uids[i], FetchOptions.Normal);
                            MailSrc bankBill = new MailSrc()
                            {
                                Uid = i.ToString(),
                                Sublic = msg.Subject,
                                From = msg.From.Address,
                                To = "yymcct@qq.com",
                                Body = msg.Body,
                                SendTime = DateTime.Parse(msg.Headers.Get("Date")),
                                AddTime = DateTime.Now,
                            };

                            _xwZxContext.MailSrcs.Add(bankBill);
                            _xwZxContext.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"uids:{i} 异常信息:{ex.Message}");
                    }
                }
            }
        }
    }
}
