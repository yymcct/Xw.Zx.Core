using System;
using System.Collections.Generic;
using System.Text;
namespace Xw.Zx.Core.Helper
{
    public static class CommonHelper
    {
        public static string GetDomian(int categoryIteam)
        {
            switch (categoryIteam)
            {
                //1酒网 2食品 3农资 4农化 5化妆品 6畜牧 7食材
                case 1: return "http://www.9998.tv";
                case 2: return "http://www.5888.tv";
                case 3: return "http://www.3456.tv";
                case 4: return "http://www.1988.tv";
                case 5: return "http://www.5588.tv";
                case 6: return "http://www.1866.tv";
                case 7: return "http://www.1588.tv";
                default: return "";
            }
        }
        /// <summary>
        /// 所上传的图片加域名
        /// </summary>
        /// <returns></returns>
        public static string GetWebAdress()
        {
            return "http://192.168.0.214:5000";
        }


        /// <summary>
        /// 新闻 公司 产品分享默认图片
        /// </summary>
        /// <returns></returns>
        public static string GetShareImg()
        {
            return "http://192.168.0.214:5000";
        }

        /// <summary>
        /// 新闻 公司 产品分享字数
        /// </summary>
        /// <returns></returns>
        public static int GetShareDescLength()
        {
            return 25;
        }




        /// <summary>
        /// 腾讯云账号的appid
        /// 测试 8月29过期
        /// </summary>
        /// <returns></returns>
        public static string GetAppId()
        {
            return "1253752814";
        }

        /// <summary>
        /// 腾讯云apiId 用来生成上传短视频的密钥
        /// 测试 8月29过期
        /// </summary>
        /// <returns></returns>
        public static string GetApiSecretId()
        {
            return "AKID3Zw56bu9cu3hZEvcbVFj2b0b5KLDbUFq";
        }

        /// <summary>
        /// 腾讯云apiKey 用来生成上传短视频的密钥
        /// 测试 8月29过期
        /// </summary>
        /// <returns></returns>
        public static string GetApiSecretKey()
        {
            return "fhX4ZpqcJNeoocNUP8mJvPilY6kEVXP3";
        }
        public static string GetIp()
        {
            //TODO 可能不完善 需多测试 检查是否有错误
            string strHostName = System.Net.Dns.GetHostName();
            //clientIPAddress是一个数组，可能有多个数据
            var clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName);
            return clientIPAddress.GetValue(1).ToString();
        }

        public static string GetMemberPhoto()
        {
            return "/UpLoad/jpg/2019-07-26/20190726081432045665.jpg";
        }
    }
}
