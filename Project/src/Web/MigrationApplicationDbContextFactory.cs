using System;
using System.IO;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wjw1.Infrastructure;

namespace Web
{
    public class MigrationSimplDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext Create(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var contentRootPath = Directory.GetCurrentDirectory();

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
