using Wjw1.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using MediatR;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.EntityFrameworkCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace AngularApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 加载功能模块程序集
        /// </summary>
        /// <param name="services"></param>
        /// <param name="hostingEnvironment"></param>
        /// <returns></returns>
        public static IServiceCollection LoadInstalledModules(this IServiceCollection services,string contentRootPath)
        {
            var modules = new List<ModuleInfo>();
            var moduleRootFolder = new DirectoryInfo(Path.Combine(contentRootPath, "bin"));//"bin/debug/netcoreapp.1.1"
            //var moduleFolders = moduleRootFolder.GetDirectories();
            var binFolder = moduleRootFolder;//
            //foreach (var moduleFolder in moduleFolders)
            //{
            //    var binFolder = new DirectoryInfo(Path.Combine(moduleFolder.FullName, "bin"));
            //    if (!binFolder.Exists)
            //    {
            //        continue;
            //    }

            foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
            {
                if (!file.FullName.Contains("e_sqlite3"))//file.FullName.Contains("Wjw1.Module"))
                {
                    Assembly assembly;
                    try
                    {
                        assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                    }
                    catch (FileLoadException e)
                    {
                        // Get loaded assembly
                        assembly = Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(file.Name)));

                        if (assembly == null)
                        {
                            throw new Exception("Load assembly failed.", e);
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                    if (file.FullName.Contains("Wjw1.Module"))
                    { 
                        modules.Add(new ModuleInfo
                        {
                            Name = assembly.FullName,
                            Assembly = assembly,
                            Path = file.FullName
                        });
                    }
                }
            }
            //}

            GlobalConfiguration.Modules = modules;
            return services;
        }
        public static IServiceCollection AddCustomizedDataStore(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("AngularApp")));
            return services;
        }
        public static IServiceProvider Build(this IServiceCollection services,
            IConfigurationRoot configuration, IHostingEnvironment hostingEnvironment)
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));

            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });

            builder.RegisterInstance(configuration);
            builder.RegisterInstance(hostingEnvironment);
            builder.Populate(services);
            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }
    }
}
