using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace swiftpass.utils
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Utils
    {
        public Utils() { }

        /// <summary>
        /// 对字符串进行URL编码
        /// </summary>
        /// <param name="instr">URL字符串</param>
        /// <param name="charset">编码</param>
        /// <returns></returns>
        public static string UrlEncode(string instr, string charset)
        {
            //return instr;
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));

                }
                catch (Exception ex)
                {
                    res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
                    Console.WriteLine(ex);
                }


                return res;
            }
        }


        /// <summary>
        /// 对字符串进行URL解码
        /// </summary>
        /// <param name="instr">编码的URL字符串</param>
        /// <param name="charset">编码</param>
        /// <returns></returns>
        public static string UrlDecode(string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));

                }
                catch (Exception ex)
                {
                    res = HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
                    Console.WriteLine(ex);
                }


                return res;

            }
        }


        /// <summary>
        /// 取时间戳生成随即数,替换交易单号中的后10位流水号
        /// </summary>
        /// <returns></returns>
        public static UInt32 UnixStamp()
        {
            TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return Convert.ToUInt32(ts.TotalSeconds);
        }


        /// <summary>
        /// 取随机数
        /// </summary>
        /// <param name="length">随机数的长度</param>
        /// <returns></returns>
        public static string BuildRandomStr(int length)
        {
            Random rand = new Random();

            int num = rand.Next();

            string str = num.ToString();

            if (str.Length > length)
            {
                str = str.Substring(0, length);
            }
            else if (str.Length < length)
            {
                int n = length - str.Length;
                while (n > 0)
                {
                    str.Insert(0, "0");
                    n--;
                }
            }

            return str;
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <returns></returns>
        public static Dictionary<String, String> loadCfg()
        {
            //string cfgPath = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ApplicationBase) 
            //                    + Path.DirectorySeparatorChar + "config" + Path.DirectorySeparatorChar + "config.properties";
            //Dictionary<String, String> cfg = new Dictionary<string, string>();
            //using(StreamReader sr = new StreamReader(cfgPath)){
            //    while(sr.Peek() >= 0){
            //        string line = sr.ReadLine();
            //        if (line.StartsWith("#")) {
            //            continue;
            //        }
            //        int startInd = line.IndexOf("=");
            //        string key = line.Substring(0,startInd);
            //        string val = line.Substring(startInd+1, line.Length-(startInd + 1));
            //        if (!cfg.ContainsKey(key) && !string.IsNullOrEmpty(val)) {
            //            cfg.Add(key, val);
            //        }
            //    }    
            //}
            Dictionary<String, String> cfg = new Dictionary<string, string>();
            //cfg.Add("mch_id", "102576520476");
            cfg.Add("groupno", "102566518392");
            cfg.Add("req_url", "https://payapi.citicbank.com/pay/gateway");
            cfg.Add("mchPrivateKey", "MIIEvwIBADANBgkqhkiG9w0BAQEFAASCBKkwggSlAgEAAoIBAQDM+dBSTbMqexz0kVBgsr/N8AmminW2O81c5mLLG1XnxeTyg2jbb9MqD7gdwMqOMVG4l2i3K7f9hOx0RuU8zn8hF6I5G95e2+7s7yvK/Lbx9xS3OvXZHmXxEaBzL3yvJAad4/4DcQs33q/hwIc94G3RjCP+iXkH7axv2b7yhzAfgDLy/MiJhnCeGvyssGcAA3fWaKGucwI0WskKPgOe8iW/+AsxORG+5eHGwqsfJ5Di369MqjnBH/0l7focpU/0t6mqwSSCLTnBwyn3eU4i4S7QZMZahFneJMUxYoJqyC00l7jx66jyGkIMtXO160dQ48GbO033uw2qCWcQSL+IoL+vAgMBAAECggEBAJq7xsAquaLK90vEWrn0lghfONnI0h10PY3wuQl8JDhn0nIkM0bbvruUzDdhr0byAi/n1HvZ+/XcnycT0b86XwvbLHISEKbpPg8MabG9XlmAXNoJH0f8w5Xc6ZOqeGQ6POh6EOoGLyLI71rrHOzf++bg9NcUbhJHIBH7fULWPMS5hdDcWg2zDFsWKZji7bmTuWLj/UVFRUM3A/evIGxoff/u95j9Tg2oCYA2cZlcFo2x8RZmTHQUuacNcFMbiomonOm2BPmSCLbxn8H1eFT99ibkBAgwYcpBLrXEt65yHaLV+j3SOqPFakpoJf5MZTKGGaqcNEZFJBwnbzFqhAufRfkCgYEA+/Vi/qTVW9jjhUGjBV5PIx8I8yAOwJ4GRaLXKDmITYTK1sZlS+IKJe7XpoAXKgP6T07iOPRThTAhHZmYMgi34u+h7Ro81SdF8tPjdCRk7urzzmf/3kTszKWhzV45t8YhtHw63Jmc7Qt9mYp4Q15Npi9x+0hRZLwiRUQ9R+QHVg0CgYEA0EOAsp+HPqRdspa6U6xd5+xxH9ijZ01LlMLInnk41vNqjnhT/23/n6FrigBU3drssA4iz9d6YDsaNVxx11RrqZi7d+yejmCtavLbBKKULlxKC8pm/5ZeG4ALtbiMgz4aDMgL0OTiiQ5ugkHVE1efdN6gKlDwYyzL7CiWd/ffGasCgYAr4Kb1u5TgYtv0d+LA65SShDQVyu5mwt5KwDoB6kr/XPHKB1DarymfQ4HQoBvomDq+Fvm0e3AUELajpfzoMbZ8Ucj8NoaDhExdJmNE9ZWSOd5tgpT/izdFyE5uKkuY4ms6OCw0AJhge5DK7V9WNBUHrNmHridmiJDbSS/21hIk/QKBgQCrQudhfEhWXgnjI4oD5tmuL8RzIM5205xUr70UfLEeKju0/wRsJ4OvPKnXaSBg9Hfw6x77dcHfwppxyRygq36Bj+SR4NYeVSgmq6Ep9ZwPZ7tIaOA8S+YFjU2T2ThLcNUxL4xv8tjLRj/gA8z5w/OPwsdJJRgZJixnfpUGAKLJUwKBgQCNL1jjviVYY+lwOnrAfBEfblCyQB8rXGCdCqYOep7mZeTwhmLrFvflcu883HNAhMj8VERvj0AfkhNpjdG1+kB/Whhu8OLcVZBSdfrKnipuisGRgHgexpt59cnoOBRsEPBAMUd2pL3VVikelmQlZAgj7caFUR+186sBH1jLPnBvsA==");
            cfg.Add("platPublicKey", "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqNxzebovJ6R+LF0jFyJD4vgdvj+Apmb5h+pW3T0EtDzWZAr7tyiSAtNedYvRjJCqN5cYw0rIwGMZFbD3lQHbJGC+IvpqXwPB8AWqRAwItI82fo2+AyHkq11yE27IgOjSrKofgg3GWJ6SSQonYuXZ0c09chXXiZPKYe0zRbvq83kAVsYDu1sMwi8mfiVff6CIALsehs1MOjmdLW40N1CicVmJaWuh2yee+sj1/0xMOlV1LyJq63hShBD7T93qpGbHoNkpdz+BFc2byrhv1idbB4DRbUiKynzj3FX2Nz8Dv9TFQv8p2Z8dIOst890atv3P8DO7a9FI8I1reLvFDdyPawIDAQAB");
            cfg.Add("notify_url", "/");
            return cfg;
        }

        ///// <summary>
        ///// 保存接口返回结果到文件中
        ///// </summary>
        ///// <param name="_param">接口结果</param>
        //public static void writeFile(string title,Hashtable _param) 
        //{
        //    string resFilePath = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ApplicationBase)
        //                        + Path.DirectorySeparatorChar + "result.txt";
        //    if (!File.Exists(resFilePath))
        //    {
        //        using (StreamWriter sw = new StreamWriter(resFilePath))
        //        {
        //            sw.WriteLine("=====================" + title + "=====================");
        //            foreach (DictionaryEntry de in _param)
        //            {
        //                sw.WriteLine("key:" + de.Key.ToString() + " value:" + de.Value.ToString());
        //            }
        //        }
        //    }
        //    else 
        //    {
        //        using(StreamWriter sw = File.AppendText(resFilePath))
        //        {
        //            sw.WriteLine("=====================" + title + "=====================");
        //            foreach (DictionaryEntry de in _param)
        //            {
        //                sw.WriteLine("key:" + de.Key.ToString() + " value:" + de.Value.ToString());
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 生成32位随机数
        /// </summary>
        /// <returns></returns>
        public static string random()
        {
            char[] constant = {'0','1','2','3','4','5','6','7','8','9',
                               'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                               'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
            StringBuilder sb = new StringBuilder(32);
            Random rd = new Random();
            for (int i = 0; i < 32; i++)
            {
                sb.Append(constant[rd.Next(62)]);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 生成16位订单号 by  hyf 2016年2月16日17:48:43
        /// </summary>
        /// <returns></returns>
        public static string Nmrandom()
        {
            string rm = "";
            Random ra = new Random();
            for (int i = 0; i < 16; i++)
            {
                rm += ra.Next(0, 9).ToString();
            }
            return rm;
        }
        /// <summary>
        /// 将Hashtable参数传为XML
        /// </summary>
        /// <param name="_params"></param>
        /// <returns></returns>
        public static string toXml(Hashtable _params)
        {
            StringBuilder sb = new StringBuilder("<xml>");
            foreach (DictionaryEntry de in _params)
            {
                string key = de.Key.ToString();
                sb.Append("<").Append(key).Append("><![CDATA[").Append(de.Value.ToString()).Append("]]></").Append(key).Append(">");
            }

            return sb.Append("</xml>").ToString();
        }

    }
}
