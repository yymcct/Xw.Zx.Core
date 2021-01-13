using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Xw.Zx.Core.Service.ShareProfit;

namespace Xw.Zx.Core.Service
{
    public static class ShareProfitServiceCollectionExtensions
    {
        private static Dictionary<int, Type> _ServiceContainers = new Dictionary<int, Type>();

        public static IServiceCollection AddProfit(this IServiceCollection services)
        {
            InitServiceContainers();

            foreach (var item in _ServiceContainers)
            {
                services.AddScoped(item.Value);
            }

            services.AddScoped(servicesProvider =>
            {
                Func<int, IShareprofit> func = templaeId =>
                {
                    var shareProfitService = _ServiceContainers[templaeId];

                    return servicesProvider.GetService(shareProfitService) as IShareprofit;
                };

                return func;
            });
            return services;
        }

        private static void InitServiceContainers()
        {
            if (_ServiceContainers.Count() == 0)
            {
                var alltypes = AssemblyLoadContext
                    .Default
                    .LoadFromAssemblyPath(AppContext.BaseDirectory + $"Xw.Zx.Core.dll")
                    .GetTypes();

                var IShareprofitTypes = alltypes
                    .Where(t => t.IsClass && t.GetInterfaces().Any(tt => tt == typeof(IShareprofit)))
                    .ToArray();

                foreach (var shareProfitType in IShareprofitTypes)
                {
                    var attrObj = shareProfitType
                         .GetCustomAttributes(typeof(ShareProfitTemplateAttribute), false)
                         .FirstOrDefault();

                    if (attrObj is ShareProfitTemplateAttribute attr)
                    {
                        _ServiceContainers.Add(attr.TemplateId, shareProfitType);
                    }
                }
            }
        }
    }
}
