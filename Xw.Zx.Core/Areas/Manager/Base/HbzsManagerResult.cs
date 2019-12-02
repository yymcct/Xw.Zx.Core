using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Areas.Manager
{
    public enum HbzsManagerResultCode
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

    public class HbzsManagerResult
    {
        public HbzsManagerResult(HbzsManagerResultCode statusCode, string errMsg)
        {
            StatusCode = statusCode;
            Msg = errMsg;
        }

        /// <summary>
        /// 状态码
        /// </summary>
        public HbzsManagerResultCode StatusCode { get; set; } = HbzsManagerResultCode.Sucess;

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; } = "";

        public string Result { get; set; } = "";

    }

    public class HbzsManagerResult<T>
    {
        public HbzsManagerResult()
        {
            StatusCode = HbzsManagerResultCode.Sucess;
            Msg = "";
        }

        public HbzsManagerResult(T result)
        {
            StatusCode = HbzsManagerResultCode.Sucess;
            Result = result;
        }

        public HbzsManagerResult(T result, int total)
        {
            StatusCode = HbzsManagerResultCode.Sucess;
            Result = result;
            Total = total;
        }

        public HbzsManagerResult(HbzsManagerResultCode statusCode, string errMsg)
        {
            StatusCode = statusCode;
            Msg = errMsg;
        }


        /// <summary>
        /// 状态码
        /// </summary>
        public HbzsManagerResultCode StatusCode { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; } = "";

        public T Result { get; set; }

        public int Total { get; set; }
    }
}
