using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xw.Zx.Core.Helper;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Service
{
    public class QQMailService: IQQMailService
    {
        private readonly ILogger<QQMailService> _logger;
        private readonly Uri _baseAddress = new Uri("https://w.mail.qq.com");
        private string _sid = "";
        private string _cookies = "";
        private HttpClient _client;


        public QQMailService(ILogger<QQMailService> logger)
        {
            _logger = logger;
        }

        public IMailService Init(string sid, string cookie)
        {
            _sid = sid;
            _cookies = cookie;

            var cookieContainer = new CookieContainer();
            foreach (var item in cookie.Split(';'))
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    var str = item;
                    if (str.Contains("msid")) continue;
                    if (str.Contains("sid"))
                    {
                        str = str.Split(',')[0];
                    }
                    var tmp = str.Split('=');
                    cookieContainer.Add(_baseAddress, new Cookie(tmp[0].Trim(), tmp[1].Trim()));
                }
            }
            var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            _client = new HttpClient(handler) { BaseAddress = _baseAddress };

            _client.DefaultRequestHeaders.Add("Accept", "*/*");        
            _client.DefaultRequestHeaders.Add("Accept-Encoding", "*gzip, deflate*");
            _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows Phone 10.0;  Android 4.2.1; Nokia; Lumia 520) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.152 Mobile Safari/537.36 Edge/12.0");
            _client.DefaultRequestHeaders.Add("Host", "w.mail.qq.com");
            _client.DefaultRequestHeaders.Add("Connection", "Keep-Alive");

            _logger.LogDebug($"开始初始化sid:{sid} cookie{cookie}");

            return this;
        }
        public async Task<List<MailInfoDto>> SearchByFrom(string fromMail)
        {
            string resStr = "";
            try
            {
                string uri = $"/cgi-bin/mail_list?ef=js&sid={_sid}&t=mobile_data.json&s=list&cursor=max&cursorcount=100&cursorsearch=1&folderid=all&sender={fromMail}&combinetype=or&device=unknow&app=phone&ver=app";

                resStr = await _client.GetStringAsync(uri);
                JObject res = JObject.Parse(resStr);
                var mls = (JArray)res["mls"];

                var mailInfo = mls.Select(c => new MailInfoDto()
                {
                    Id = (string)c["inf"]["id"],
                    Subj = (string)c["inf"]["subj"],
                    Date = TypeHelper.UnixTimeToDateTime((int)c["inf"]["date"]),
                })
                .ToList();

                return mailInfo;
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"[SearchByFrom]错误:sid:{_sid};cookies:{_cookies};fromMail:{fromMail};resStr:{resStr};Exception:{ex.Message}");
            }
            return new List<MailInfoDto>();
        }

        public async Task<List<MailInfoDto>> SearchByFrom(string fromMail, string keyword)
        {
            string resStr = "";
            try
            {
                //string uri = $"/cgi-bin/mail_list?ef=js&sid={_sid}&t=mobile_data.json&s=list&cursor=max&cursorcount=100&cursorsearch=1&folderid=all&sender={fromMail}&combinetype=or&device=unknow&app=phone&ver=app";
                string uri = $"/cgi-bin/mail_list?ef=js&sid={_sid}&t=mobile_data.json&s=list&searchmode=advance&page=0&topmails=0&subject={keyword}&receiver=&sender=ccsvc%40message.cmbchina.com&advancesearch=2&flagnew=&attach=&position=0&folderid=all&daterange=8&resp_charset=UTF8";
                resStr = await _client.GetStringAsync(uri);
                JObject res = JObject.Parse(resStr);
                var mls = (JArray)res["mls"];

                var mailInfo = mls.Select(c => new MailInfoDto()
                {
                    Id = (string)c["inf"]["id"],
                    Subj = (string)c["inf"]["subj"],
                    From = (string)c["inf"]["from"]["addr"],
                    To = (string)c["inf"]["toLst"][0]["addr"],
                    Date = TypeHelper.UnixTimeToDateTime((int)c["inf"]["date"]),
                })
                .ToList();

                return mailInfo;
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"[SearchByFrom]错误:sid:{_sid};cookies:{_cookies};fromMail:{fromMail};resStr:{resStr};Exception:{ex.Message}");
            }
            return null;
        }

        public async Task<List<MailInfoDto>> SearchByFrom(string fromMail, int page, int pagesize)
        {
            string resStr = "";
            try
            {               
                string uri = $"/cgi-bin/mail_list?sid={_sid}&t=mobile_data.json&s=list&page={page}&pagesize={pagesize}&folderid=all&topmails=0&subject={fromMail}";
                resStr = await _client.GetStringAsync(uri);
                JObject res = JObject.Parse(resStr);
                var mls = (JArray)res["mls"];

                var mailInfo = mls.Select(c => new MailInfoDto()
                {
                    Id = (string)c["inf"]["id"],
                    Subj = (string)c["inf"]["subj"],
                    From = (string)c["inf"]["from"]["addr"],
                    To = (string)c["inf"]["toLst"][0]["addr"],
                    Date = TypeHelper.UnixTimeToDateTime((int)c["inf"]["date"]),
                })
                .ToList();

                return mailInfo;
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"[SearchByFrom]错误:sid:{_sid};cookies:{_cookies};fromMail:{fromMail};resStr:{resStr};Exception:{ex.Message}");
            }
            return new List<MailInfoDto>();
        }

        public async Task<List<string>> SearchByZhaoshang()
        {
            string resStr = "";
            var mailIds = new List<string>();
            try
            {
                string uri = $"/cgi-bin/mail_list?sid={_sid}&s=search&folderid=all&page=0&subject=ccsvc@message.cmbchina.com%20%D1%AD%BB%B7%C0%FB%CF%A2&sender=ccsvc@message.cmbchina.com%20%D1%AD%BB%B7%C0%FB%CF%A2&receiver=ccsvc@message.cmbchina.com%20%D1%AD%BB%B7%C0%FB%CF%A2&searchmode=&topmails=0&advancesearch=0&loc=frame_html,,,6";
                resStr = await _client.GetStringAsync(uri);

                var matchs = new Regex(@"mailid=(.+?)\&").Matches(resStr);
                
                foreach (Match m in matchs)
                {
                    var id = m.Groups[1].Value;

                    mailIds.Add(id);
                }

                return mailIds;
            }
            catch (Exception ex)
            {
               // _logger.LogDebug($"[SearchByFrom]错误:sid:{_sid};cookies:{_cookies};fromMail:{fromMail};resStr:{resStr};Exception:{ex.Message}");
            }
            return mailIds;
        }

        public async Task<List<string>> SearchByGuangfa()
        {
            string resStr = "";
            var mailIds = new List<string>();
            try
            {
                string uri = $"/cgi-bin/mail_list?sid={_sid}&s=search&folderid=all&page=0&subject=creditcard@cgbchina.com.cn%20%C1%E3%CA%DB%C0%FB%CF%A2&sender=creditcard@cgbchina.com.cn%20%C1%E3%CA%DB%C0%FB%CF%A2&receiver=creditcard@cgbchina.com.cn%20%C1%E3%CA%DB%C0%FB%CF%A2&searchmode=&topmails=0&advancesearch=0&loc=frame_html,,,6";
                resStr = await _client.GetStringAsync(uri);

                var matchs = new Regex(@"mailid=(.+?)\&").Matches(resStr);

                foreach (Match m in matchs)
                {
                    var id = m.Groups[1].Value;

                    mailIds.Add(id);
                }

                return mailIds;
            }
            catch (Exception ex)
            {
                // _logger.LogDebug($"[SearchByFrom]错误:sid:{_sid};cookies:{_cookies};fromMail:{fromMail};resStr:{resStr};Exception:{ex.Message}");
            }
            return mailIds;
        }
        public async Task<List<string>> SearchByZhongxin()
        {
            string resStr = "";
            var mailIds = new List<string>();
            try
            {
                string uri = $"/cgi-bin/mail_list?sid={_sid}&s=search&folderid=all&page=0&subject=citiccard@bill.citiccard.com%20%C0%FB%CF%A2&sender=citiccard@bill.citiccard.com%20%C0%FB%CF%A2&receiver=citiccard@bill.citiccard.com%20%C0%FB%CF%A2&searchmode=&topmails=0&advancesearch=0&loc=frame_html,,,6";
                resStr = await _client.GetStringAsync(uri);

                var matchs = new Regex(@"mailid=(.+?)\&").Matches(resStr);

                foreach (Match m in matchs)
                {
                    var id = m.Groups[1].Value;

                    mailIds.Add(id);
                }

                return mailIds;
            }
            catch (Exception ex)
            {
                // _logger.LogDebug($"[SearchByFrom]错误:sid:{_sid};cookies:{_cookies};fromMail:{fromMail};resStr:{resStr};Exception:{ex.Message}");
            }
            return mailIds;
        }

        public async Task<MailSrc> GetMail(string mailid)
        {
            string resStr = "";
            try
            {
                string uri = $"/cgi-bin/readmail?ef=js&sid={_sid}&t=mobile_data.json&s=read&showreplyhead=1&disptype=html&mailid={mailid}";

                resStr = await _client.GetStringAsync(uri);
                JObject res = JObject.Parse(resStr);
                var mls = (JToken)res["mls"][0];

                var mail = new MailSrc
                {
                    Uid = (string)mls["inf"]["id"],
                    Sublic = (string)mls["inf"]["subj"],
                    From = (string)mls["inf"]["from"]["addr"],
                    To = (string)mls["inf"]["toLst"][0]["addr"],
                    Body = (string)mls["content"]["body"],
                    BodyText = (string)mls["content"]["bodytext"],
                    SendTime = TypeHelper.UnixTimeToDateTime((int)mls["inf"]["date"]),
                    AddTime = DateTime.Now
                };

                return mail;
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"[SearchByFrom]错误:sid:{_sid};cookies:{_cookies};mailid:{mailid};resStr:{resStr};Exception:{ex.Message}");
            }
            return null;
        }
    }
}
