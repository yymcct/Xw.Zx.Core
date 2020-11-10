
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Schema;

namespace Xw.Zx.Core.Helper
{
    public class Code2SessionRes
    {
        public string Openid { get; set; }
        public string session_key { get; set; }
        public string Unionid { get; set; }
        public int Errcode { get; set; }
        public string Errmsg { get; set; }
    }

    public class WxUser
    {
        public string avatarUrl { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string gender { get; set; }
        public string language { get; set; }
        public string nickName { get; set; }
        public string province { get; set; }
        public string unionId { get; set; }
        public string openid { get; set; }
    }

    public class WxUserInfo
    {
        public string openid { get; set; }
        public string nickname { get; set; }
        public string sex { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public string unionid { get; set; }
    }

    public class AccessToken
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }

        public string unionid { get; set; }

    }

    public class WxAccessToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }

    public class WxJsApiTictket
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string ticket { get; set; }

        public int expires_in { get; set; }
    }
    public class WxMsgSecCheck
    {
        public int errcode { get; set; }

        public string errMsg { get; set; }
    }
    public class WxHelperDic
    {
        public DateTime ExpiresTime { get; set; }
        public string Value { get; set; }
    }
    public class GetLoginToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public int statusCode { get; set; }
        public string msg { get; set; }
    }

    public class GetWxLoginUserInfoByToken
    {
        public string openid { get; set; }
        public string nickname { get; set; }
        public int sex { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public string unionid { get; set; }
    }


    public class WxHelper
    {
        private string _appid;
        private string _secret;
        //static Dictionary<string, WxHelperDic> WxHelperDictionary = new Dictionary<string, WxHelperDic>();

        static protected ConcurrentDictionary<string, WxHelperDic> _wxhelperDic = new ConcurrentDictionary<string, WxHelperDic>();

        public WxHelper(string appid, string secret)
        {
            _appid = appid;
            _secret = secret;
        }

        #region 微信公众平台
        /// <summary>
        /// 获取微信公众平台accesstoken，无则添加，有则判断是否过期
        /// </summary>
        /// <returns></returns>
        public WxAccessToken GetWxShareWxAccessToken()
        {
            if (!_wxhelperDic.ContainsKey("WxShareAccessToken"))
            {
                var gettokenRespose = new HttpClient()
                                                .GetAsync($"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={_appid}&secret={_secret}")
                                                .Result
                                                .Content
                                                .ReadAsStringAsync()
                                                .Result;
                var result = JsonConvert.DeserializeObject<WxAccessToken>(gettokenRespose);
                var wxtokenDic = new WxHelperDic();
                wxtokenDic.Value = result.access_token;
                wxtokenDic.ExpiresTime = DateTime.Now.AddSeconds(result.expires_in);
                _wxhelperDic.TryAdd("WxShareAccessToken", wxtokenDic);
                return result;
            }
            else
            {
                WxHelperDic WxShareAccessToken;
                _wxhelperDic.TryGetValue("WxShareAccessToken", out WxShareAccessToken);
                if (WxShareAccessToken != null && WxShareAccessToken.ExpiresTime < DateTime.Now.AddSeconds(-10))
                {
                    var gettokenRespose = new HttpClient()
                                             .GetAsync($"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={_appid}&secret={_secret}")
                                             .Result
                                             .Content
                                             .ReadAsStringAsync()
                                             .Result;
                    var result = JsonConvert.DeserializeObject<WxAccessToken>(gettokenRespose);
                    var wxtokenDic = new WxHelperDic();
                    wxtokenDic.Value = result.access_token;
                    wxtokenDic.ExpiresTime = DateTime.Now.AddSeconds(result.expires_in);
                    _wxhelperDic.AddOrUpdate("WxShareAccessToken", (key) => wxtokenDic, (key, old) => wxtokenDic);
                    return result;
                }
                else
                {
                    var result = new WxAccessToken();
                    result.access_token = WxShareAccessToken.Value;
                    return result;
                }
            }
        }

        /// <summary>
        /// 获取微信公众平台ticket，无则添加，有则判断是否过期
        /// </summary>
        /// <returns></returns>
        public WxJsApiTictket GetWxShareWxJsApiTictket(string access_token)
        {
            if (!_wxhelperDic.ContainsKey("WxShareTicket"))
            {
                var getjsapiticketRespose = new HttpClient()
                                                         .GetAsync($"https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + access_token + "&type=jsapi")
                                                         .Result
                                                         .Content
                                                         .ReadAsStringAsync()
                                                         .Result;

                var tictket = JsonConvert.DeserializeObject<WxJsApiTictket>(getjsapiticketRespose);
                var wxtictketDic = new WxHelperDic();
                wxtictketDic.Value = tictket.ticket;
                wxtictketDic.ExpiresTime = DateTime.Now.AddSeconds(tictket.expires_in);
                _wxhelperDic.TryAdd("WxShareTicket", wxtictketDic);
                return tictket;
            }
            else
            {
                WxHelperDic WxShareTicket;
                _wxhelperDic.TryGetValue("WxShareTicket", out WxShareTicket);
                if (WxShareTicket != null && WxShareTicket.ExpiresTime < DateTime.Now.AddSeconds(-10))
                {
                    var getjsapiticketRespose = new HttpClient()
                                                       .GetAsync($"https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + access_token + "&type=jsapi")
                                                       .Result
                                                       .Content
                                                       .ReadAsStringAsync()
                                                       .Result;
                    var result = JsonConvert.DeserializeObject<WxJsApiTictket>(getjsapiticketRespose);
                    var wxtokenDic = new WxHelperDic();
                    wxtokenDic.Value = result.ticket;
                    wxtokenDic.ExpiresTime = DateTime.Now.AddSeconds(result.expires_in);
                    _wxhelperDic.AddOrUpdate("WxShareTicket", (key) => wxtokenDic, (key, old) => wxtokenDic);
                    return result;
                }
                else
                {
                    var result = new WxJsApiTictket();
                    result.ticket = WxShareTicket.Value;
                    return result;
                }
            }
        }
        #endregion
        public Code2SessionRes Code2Session(string code)
        {
            //TODO 放入字典
            var getcodeRespose = new HttpClient()
                .GetAsync($"https://api.weixin.qq.com/sns/jscode2session?appid={_appid}&secret={_secret}&js_code={code}&grant_type=authorization_code")
                .Result
                .Content
                .ReadAsStringAsync()
                .Result;
            //throw new Exception(getcodeRespose);
            return JsonConvert.DeserializeObject<Code2SessionRes>(getcodeRespose);
        }
        public WxUser DecodeWxUser(string encryptedData, string iv, string sessionkey)
        {
            var text = AESDecrypt(encryptedData, iv, sessionkey);

            return JsonConvert.DeserializeObject<WxUser>(text);
        }
        private string AESDecrypt(string text, string iv, string sessionkey)
        {
            try
            {
                byte[] encryptedData = Convert.FromBase64String(text);  // strToToHexByte(text);
                RijndaelManaged rijndaelCipher = new RijndaelManaged();
                rijndaelCipher.Key = Convert.FromBase64String(sessionkey); // Encoding.UTF8.GetBytes(AesKey);
                rijndaelCipher.IV = Convert.FromBase64String(iv);// Encoding.UTF8.GetBytes(AesIV);
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
                byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                string result = Encoding.Default.GetString(plainText);
                return result;
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        /// <summary>
        /// 公众号网页获取用户信息的token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public AccessToken Code2Accesstoken(string code)
        {
            var getcodeRespose = new HttpClient()
               .GetAsync($"https://api.weixin.qq.com/sns/oauth2/access_token?appid={_appid}&secret={_secret}&code={code}&grant_type=authorization_code")
               .Result
               .Content
               .ReadAsStringAsync()
               .Result;

            return JsonConvert.DeserializeObject<AccessToken>(getcodeRespose);
        }
        public WxUserInfo GetUserInfo(AccessToken accessToken)
        {

            var getcodeRespose = new HttpClient()
                   .GetAsync($"https://api.weixin.qq.com/sns/userinfo?access_token={accessToken.access_token}&openid={accessToken.openid}&lang=zh_CN")
                   .Result
                   .Content
                   .ReadAsStringAsync()
                   .Result;

            return JsonConvert.DeserializeObject<WxUserInfo>(getcodeRespose);

        }
        /// <summary>
        /// 检查小程序发布动态敏感词
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public WxMsgSecCheck PostMsgSecCheck(string content)
        {
            var accessToken = GetHbMeetingWxAccessToken();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.weixin.qq.com/wxa/msg_sec_check?access_token={accessToken.access_token}");
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            string strContent = "{\"content\":\"" + content + "\"}";
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(strContent);
                dataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            return JsonConvert.DeserializeObject<WxMsgSecCheck>(reader.ReadToEnd());
        }
        /// <summary>
        /// 检查小程序发布违规图片
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public WxMsgSecCheck PostImgSecCheck(FileStream fs, string filename, string pathname)
        {
            filename = "1.jpg";
            pathname = "jpg";
            var accessToken = GetHbMeetingWxAccessToken();
            //FileStream fs = new FileStream(@"X:\1.jpg", FileMode.Open, FileAccess.Read, FileShare.Read);
            var html = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));

                HttpContent fileContent = new StreamContent(fs);//为文件流提供的HTTP容器
                fileContent.Headers.Add("Content-Disposition", @"form-data; name=""fieldNameHere""; filename=""1.jpg""");
                fileContent.Headers.Add("ContentType", "image/jpeg");
                using (var multipartFormDataContent = new MultipartFormDataContent())
                {
                    var boundary = multipartFormDataContent.Headers.ContentType.Parameters.First(o => o.Name == "boundary");
                    boundary.Value = boundary.Value.Replace("\"", String.Empty);
                    multipartFormDataContent.Add(fileContent, "fieldNameHere", filename);
                    var requestUri = $"https://api.weixin.qq.com/wxa/img_sec_check?access_token={accessToken.access_token}";
                    html = client.PostAsync(requestUri, multipartFormDataContent).Result.Content.ReadAsStringAsync().Result;
                    // return JsonConvert.DeserializeObject<WxMsgSecCheck>(reader.ReadToEnd());
                }
            }

            return JsonConvert.DeserializeObject<WxMsgSecCheck>(html);
        }
        /// <summary>
        /// 获取小程序的accesstoken
        /// </summary>
        /// <returns></returns>
        public WxAccessToken GetHbMeetingWxAccessToken()
        {
            if (!_wxhelperDic.ContainsKey("HbMeetingAccessToken"))
            {
                var gettokenRespose = new HttpClient()
                                                .GetAsync($"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={_appid}&secret={_secret}")
                                                .Result
                                                .Content
                                                .ReadAsStringAsync()
                                                .Result;
                var result = JsonConvert.DeserializeObject<WxAccessToken>(gettokenRespose);
                var wxtokenDic = new WxHelperDic();
                wxtokenDic.Value = result.access_token;
                wxtokenDic.ExpiresTime = DateTime.Now.AddSeconds(result.expires_in);
                _wxhelperDic.TryAdd("HbMeetingAccessToken", wxtokenDic);
                return result;
            }
            else
            {
                WxHelperDic HbMeetingAccessToken;
                _wxhelperDic.TryGetValue("HbMeetingAccessToken", out HbMeetingAccessToken);
                if (HbMeetingAccessToken != null && HbMeetingAccessToken.ExpiresTime < DateTime.Now.AddSeconds(-10))
                {
                    var gettokenRespose = new HttpClient()
                                             .GetAsync($"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={_appid}&secret={_secret}")
                                             .Result
                                             .Content
                                             .ReadAsStringAsync()
                                             .Result;
                    var result = JsonConvert.DeserializeObject<WxAccessToken>(gettokenRespose);
                    var wxtokenDic = new WxHelperDic();
                    wxtokenDic.Value = result.access_token;
                    wxtokenDic.ExpiresTime = DateTime.Now.AddSeconds(result.expires_in);
                    _wxhelperDic.AddOrUpdate("HbMeetingAccessToken", (key) => wxtokenDic, (key, old) => wxtokenDic);
                    return result;
                }
                else
                {
                    if (CheckAccessToken(HbMeetingAccessToken.Value))
                    {
                        _wxhelperDic.TryGetValue("HbMeetingAccessToken", out HbMeetingAccessToken);
                        var result = new WxAccessToken();
                        result.access_token = HbMeetingAccessToken.Value;
                        return result;
                    }
                    else
                    {
                        _wxhelperDic.TryRemove("HbMeetingAccessToken", out WxHelperDic wxHelperDic);
                        return GetHbMeetingWxAccessToken();
                    }
                }
            }
        }
       
        /// <summary>
        /// 通过图片url和App代码绝对路径  保存图片到App中
        /// </summary>
        /// <param name="url">图片Url</param>
        /// <param name="path">代码绝对路径</param>
        /// <returns></returns>
        public string SaveWxUserPic(string url, string path)
        {
            var imgurl = url;
            string fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssffffff")}.jpg";
            string filePath = $"/UpLoad/jpg/{DateTime.Now.ToString("yyyy-MM-dd")}/";
            try
            {
                String WebAddress = @"" + imgurl + "";
                WebRequest imgRequest = WebRequest.Create(WebAddress);
                WebResponse imgResponse = imgRequest.GetResponse();
                Stream imgResponseStream = imgResponse.GetResponseStream();
                DirectoryInfo di = new DirectoryInfo(path + filePath);
                if (!di.Exists) { di.Create(); }
                using (FileStream fs = System.IO.File.Create(path + filePath + fileName))
                {
                    imgResponseStream.CopyTo(fs);
                    fs.Flush();
                }
            }
            catch (Exception)
            {
                fileName = "20191023144139921578.jpg";
                filePath = "/UpLoad/jpg/2019-10-23/";
            }
            return filePath + fileName;
        }

        /// <summary>
        /// 微信一键登录 通过accesstoken跟openid获取用户信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public GetWxLoginUserInfoByToken GetWxLoginUserInfoByToken(string accesToken, string openid)
        {
            var gettokenRespose = new HttpClient()
                              .GetAsync($"https://api.weixin.qq.com/sns/userinfo?access_token={accesToken}&openid={openid}")
                              .Result
                              .Content
                              .ReadAsStringAsync()
                              .Result;
            return JsonConvert.DeserializeObject<GetWxLoginUserInfoByToken>(gettokenRespose);
        }


        /// <summary>
        /// 获取公司微信小程序码
        /// </summary>
        /// <param name="Companyid"></param>
        /// <param name="Meetingid"></param>
        /// <param name="width"></param>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string GetMiniProgram(int Companyid, int Meetingid, int width, string fileName, string filePath)
        {
            try
            {
                var token = GetHbMeetingWxAccessToken().access_token;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.weixin.qq.com/wxa/getwxacode?access_token={token}");
                request.Method = "POST";
                request.ContentType = "text/html";
                string strContent = "{\"path\":\"pages/expo/com/com?id=" + Companyid + "&meetingid=" + Meetingid + "\",\"width\":\"" + width + "\"}";
                using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
                {
                    dataStream.Write(strContent);
                    dataStream.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var imgResponseStream = response.GetResponseStream();
                if (response.ContentLength < 150)
                {
                    StreamReader reader = new StreamReader(imgResponseStream, Encoding.GetEncoding("UTF-8"));
                    return JsonConvert.DeserializeObject<WxMsgSecCheck>(reader.ReadToEnd()).errMsg;
                }
                DirectoryInfo di = new DirectoryInfo(filePath);
                if (!di.Exists) { di.Create(); }
                using (FileStream fs = System.IO.File.Create(filePath + fileName))
                {
                    imgResponseStream.CopyTo(fs);
                    fs.Flush();
                }
                return "生成成功";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 验证token是否有效
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        private bool CheckAccessToken(string accessToken)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.weixin.qq.com/cgi-bin/menu/get?access_token={accessToken}");
            request.Method = "POST";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            var errMsg = JsonConvert.DeserializeObject<WxMsgSecCheck>(reader.ReadToEnd());
            if (errMsg.errcode == 48001)
                return true;
            else
                return false;
        }
    }
}
