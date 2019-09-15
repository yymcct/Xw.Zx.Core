using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Helper
{
    public class QQMailSync
    {
        private Uri _baseAddress = new Uri("https://w.mail.qq.com");
        public string _sid = "";
        public string _cookies = "";
        HttpClient _client;
        public QQMailSync()
        { 
        
        }

        public QQMailSync Init(string sid, string cookie)
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
            //_client.DefaultRequestHeaders.Add("Accept-Language", "*zh-Hans-CN,zh-Hans;q=0.8,en-US;q=0.5,en;q=0.3*");
            _client.DefaultRequestHeaders.Add("Accept-Encoding", "*gzip, deflate*");
            _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows Phone 10.0;  Android 4.2.1; Nokia; Lumia 520) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.152 Mobile Safari/537.36 Edge/12.0");
            _client.DefaultRequestHeaders.Add("Host", "w.mail.qq.com");
            _client.DefaultRequestHeaders.Add("Connection", "Keep-Alive");

            return this;
        }
        //ccsvc%40message.cmbchina.com
        public async Task<List<MailInfo>> SearchByFrom(string fromMail)
        {
            string uri = $"/cgi-bin/mail_list?ef=js&sid={_sid}&t=mobile_data.json&s=list&cursor=max&cursorcount=200&cursorsearch=1&folderid=all&sender={fromMail}&combinetype=or&device=unknow&app=phone&ver=app";

            var resJson = await _client.GetStringAsync(uri);
            JObject res = JObject.Parse(resJson);
            var mls = (JArray)res["mls"];

            var mailInfo = mls.Select(c => new MailInfo()
            {
                Id = (string)c["inf"]["id"],
                Subj = (string)c["inf"]["subj"],
                Date = UnixTimeToDateTime((int)c["inf"]["date"]),
            })
            .ToList();

            return mailInfo;
        }

        public async Task<MailSrc> GetMail(string mailid)
        {
            string uri = $"/cgi-bin/readmail?ef=js&sid={{sid}}&t=mobile_data.json&s=read&showreplyhead=1&disptype=html&mailid=${mailid}";

            var resJson = await _client.GetStringAsync(uri);
            JObject res = JObject.Parse(resJson);
            var mls = (JArray)res["mls"];

            var mailInfo = mls.Select(c => new MailInfo()
            {
                Id = (string)c["inf"]["id"],
                Subj = (string)c["inf"]["subj"],
                Date = UnixTimeToDateTime((int)c["inf"]["date"]),
            })
            .ToList();

            return mailInfo;
        }

        private  DateTime UnixTimeToDateTime(long unixtime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return sTime.AddSeconds(unixtime);
        }
    }


    public class MailInfo
    {
        public string Id { get; set; }
        public string Subj { get; set; }
        public DateTime Date { get; set; }
    }
}
