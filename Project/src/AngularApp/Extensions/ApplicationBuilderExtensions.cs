using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularApp.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDataBaseInitializer(this IApplicationBuilder app)
        {
            new DataBaseInitializer().SeedData(app);
            //获取功能模块的数据初始化程序并调用
            new Wjw1.Module.Localization.DataBaseInitializer().SeedData(app);
            return app;
        }
    }
}
