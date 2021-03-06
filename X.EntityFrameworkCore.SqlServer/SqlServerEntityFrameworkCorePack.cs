﻿using System;
using System.ComponentModel;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using X.Core;
using X.Core.Packs;
using X.Dependency;
using X.Entity.Database;

namespace X.EntityFrameworkCore.SqlServer
{
    /// <summary>
    /// SqlServerEntityFrameworkCore模块
    /// </summary>
    [Description("SqlServerEntityFrameworkCore模块")]
    public class SqlServerEntityFrameworkCorePack : EntityFrameworkCorePackBase
    {
        /// <summary>
        /// 获取 模块级别
        /// </summary>
        public override PackLevel Level => PackLevel.Framework;
        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动
        /// </summary>
        public override int Order => 1;
        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services = base.AddServices(services);
            services.AddScoped(typeof(ISqlExecutor<,>), typeof(SqlServerDapperSqlExecutor<,>));
            return services;
        }
        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void UsePack(IServiceProvider provider)
        {//TODO 使用配置文件
            bool? hasMsSql = provider.GetOSharpOptions()?.DbContexts?.Values.Any(m => m.DatabaseType == DatabaseType.SqlServer);
            if (hasMsSql == null || !hasMsSql.Value)
            {
                return;
            }
            base.UsePack(provider);
        }
    }
}