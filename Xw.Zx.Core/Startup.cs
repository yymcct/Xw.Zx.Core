using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Alipay.AopSdk.AspnetCore;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using Swashbuckle.AspNetCore.Swagger;
using Xw.Zx.Core.Config;
using Xw.Zx.Core.Config.Swagger;
using Xw.Zx.Core.HangfireJob;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service;
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

            #region Swagger           

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "债减减", Version = "v1" });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "Xw.Zx.Core.xml");
                c.IncludeXmlComments(xmlPath);
                c.DocumentFilter<HiddenApiFilter>();
            });
            #endregion

            services.Configure<BiqilinOption>(Configuration.GetSection("Biqilin"));
            PreWechatHelper.WxPayConfig.Init(Configuration);

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => { options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; });

            #region 权限配置
            //App
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admins",
                     policy => policy.RequireRole(nameof(MemberRole.Admin)
                            , nameof(MemberRole.Admin_Tongjibu)
                            , nameof(MemberRole.Admin_Caiwu)
                            , nameof(MemberRole.Admin_CaiwuPayChange)
                            , nameof(MemberRole.Admin_CaiwuManager)));
            });
            #endregion

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

            #region Sieve
            //Sieve
            services.Configure<SieveOptions>(Configuration.GetSection("Sieve"));
            services.AddScoped<ISieveCustomSortMethods, SieveCustomSortMethods>();
            services.AddScoped<ISieveCustomFilterMethods, SieveCustomFilterMethods>();
            services.AddScoped<ISieveProcessor, ApplicationSieveProcessor>();
            #endregion

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
            //services.AddScoped<IMailService, MailService>();
            //services.AddScoped<IQQMailService, QQMailService>();
            //services.AddScoped<ISyncService, SyncService>();
            AddAssembly(services, "Xw.Zx.Core");
            #endregion

            #region AutoMapper
            //AutoMapper
            services.AddAutoMapper(options =>
            {
                options.ForAllMaps((a, b) => b.ForAllMembers(opt => opt.Condition((src, dest, sourceMember) => sourceMember != null)));
            }, AssemblyLoadContext.Default.LoadFromAssemblyPath($"{AppContext.BaseDirectory}Xw.Zx.Core.dll"));
            #endregion

            #region Alipay
            services.AddAlipay(Configuration.GetSection("Alipay"));
            #endregion



            new AppsettingsUtility().Initial(Configuration);

            services.AddHttpClient();

            services.AddProfit();
            services.AddOrderService();

            #region Hangfire
            services.AddScoped<OrderPayCheck>();
            services.AddScoped<XtsuoOrderSync>();
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetSection("DbConnections:XwZx").Value));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            PreWechatHelper.WxPayConfig.ContenxtPaht = env.ContentRootPath;
            app.UseIdentityServer();

            app.UseAuthentication();

            app.UseCors("AllowAllHeaders");

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseHttpsRedirection();

            app.UseDefaultFiles();

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                     Path.Combine(Directory.GetCurrentDirectory(), "UpLoad")),
                RequestPath = "/UpLoad",
                ServeUnknownFileTypes = true
            });
            var provider = new FileExtensionContentTypeProvider();
            //新增一些新的映射
            provider.Mappings[".apk"] = "application/vnd.android.package-archive";

            app.UseMvc();

            app.UseSwagger();


            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Xw.Zx.Core V1");
            });

            app.Use(async (context, next) =>
            {
                var path = context.Request.Path.ToString();
                if (System.Text.RegularExpressions.Regex.IsMatch(path, @"^/sqb/"))
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "sqb/index.html"));
                }
                else
                {
                    await next.Invoke();
                }

            });

            app.UseHangfireServer();//启动Hangfire服务
            app.UseHangfireDashboard();//启动hangfire面板

            RecurringJob.AddOrUpdate<OrderPayCheck>(i => i.Run(), "0 */30 * * * ?");
            RecurringJob.AddOrUpdate<XtsuoOrderSync>(i => i.Run(), "0 */25 * * * ?");
        }


        public void AddAssembly(IServiceCollection service, string assemblyName
            , ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(AppContext.BaseDirectory + $"{assemblyName}.dll");

            var types = assembly.GetTypes();
            var list = types.Where(u => u.IsClass && !u.IsAbstract && !u.IsGenericType && u.Namespace == "Xw.Zx.Core.Service").ToList();
            list = list.Where(u => u.Name.Contains("Service")).ToList();
            foreach (var type in list)
            {
                var interfaceList = type.GetInterfaces();
                if (interfaceList.Any())
                {
                    var inter = interfaceList.First();

                    switch (serviceLifetime)
                    {
                        case ServiceLifetime.Transient:
                            service.AddTransient(inter, type);
                            break;
                        case ServiceLifetime.Scoped:
                            service.AddScoped(inter, type);
                            break;
                        case ServiceLifetime.Singleton:
                            service.AddSingleton(inter, type);
                            break;

                    }
                    service.AddScoped(inter, type);
                }
            }
        }
    }
}
