using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Service.ShareProfit
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ShareProfitTemplateAttribute : Attribute
    {
        public ShareProfitTemplateAttribute(int templateId)
        {
            TemplateId = templateId;
        }

        public int TemplateId { get; set; }
    }
}
