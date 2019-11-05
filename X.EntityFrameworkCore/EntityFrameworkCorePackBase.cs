using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using X.Core.Packs;
using X.Entity.Database;
using X.EntityFrameworkCore.Defaults;
using X.EventBuses;
namespace X.EntityFrameworkCore
{
    /// <summary>
    /// EntityFrameworkCore基模块
    /// </summary>
    [DependsOnPacks(typeof(EventBusPack))]
    public abstract class EntityFrameworkCorePackBase : OsharpPack
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override PackLevel Level => PackLevel.Framework;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.TryAddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddOsharpDbContext<DefaultDbContext>();

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void UsePack(IServiceProvider provider)
        {
            IEntityManager manager = provider.GetService<IEntityManager>();
            manager?.Initialize();
            IsEnabled = true;
        }
    }
}