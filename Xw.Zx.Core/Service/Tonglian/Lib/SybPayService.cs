using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Service.Tonglian.Lib
{

    //public class SybPayService
    //{
  
    //    public Dictionary<string, string> pay(long trxamt, string reqsn, string paytype, string body, string remark, string acct, string validtime, string notify_url, string limit_pay,
    //            string idno, string truename, string asinfo, string sub_appid, string goods_tag, string benefitdetail, string chnlstoreid, string subbranch, string extendparams, string cusip, string fqnum)
    //    {
    //        //HttpConnectionUtil http = new HttpConnectionUtil(SybConstants.SYB_APIURL + "/pay");
    //        //http.init();
    //        Dictionary<string, string> dic = new Dictionary<string, string>();
    //        if (!string.IsNullOrWhiteSpace(SybConstants.SYB_ORGID))
    //            dic.Add("orgid", SybConstants.SYB_ORGID);


    //        dic.Add("cusid", SybConstants.SYB_CUSID);
    //        dic.Add("appid", SybConstants.SYB_APPID);
    //        dic.Add("version", "11");
    //        dic.Add("trxamt", trxamt.ToString());
    //        dic.Add("reqsn", reqsn);
    //        dic.Add("paytype", paytype);
    //        dic.Add("randomstr", getValidatecode(8));
    //        dic.Add("body", body);
    //        dic.Add("remark", remark);
    //        dic.Add("validtime", validtime);
    //        dic.Add("acct", acct);
    //        dic.Add("notify_url", notify_url);
    //        dic.Add("limit_pay", limit_pay);
    //        dic.Add("sub_appid", sub_appid);
    //        dic.Add("goods_tag", goods_tag);
    //        dic.Add("benefitdetail", benefitdetail);
    //        dic.Add("chnlstoreid", chnlstoreid);
    //        dic.Add("subbranch", subbranch);
    //        dic.Add("extendparams", extendparams);
    //        dic.Add("cusip", cusip);
    //        dic.Add("fqnum", fqnum);
    //        dic.Add("idno", idno);
    //        dic.Add("truename", truename);
    //        dic.Add("asinfo", asinfo);
    //        dic.Add("signtype", SybConstants.SIGN_TYPE);
    //        string appkey = "";
    //        if (SybConstants.SIGN_TYPE.Equals("RSA"))
    //            appkey = SybConstants.SYB_RSACUSPRIKEY;
    //        else if (SybConstants.SIGN_TYPE.Equals("SM2"))
    //            appkey = SybConstants.SYB_SM2PPRIVATEKEY;
    //        else
    //            appkey = SybConstants.SYB_MD5_APPKEY;
    //        dic.Add("sign", SybUtil.unionSign(params, appkey, SybConstants.SIGN_TYPE));
    //        byte[] bys = http.postParams(params, true);
    //        string result = new string(bys, "UTF-8");
    //        Dictionary<string, string> map = handleResult(result);
    //        return map;

    //    }

    //    public static string getValidatecode(int n)
    //    {
    //        Random random = new Random();
    //        string sRand = "";
    //        n = n == 0 ? 4 : n;// default 4
    //        for (int i = 0; i < n; i++)
    //        {
    //            string rand = random.Next(10).ToString();
    //            sRand += rand;
    //        }
    //        return sRand;
    //    }
    //}
}