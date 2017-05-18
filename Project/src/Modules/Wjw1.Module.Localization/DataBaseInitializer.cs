using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Wjw1.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Wjw1.Module.Localization.Models;

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
                    var culturies = new[] {
                        new Culture{ Id="100",Name="en_US"},
                        new Culture{ Id="200",Name="zh_CN"},
                    };
                    db.Set<Culture>().AddRangeAsync(culturies);

                    var resourcies = new[] {
                        new Resource{ Id="100101",CultureId="en_US",Key="Culture_Name",Value="Name"},
                        new Resource{ Id="100102",CultureId="en_US",Key="Resource_Key",Value="Key"},
                        new Resource{ Id="100103",CultureId="en_US",Key="Resource_Value",Value="Value"},
                        new Resource{ Id="100104",CultureId="en_US",Key="Resource_Culture",Value="Culture"},
                        new Resource{ Id="100105",CultureId="en_US",Key="Resource_Culture_Required",Value="Cultre is requred."},
                        new Resource{ Id="200101",CultureId="zh_cn",Key="Culture_Name",Value="名称"},
                        new Resource{ Id="200102",CultureId="zh_cn",Key="Resource_Key",Value="键"},
                        new Resource{ Id="200103",CultureId="zh_cn",Key="Resource_Value",Value="翻译"},
                        new Resource{ Id="200104",CultureId="zh_cn",Key="Resource_Culture",Value="语言"},
                        new Resource{ Id="200105",CultureId="zh_cn",Key="Resource_Culture_Required",Value="请选择文化语言"},
                    };
                    db.Set<Resource>().AddRangeAsync(resourcies);

                    db.CommitAsync().Wait();
                }
            }
        }
    }
}
