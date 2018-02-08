using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularApp.Extensions;
using AngularApp.Helpers;
using AngularApp.Hubs;
using AngularApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Wjw1.Infrastructure;
using Wjw1.Infrastructure.Models;
using Wjw1.Module.Localization;

namespace AngularApp
{
    public class Startup
    {
        private static readonly IList<ModuleInfo> Modules = new List<ModuleInfo>();

        private readonly IHostingEnvironment _env;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);
            
            _env = env;
            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            GlobalConfiguration.WebRootPath = _env.WebRootPath;
            GlobalConfiguration.ContentRootPath = _env.ContentRootPath;
            services.LoadInstalledModules(_env.ContentRootPath);//Modules, _env);

            // Add framework services.
            services.AddCustomizedDataStore(Configuration);
            services.AddIdentity<SysUser, SysRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // SQL Server 缓存数据
            services.AddDistributedSqlServerCache(
                a =>
                {
                    a.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
                    a.SchemaName = "dbo";
                    a.TableName = "SysCaches";
                });

            // 添加分布式内存缓存
            // services.AddDistributedMemoryCache();

            // Response Caching Middleware
            services.AddResponseCaching();

            // 支持 Tempdata["xx"]
            //services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

            // Add application services.
            //瞬时（Transient）生命周期服务在它们每次请求时被创建。这一生命周期适合轻量级的，无状态的服务。
            //作用域（Scoped）生命周期服务在每次请求被创建一次。
            //单例（Singleton）生命周期服务在它们第一次被请求时创建（或者如果你在 ConfigureServices运行时指定一个实例）并且每个后续请求将使用相同的实例。如果你的应用程序需要单例行为，建议让服务容器管理服务的生命周期而不是在自己的类中实现单例模式和管理对象的生命周期。

            //services.AddTransient(typeof(ApplicationDbContext));
            //多语言包支持
            services.AddSingleton<IStringLocalizer, EfStringLocalizer>();
            services.AddSingleton<IStringLocalizerFactory, EfStringLocalizerFactory>();
            services.AddTransient<IUserInfo, UserInfo>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            // add user services


            services.AddMvc(a =>
            {
                // 记录action 日志
                a.Filters.Add(typeof(LogFilter));
            })
            .AddViewLocalization()
            .AddDataAnnotationsLocalization();
            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<SysUser>()
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddProfileService<ProfileService>()
                ;


            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:53278";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "api1";
                    options.BackChannelTimeouts = TimeSpan.FromDays(14);
                });

            // 启用gzip压缩
            services.AddResponseCompression();

            services.AddOptions();
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new Info
                {
                    Title = "Web API for ProjectName",
                    Description = "Description for this API",
                    Version = "v1"
                });
                o.SwaggerDoc("v2", new Info
                {
                    Title = "Web API for ProjectName",
                    Description = "Description for this API",
                    Version = "v2"
                });
                //ApiKeyScheme/BasicAuthScheme/OAuth2Scheme/SecurityScheme
                o.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    //Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey" //apiKey
                });
                //o.OperationFilter<HttpHeaderFilter>();
            });
            services.AddSignalR();
            services.AddSingleton<IHostedService, Counter>();
            services.AddSingleton<IHostedService, Weather>();
            return services.Build(Configuration, _env);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole((c, l) => l >= LogLevel.Trace);

            //loggerFactory.AddConsole((c, l) => l >= LogLevel.Information);
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            //loggerFactory.AddDebug((c, l) => l >= LogLevel.Warning);

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseDatabaseErrorPage();
            //    app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}
            app.UseExceptionHandler(builder =>
            {
                builder.Run(
                  async context =>
                  {
                      var error = context.Features.Get<IExceptionHandlerFeature>();
                      if (error != null)
                      {
                          var data = Encoding.UTF8.GetBytes(error.Error.Message);

                          await context.Response.Body.WriteAsync(data, 0, data.Length).ConfigureAwait(false);
                      }
                  });
            });

            //app.UseStatusCodePages();


            // 启用gzip压缩
            //app.UseResponseCompression();

            // Response Caching Middleware
            //app.UseResponseCaching();

            app.UseStaticFiles();

            var supportedCultures = new[]
            {
                new CultureInfo("zh-CN"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("zh-CN", "zh-CN"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });


            // 初始化数据
            app.UseDataBaseInitializer();

            //app.UseAuthentication();
            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715
            app.UseIdentityServer();
            //app.UseWebSockets();
            app.UseSignalR(routes => {
                routes.MapHub<CounterHub>("c");
                routes.MapHub<WeatherHub>("w");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute",
                    "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            })
            .UseSwagger()
            .UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectName API V1");
                o.SwaggerEndpoint("/swagger/v2/swagger.json", "ProjectName API V2");
            });
        }
    }
}
