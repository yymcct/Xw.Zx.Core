using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public enum AppPlatform
    {
        Android = 0,
        Ios = 1
    }
    public class AppVersion
    {
        public int Id { get; set; }

        public AppPlatform AppPlatform { get; set; } = AppPlatform.Android;

        public int Version { get; set; }

        public string DownLoadUrl { get; set; }

        public string WithPhones { get; set; } = "";

        public bool IsAllUpdate { get; set; } = false;

        public DateTime AddTime { get; set; } = DateTime.Now;
    }
}
