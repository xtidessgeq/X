
using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using X.Core;
using X.Core.Options;
using X.Core.Packs;
using X.Dependency;
using X.Entity.Database;

namespace X.EntityFrameworkCore.Migration
{
    /// <summary>
    /// 数据迁移模块基类
    /// </summary>
    /// <typeparam name="TDbContext">数据上下文类型</typeparam>
    public abstract class MigrationPackBase<TDbContext> : OsharpPack
        where TDbContext : DbContext
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override PackLevel Level => PackLevel.Framework;

        /// <summary>
        /// 获取 数据库类型
        /// </summary>
        protected abstract DatabaseType DatabaseType { get; }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void UsePack(IServiceProvider provider)
        {
            OsharpOptions options = provider.GetOSharpOptions();
            OsharpDbContextOptions contextOptions = options.GetDbContextOptions(typeof(TDbContext));
            if (contextOptions?.DatabaseType != DatabaseType)
            {
                return;
            }

            using (IServiceScope scope = provider.CreateScope())
            {
                TDbContext context = CreateDbContext(scope.ServiceProvider);
                if (context != null && contextOptions.AutoMigrationEnabled)
                {
                    context.CheckAndMigration();
                    DbContextModelCache modelCache = scope.ServiceProvider.GetService<DbContextModelCache>();
                    modelCache?.Set(context.GetType(), context.Model);
                }
            }

            IsEnabled = true;
        }

        /// <summary>
        /// 重写实现获取数据上下文实例
        /// </summary>
        /// <param name="scopedProvider">服务提供者</param>
        /// <returns></returns>
        protected abstract TDbContext CreateDbContext(IServiceProvider scopedProvider);
    }
}