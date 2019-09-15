using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using Swashbuckle.AspNetCore.Swagger;
using Xw.Zx.Core.Config;
using Xw.Zx.Core.Models.Model;
using Xw.Zx.Core.Service;
using Xw.Zx.Core.Utility;


namespace Xw.Zx.Core
{
    public class Startup
    {   /// <summary>
        /// Api版本信息
        /// </summary>
        private IApiVersionDescriptionProvider Provider;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region Swagger           

            services.AddApiVersioning(option =>
            {
                // 可选，为true时API返回支持的版本信息
                option.ReportApiVersions = true;
                // 不提供版本时，默认为1.0
                option.AssumeDefaultVersionWhenUnspecified = true;
                // 请求中未指定版本时默认为1.0
                option.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddVersionedApiExplorer(option =>
            {
                // 版本名的格式：v+版本号
                option.GroupNameFormat = "'v'VVVV";
                option.AssumeDefaultVersionWhenUnspecified = true;
            });

            Provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

            //Swagger
            services.AddSwaggerGen(options =>
            {
                foreach (var item in Provider.ApiVersionDescriptions)
                {
                    // 添加文档信息
                    options.SwaggerDoc(item.GroupName, new Info
                    {
                        Title = "追息宝 Api",
                        Version = item.ApiVersion.ToString(),
                    });
                }
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "Xw.Zx.Core.xml");
                options.IncludeXmlComments(xmlPath);
                options.OperationFilter<SwaggerUploadFilter>();

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Authorization: Bearer {token}",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                options.AddSecurityRequirement(security);
            });
            #endregion

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => { options.SerializerSettings.DateFormatString = "yyyy-MM-dd"; });
            
          

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
            services.AddScoped<IQQMailService, QQMailService>();
            services.AddScoped<ISyncService, SyncService>();
            #endregion

            #region AutoMapper
            //AutoMapper
            services.AddAutoMapper(options =>
            {
                options.ForAllMaps((a, b) => b.ForAllMembers(opt => opt.Condition((src, dest, sourceMember) => sourceMember != null)));
            }, AssemblyLoadContext.Default.LoadFromAssemblyPath($"{AppContext.BaseDirectory}Xw.Zx.Core.dll"));
            #endregion

            services.AddHttpClient();
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

            app.UseDefaultFiles();

            app.UseStaticFiles();
            var provider = new FileExtensionContentTypeProvider();
            //新增一些新的映射
            provider.Mappings[".apk"] = "application/vnd.android.package-archive";

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                foreach (var item in Provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{item.GroupName}/swagger.json", "Hbzs.Api " + item.ApiVersion);
                }                
            });
        }
    }
}
