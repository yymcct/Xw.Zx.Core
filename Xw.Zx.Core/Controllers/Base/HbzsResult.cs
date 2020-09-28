using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Controllers
{
    public enum HbzsResultCode
    {
        Sucess = 200,
        /// <summary>
        /// 远程服务错误
        /// </summary>
        Remote_Service_Error = 500,

        /// <summary>
        /// 参数校验错误
        /// </summary>
        Invalid_Error = 10001,
    }

    public class HbzsResult<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public HbzsResultCode StatusCode { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; } = "";

        public T Result { get; set; }

        public int Total { get; set; }

        public HbzsResult()
        {
            StatusCode = HbzsResultCode.Sucess;
            Msg = "";
        }

        public HbzsResult(T result)
        {
            StatusCode = HbzsResultCode.Sucess;
            Result = result;
        }

        public HbzsResult(T result, int total)
        {
            StatusCode = HbzsResultCode.Sucess;
            Result = result;
            Total = total;
        }

        public HbzsResult(HbzsResultCode statusCode)
        {
            StatusCode = statusCode;
        }

        public HbzsResult(HbzsResultCode statusCode, string errMsg)
        {
            StatusCode = statusCode;
            Msg = errMsg;
        }
    }

    public class HbzsResult : HbzsResult<string>
    {
       

        public HbzsResult(string result) : base(result)
        { }

        public HbzsResult(HbzsResultCode statusCode) : base(statusCode)
        { }

        public HbzsResult(HbzsResultCode statusCode, string errMsg) : base(statusCode, errMsg)
        {

        }
    }
}
