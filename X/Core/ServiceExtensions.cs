using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using X.Core.Builders;
using X.Core.Options;
using X.Core.Packs;
using X.Data;
using X.Dependency;
using X.Entity.Database;
using X.Reflection;
using X.Reflection.Finder;

namespace X.Core
{
    /// <summary>
    /// 依赖注入服务集合扩展
    /// </summary>
    public static class ServiceExtensions
    {
        public static IServiceCollection AddOSharp<TOsharpPackManager>(this IServiceCollection services, Action<IOsharpBuilder> builderAction = null)
              where TOsharpPackManager : IOsharpPackManager, new()
        {
            Check.NotNull(services, nameof(services));

            IConfiguration configuration = services.GetConfiguration();
            Singleton<IConfiguration>.Instance = configuration;

            //初始化所有程序集查找器
            services.TryAddSingleton<IAllAssemblyFinder>(new AppDomainAllAssemblyFinder());

            IOsharpBuilder builder = services.GetSingletonInstanceOrNull<IOsharpBuilder>() ?? new OsharpBuilder();
            builderAction?.Invoke(builder);
            services.TryAddSingleton<IOsharpBuilder>(builder);

            TOsharpPackManager manager = new TOsharpPackManager();
            services.AddSingleton<IOsharpPackManager>(manager);
            manager.LoadPacks(services);
            return services;
        }

       
        /// <summary>
        /// 获取<see cref="IConfiguration"/>配置信息
        /// </summary>
        public static IConfiguration GetConfiguration(this IServiceCollection services)
        {
            return services.GetSingletonInstanceOrNull<IConfiguration>();
        }

        /// <summary>
        /// 从服务提供者中获取OSharpOptions
        /// </summary>
        public static OsharpOptions GetOSharpOptions(this IServiceProvider provider)
        {
            return provider.GetService<IOptions<OsharpOptions>>()?.Value;
        }

        /// <summary>
        /// 获取指定类型的日志对象
        /// </summary>
        /// <typeparam name="T">非静态强类型</typeparam>
        /// <returns>日志对象</returns>
        public static ILogger<T> GetLogger<T>(this IServiceProvider provider)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger<T>();
        }

        /// <summary>
        /// 获取指定类型的日志对象
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="type">指定类型</param>
        /// <returns>日志对象</returns>
        public static ILogger GetLogger(this IServiceProvider provider, Type type)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger(type);
        }

        /// <summary>
        /// 获取指定名称的日志对象
        /// </summary>
        public static ILogger GetLogger(this IServiceProvider provider, string name)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger(name);
        }

        /// <summary>
        /// 获取指定实体类的上下文所在工作单元
        /// </summary>
        public static IUnitOfWork GetUnitOfWork<TEntity, TKey>(this IServiceProvider provider) where TEntity : IEntity<TKey>
        {
            IUnitOfWorkManager unitOfWorkManager = provider.GetService<IUnitOfWorkManager>();
            return unitOfWorkManager.GetUnitOfWork<TEntity, TKey>();
        }

        ///// <summary>
        ///// 获取指定实体类型的上下文对象
        ///// </summary>
        //public static IDbContext GetDbContext<TEntity, TKey>(this IServiceProvider provider) where TEntity : IEntity<TKey>
        //{
        //    IUnitOfWorkManager unitOfWorkManager = provider.GetService<IUnitOfWorkManager>();
        //    return unitOfWorkManager.GetDbContext<TEntity, TKey>();
        //}

        /// <summary>
        /// OSharp框架初始化，适用于非AspNetCore环境
        /// </summary>
        public static IServiceProvider UseOsharp(this IServiceProvider provider)
        {
            IOsharpPackManager packManager = provider.GetService<IOsharpPackManager>();
            packManager.UsePack(provider);
            return provider;
        }
    }
}
