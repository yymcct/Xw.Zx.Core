using System;
using System.Collections.Generic;
using System.Text;
namespace Xw.Zx.Core.Helper
{
    public static class CommonHelper
    {



        /// <summary>
        /// 新闻 公司 产品分享字数
        /// </summary>
        /// <returns></returns>
        public static int GetShareDescLength()
        {
            return 25;
        }



        public static string GetIp()
        {
            //TODO 可能不完善 需多测试 检查是否有错误
            string strHostName = System.Net.Dns.GetHostName();
            //clientIPAddress是一个数组，可能有多个数据
            var clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName);
            return clientIPAddress.GetValue(1).ToString();
        }

    }
}
