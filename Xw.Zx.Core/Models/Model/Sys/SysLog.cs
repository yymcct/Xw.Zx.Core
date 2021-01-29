using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public partial class SysLog : ModelBase_Id_CreateTime
    {
        public LogType logType { get; set; }

        public string Log { get; set; }

        public string AdminName { get; set; }

        public int AdminId { get; set; }
    }

    public partial class SysLog
    {
        public enum LogType
        {
            修改上级 = 0,
            发放优惠券=1,
            优惠券兑换积分=2
        }
    }
}
