using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.PreWechatHelper;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Xw.Zx.Core.Service
{
    public class XtsuoService : IXtsuoService
    {
        private readonly XwZxContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<XtsuoService> _logger;

        public XtsuoService(XwZxContext context, IHttpClientFactory httpClientFactory, ILogger<XtsuoService> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public bool SyncXtsuoOrders(XtsuoOrdersRequestDto getThirdQueryOrdersRequestPsDto)
        {
            int page = 1;
            int pgcount = 0;
            try
            {
                if (getThirdQueryOrdersRequestPsDto == null)
                    throw new Exception("请传入查询开始和截止时间参数");
                if (getThirdQueryOrdersRequestPsDto.StartTime == null || getThirdQueryOrdersRequestPsDto.EndTime == null)
                    throw new Exception("开始和截止时间不能为空");
                GetThirdQueryOrdersResultDto result = GetThirdOrders((DateTime)getThirdQueryOrdersRequestPsDto.StartTime, (DateTime)getThirdQueryOrdersRequestPsDto.EndTime, page);
                if (result == null)
                    throw new Exception("未获取到结果");
                if (result.page <= 0)
                    throw new Exception("未获取到结果");
                pgcount = result.page;
                //保存记录
                SaveWechatOrders(result.content);
                while (page <= pgcount)
                {
                    page = page + 1;
                    //查询 并保存记录
                    GetThirdQueryOrdersResultDto rlt = GetThirdOrders((DateTime)getThirdQueryOrdersRequestPsDto.StartTime, (DateTime)getThirdQueryOrdersRequestPsDto.EndTime, page);
                    if (rlt == null)
                        break;
                    if (rlt.content == null)
                        break;
                    SaveWechatOrders(rlt.content);
                }

                // return new HbzsManagerResult<>(HbzsManagerResultCode.Sucess, "查询处理成功！");
                return true;
            }
            catch (Exception ex)
            {
                //return new HbzsManagerResult<string>(HbzsManagerResultCode.Remote_Service_Error, ex.Message);
                _logger.LogError(ex.Message);
                return false;
            }
        }

        private void SaveWechatOrders(List<GetThirdQueryOrdersDto> ps)
        {
            try
            {
                if (ps == null)
                    return;
                foreach (GetThirdQueryOrdersDto r in ps)
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(r.transid) || string.IsNullOrWhiteSpace(r.ordersn) || string.IsNullOrWhiteSpace(r.paytime))
                            continue;
                        WechatOrders wechatOrders = _context.WechatOrders.FirstOrDefault(c => c.Out_Order_No == r.ordersn && c.TransactionID == r.transid);
                        if (wechatOrders != null)
                            continue;
                        WechatOrders neworder = new WechatOrders();
                        neworder.Out_Order_No = r.ordersn;
                        neworder.TransactionID = r.transid;
                        neworder.SubState = WechatOrders.WechatOrderState.待申请;
                        neworder.TranTime = ConvertStringToDateTime(r.paytime);
                        neworder.Amount = r.price;
                        decimal je = Math.Floor(neworder.Amount * (1 - WxPayConfig.Config.Rate) * 100) / 100;
                        neworder.SubCharge = Math.Floor(je * WxPayConfig.Config.MaxRate * 100) / 100;
                        neworder.PayState = "SUCCESS";
                        neworder.PayDescription = "支付成功";
                        _context.WechatOrders.Add(neworder);
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private GetThirdQueryOrdersResultDto GetThirdOrders(DateTime startTime, DateTime endTime, int curpage)
        {
            string key = "flu6L9KdZEA9HtdClxp9BcGNjE2QMNdZPXZtLmzho6k";
            string timestr = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString();
            string sign = CreateMD5(timestr + key);
            string kssj = Convert.ToInt64((startTime - new DateTime(1970, 1, 1, 8, 0, 0, 0)).TotalSeconds).ToString();
            string jzsj = Convert.ToInt64((endTime - new DateTime(1970, 1, 1, 8, 0, 0, 0)).TotalSeconds).ToString();
            int mpage = curpage;
            int pgsize = 50;
            string url = $"http://zjj.xtsuo.cn/api/evshopapi.php?i=1&time={timestr}&pwd={sign}&startime={kssj}&endtime={jzsj}&page={mpage}&psize={pgsize}";
            HttpClient httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage res = httpClient.GetAsync(url).Result;
            if (res.IsSuccessStatusCode)
            {
                string result = Regex.Unescape(res.Content.ReadAsStringAsync().Result);

                _logger.LogWarning("同步微信订单:" + url);
                _logger.LogWarning("xtsuo应答:" + result);
                return JsonConvert.DeserializeObject<GetThirdQueryOrdersResultDto>(result);
            }
            else
            {
                return null;
            }
        }

        private DateTime ConvertStringToDateTime(string timeStamp)
        {
            long tsp = 0;
            long.TryParse(timeStamp, out tsp);
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            startTime = startTime.AddSeconds(tsp);
            return startTime;
        }
        private string CreateMD5(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.Default.GetBytes(input));
                var strResult = BitConverter.ToString(result).ToLower();
                return strResult.Replace("-", "");
            }
        }

    }
}
