﻿
using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using X.AspNetCore;
using X.Core.Packs;

using StackExchange.Profiling;

namespace X.MiniProfiler
{
    /// <summary>
    /// MiniProfiler模块基类
    /// </summary>
    [DependsOnPacks(typeof(AspNetCorePack))]
    public abstract class MiniProfilerPackBase : AspOsharpPack
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override PackLevel Level => PackLevel.Application;

        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，同一级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 0;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            Action<MiniProfilerOptions> miniProfilerAction = GetMiniProfilerAction(services);

            services.AddMiniProfiler(miniProfilerAction).AddEntityFramework();


            return services;
        }

        /// <summary>
        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">Asp应用程序构建器</param>
        public override void UsePack(IApplicationBuilder app)
        {
            app.UseMiniProfiler();
            IsEnabled = true;
        }

        /// <summary>
        /// 重写以获取MiniProfiler的选项
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        protected virtual Action<MiniProfilerOptions> GetMiniProfilerAction(IServiceCollection services)
        {
            return options =>
            {
                options.RouteBasePath = "/profiler";
            };
        }
    }
}