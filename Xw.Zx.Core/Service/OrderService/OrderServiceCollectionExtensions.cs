using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Service
{
    public static class OrderServiceCollectionExtensions
    {
        public static IServiceCollection AddOrderService(this IServiceCollection services)
        {
            services.AddScoped<ShareProfitHandle>();
            services.AddScoped<UpdateMemberTypeHandle>();
            services.AddScoped<LogReceive>();
            services.AddScoped<PresentedCoupons>();
            return services;
        }
    }
}
