using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Wjw1.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Wjw1.Module.Task.Models;

namespace Wjw1.Module.Task
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
                    var tasks = new[] {
                        new TaskCenter{ Id="t1",EnterpriseId="defaultEnt"},
                        new TaskCenter{ Id="t2",EnterpriseId="defaultEnt"},
                    };
                    db.Set<TaskCenter>().AddRangeAsync(tasks);
                    db.CommitAsync().Wait();
                }
            }
        }
    }
}
