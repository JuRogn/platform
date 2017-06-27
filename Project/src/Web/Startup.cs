using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;
using Microsoft.AspNetCore.Localization;
using Web.Helpers;
using Web.Services;
using Wjw1.Infrastructure;
using Wjw1.Infrastructure.Models;
using Web.Extensions;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Localization;
using Wjw1.Module.Localization;

namespace Web
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

            if (env.IsDevelopment())
                builder.AddUserSecrets<Startup>();
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
            services.LoadInstalledModules(Modules, _env);

            // 连接弹性（Connection resiliency） 
            // 所谓的连接弹性则是执行数据库命令失败时我们可以重试
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")
                ,b=>b.MigrationsAssembly("Web")                
                ));

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
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

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
                // 身份验证
                a.Filters.Add(new UserAuthorizeAttribute(new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build()));

                // 记录action 日志
                a.Filters.Add(typeof(LogFilter));
            })
            .AddViewLocalization()
            .AddDataAnnotationsLocalization();

            // 启用gzip压缩
            services.AddResponseCompression();

            services.AddOptions();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
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
            app.UseResponseCompression();

            // Response Caching Middleware
            app.UseResponseCaching();

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
            
            // 
            //app.UseIdentity();
            app.UseAuthentication();
            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715
            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute",
                    "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class AppSettings
    {
        public string SystemName { get; set; }

        public string Copyright { get; set; }
    }
}