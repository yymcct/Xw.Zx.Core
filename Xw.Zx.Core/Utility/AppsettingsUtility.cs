using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Utility
{
    public class AppsettingsUtility
    {
        public static List<string> CanCreateUpdateVipCodePhone;

        public void Initial(IConfiguration configuration)
        {
            CanCreateUpdateVipCodePhone = new List<string>();

            string phones = configuration.GetSection("CanCreateUpdateVipCodePhone").Value;

            foreach (var phone in phones.Split(";"))
            {
                if (!string.IsNullOrWhiteSpace(phone))
                    CanCreateUpdateVipCodePhone.Add(phone);
            }

        }

    }
}
