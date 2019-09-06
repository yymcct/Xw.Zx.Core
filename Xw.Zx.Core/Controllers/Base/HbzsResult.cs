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

    public class HbzsResult
    {
        public HbzsResult(HbzsResultCode statusCode, string errMsg="")
        {
            StatusCode = statusCode;
            Msg = errMsg;
        }

        /// <summary>
        /// 状态码
        /// </summary>
        public HbzsResultCode StatusCode { get; set; } = HbzsResultCode.Sucess;

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; } = "";

        public string Result { get; set; } = "";

    }

    public class HbzsResult<T>
    {
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

        public HbzsResult(HbzsResultCode statusCode, string errMsg)
        {
            StatusCode = statusCode;
            Msg = errMsg;
        }


        /// <summary>
        /// 状态码
        /// </summary>
        public HbzsResultCode StatusCode { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; } = "";

        public T Result { get; set; }
    }
}
