using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Wjw1.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Wjw1.Module.Localization.Models;
using System.Linq;

namespace Wjw1.Module.Localization
{
    public class DataBaseInitializer : IDataBaseInitializer
    {
        public void SeedData(IApplicationBuilder app)
        {
            using (var serviceScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;

                using (var db = serviceProvider.GetService<ApplicationDbContext>())
                {
                    if(db.Set<Culture>().Any(c=>c.Id.Equals("100")))
                        return;

                    var culturies = new[] {
                        new Culture{ Id="100",Name="en-US"},
                        new Culture{ Id="200",Name="zh-CN"},
                    };
                    db.Set<Culture>().AddRangeAsync(culturies);

                    var resourcies = new[] {
                        new Resource{ Id="100101",CultureId="100",Key="Culture_Name",Value="Name"},
                        new Resource{ Id="100102",CultureId="100",Key="Culture_Name_Required",Value="Cultre Name is requred."},
                        new Resource{ Id="100111",CultureId="100",Key="Resource_Key",Value="Key"},
                        new Resource{ Id="100112",CultureId="100",Key="Resource_Value",Value="Value"},
                        new Resource{ Id="100113",CultureId="100",Key="Resource_Culture",Value="Culture"},
                        new Resource{ Id="100114",CultureId="100",Key="Resource_Culture_Required",Value="Cultre is requred."},
                        new Resource{ Id="200101",CultureId="200",Key="Culture_Name",Value="名称"},
                        new Resource{ Id="200102",CultureId="200",Key="Culture_Name_Required",Value="请输入{0}."},
                        new Resource{ Id="200111",CultureId="200",Key="Resource_Key",Value="键"},
                        new Resource{ Id="200112",CultureId="200",Key="Resource_Value",Value="翻译"},
                        new Resource{ Id="200113",CultureId="200",Key="Resource_Culture",Value="语言"},
                        new Resource{ Id="200114",CultureId="200",Key="Resource_Culture_Required",Value="请选择文化语言"},
                    };
                    db.Set<Resource>().AddRangeAsync(resourcies);

                    db.CommitAsync().Wait();
                }
            }
        }
    }
}
