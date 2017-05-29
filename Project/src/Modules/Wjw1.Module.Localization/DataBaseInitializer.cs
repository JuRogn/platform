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
                        new Culture{ Id="100",Name="en-US",DisplayName="English(US)"},
                        new Culture{ Id="200",Name="zh-CN",DisplayName="简体中文(中国)"},
                    };
                    db.Set<Culture>().AddRangeAsync(culturies);

                    var resourcies = new[] {
                        new Resource{ Id="Culture_Name_100",CultureId="100",Key="Culture_Name",Value="Name"},
                        new Resource{ Id="Culture_Name_Required_100",CultureId="100",Key="Culture_Name_Required",Value="Cultre Name is requred."},
                        new Resource{ Id="Culture_DisplayName_100",CultureId="100",Key="Culture_DisplayName",Value="Display Name"},
                        new Resource{ Id="Culture_DisplayName_Required_100",CultureId="100",Key="Culture_DisplayName_Required",Value="Cultre Display Name is requred."},
                        new Resource{ Id="Resource_Key_100",CultureId="100",Key="Resource_Key",Value="Key"},
                        new Resource{ Id="Resource_Value_100",CultureId="100",Key="Resource_Value",Value="Value"},
                        new Resource{ Id="Resource_Culture_100",CultureId="100",Key="Resource_Culture",Value="Culture"},
                        new Resource{ Id="Resource_Culture_Required_100",CultureId="100",Key="Resource_Culture_Required",Value="Cultre is requred."},
                        new Resource{ Id="Culture_Name_200",CultureId="200",Key="Culture_Name",Value="名称"},
                        new Resource{ Id="Culture_Name_Required_200",CultureId="200",Key="Culture_Name_Required",Value="请输入{0}."},
                        new Resource{ Id="Culture_DisplayName_200",CultureId="200",Key="Culture_DisplayName",Value="显示名称"},
                        new Resource{ Id="Culture_DisplayName_Required_200",CultureId="200",Key="Culture_DisplayName_Required",Value="请输入{0}."},
                        new Resource{ Id="Resource_Key_200",CultureId="200",Key="Resource_Key",Value="键"},
                        new Resource{ Id="Resource_Value_200",CultureId="200",Key="Resource_Value",Value="翻译"},
                        new Resource{ Id="Resource_Culture_200",CultureId="200",Key="Resource_Culture",Value="语言"},
                        new Resource{ Id="Resource_Culture_Required_200",CultureId="200",Key="Resource_Culture_Required",Value="请选择文化语言"},
                    };
                    db.Set<Resource>().AddRangeAsync(resourcies);

                    db.CommitAsync().Wait();
                }
            }
        }
    }
}
