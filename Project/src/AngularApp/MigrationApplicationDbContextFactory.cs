using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wjw1.Infrastructure;
using Microsoft.EntityFrameworkCore.Design;
using AngularApp.Extensions;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace AngularApp
{
    public class MigrationSimplDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var contentRootPath = AppContext.BaseDirectory.Remove(AppContext.BaseDirectory.IndexOf("bin"));//Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
                            .SetBasePath(contentRootPath)
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{environmentName}.json", true);

            builder.AddEnvironmentVariables();
            var _configuration = builder.Build();

            //setup DI
            var containerBuilder = new ContainerBuilder();
            IServiceCollection services = new ServiceCollection();

            services.LoadInstalledModules(contentRootPath);
            services.AddCustomizedDataStore(_configuration);
            containerBuilder.Populate(services);
            var _serviceProvider = containerBuilder.Build().Resolve<IServiceProvider>();

            return _serviceProvider.GetRequiredService<ApplicationDbContext>();
        }
        
    }
}
