using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Xw.Zx.Core.Helper
{
    public static class HtmlContentHelper
    {
        #region 转移图片




        /// <summary>   
        /// 取得HTML中所有图片的 URL。   
        /// </summary>   
        /// <param name="CategoryIteamID">项目Id</param>   
        /// <param name="htmlText">HTML代码</param>   
        /// <returns>图片的URL列表</returns>   
        public static IEnumerable<string> GetHtmlImageUrlList(int CategoryIteamID, string htmlText)
        {
            // 定义正则表达式用来匹配 img 标签   
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串   
            MatchCollection matches = regImg.Matches(htmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表   
            foreach (Match match in matches)
            {
                if (match.ToString().Contains("http") == true)
                {
                    sUrlList[i++] = match.Groups["imgUrl"].Value;
                }
                else
                {
                    sUrlList[i++] =  match.Groups["imgUrl"].Value;
                }

            }
            return sUrlList;
        }


        /// <summary>   
        /// 取得HTML中部分图片的 URL 可选最大值。   
        /// </summary>   
        /// <param name="CategoryIteamID">项目Id</param>   
        /// <param name="htmlText">HTML代码</param> 
        /// <param name="max">最大图片数量</param> 
        /// <returns>图片的URL列表</returns>   
        public static IEnumerable<string> GetHtmlImageUrlList(int CategoryIteamID, string htmlText, int max)
        {
            var res = GetHtmlImageUrlList(CategoryIteamID, htmlText);
            return res.Take(max);
        }



        /// <summary>
        /// 清除HTML中的JS脚本和style脚本
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static string RemoveScriptsAndStyles(string htmlText)
        {
            htmlText = Regex.Replace(htmlText, @"<\s*script[^>]*?>.*?<\s*/\s*script\s*>", "", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"<\s*style[^>]*?>.*?<\s*/\s*style\s*>", "", RegexOptions.IgnoreCase);
            return htmlText;
        }

        #endregion

        #region 去除新闻中的html标签
        /// <summary>
        /// 去除新闻中的html标签，一般用来从内容中取简介
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static string RemoveHtml(string htmlText)
        {
            string strText = Regex.Replace(htmlText, "<[^>]+>", "");
            strText = Regex.Replace(strText, "&[^;]+;", "");
            return strText;
        }
        /// <summary>
        /// 去除新闻中的html标签 并截取字符
        /// </summary>
        /// <param name="htmlText"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RemoveHtml(string htmlText, int length)
        {
            if (RemoveHtml(htmlText).Length > length)
                return RemoveHtml(htmlText).Substring(0, length);
            else
                return RemoveHtml(htmlText);
        }
        #endregion

        #region 替换新闻中不带域名的img标签 、 a标签 以及a标签样式
        /// <summary>
        /// 替换新闻中不带域名的img标签、a标签 以及a标签样式
        /// </summary>
        /// <param name="CategoryIteamID">项目Id</param>   
        /// <param name="htmlText">HTML代码</param>   
        /// <returns></returns>
        public static string ReplaceWebSiteImg(int CategoryIteamID, string htmlText)
        {
            htmlText = ReplacACss(htmlText.ToLower());
            return htmlText.Replace("src=\"/", "src=\"" +  "/");
        }
        #endregion
        #region 替换新闻中a标签的样式
        /// <summary>
        /// 替换新闻中a标签的样式
        /// </summary>
        /// <param name="htmlText">HTML代码</param>   
        /// <returns></returns>
        public static string ReplacACss(string htmlText)
        {
            var result = Regex.Replace(htmlText, "(<a[^>]+?>)", "$1" + " <span style =\"color:#ff0000;\">").Replace("</a>", "</span></a>");
            return result;
        }
        #endregion
    }
}
