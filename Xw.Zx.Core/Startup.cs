using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xw.Zx.Core.Config;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Utility;

namespace Xw.Zx.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => { options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; });
            
            
            #region 跨域
            services.AddCors(options =>
            {

                options.AddPolicy("AllowAllHeaders", configurePolicy =>
                {
                    configurePolicy.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });
            #endregion
            #region 数据库     
            services.AddDbContext<XwZxContext>(options =>
            {
                options.UseSqlServer(Configuration.GetSection("DbConnections:XwZx").Value);
                options.EnableSensitiveDataLogging();
            });
            #endregion

            //#region Sieve
            ////Sieve
            //services.Configure<SieveOptions>(Configuration.GetSection("Sieve"));
            //services.AddScoped<ISieveCustomSortMethods, SieveCustomSortMethods>();
            //services.AddScoped<ISieveCustomFilterMethods, SieveCustomFilterMethods>();
            //services.AddScoped<ISieveProcessor, ApplicationSieveProcessor>();
            //#endregion

            #region IdentityServer4
            //配置IdentityServer4
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential(true, "Hbzs.rsa")
                    .AddInMemoryIdentityResources(IdentityServer4Config.GetIdentityResources())
                    .AddInMemoryApiResources(IdentityServer4Config.GetApis())
                    .AddInMemoryClients(IdentityServer4Config.GetClients())
                    .AddResourceOwnerValidator<IdentityServer4UserValidator>();



            //配置验证服务器
            services.AddAuthorization()
                  .AddAuthentication("Bearer")
                  .AddJwtBearer("Bearer", options =>
                  {
                      options.Authority = Configuration.GetSection("AppHost").Value;
                      options.RequireHttpsMetadata = false;

                      options.Audience = "AppApi";
                  });
            #endregion

            #region 注册服务
            services.AddScoped<IMailService, MailService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.UseAuthentication();

            app.UseCors("AllowAllHeaders");

            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
