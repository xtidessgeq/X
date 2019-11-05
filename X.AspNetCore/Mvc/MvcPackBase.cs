using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json.Serialization;
using X.AspNetCore.Mvc.Conventions;
using X.Core.Packs;


namespace X.AspNetCore.Mvc
{
    /// <summary>
    /// Mvc模块基类
    /// </summary>
    [DependsOnPacks(typeof(AspNetCorePack))]
    public abstract class MvcPackBase : AspOsharpPack
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override PackLevel Level => PackLevel.Application;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services = AddCors(services);

#if NETCOREAPP3_0
            services.AddControllersWithViews(options =>
            {
                options.Conventions.Add(new DashedRoutingConvention());
                options.Filters.Add(new OnlineUserAuthorizationFilter()); // 构建在线用户信息
                options.Filters.Add(new FunctionAuthorizationFilter()); // 全局功能权限过滤器
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
#else
            //services.AddMvcCore(options =>
            //{
            //    options.Conventions.Add(new DashedRoutingConvention());
            //    //options.Filters.Add(new OnlineUserAuthorizationFilter()); // 构建在线用户信息
            //    //options.Filters.Add(new FunctionAuthorizationFilter()); // 全局功能权限过滤器
            //}).AddJsonOptions(options =>
            //{
            //    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //}).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMvcCore(options =>
            {
                options.Conventions.Add(new DashedRoutingConvention());
                //options.Filters.Add(new OnlineUserAuthorizationFilter()); // 构建在线用户信息
                //options.Filters.Add(new FunctionAuthorizationFilter()); // 全局功能权限过滤器
            });
#endif

            //todo    services.AddScoped<UnitOfWorkFilterImpl>();
            services.AddHttpsRedirection(opts => opts.HttpsPort = 443);
            services.AddDistributedMemoryCache();

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        public override void UsePack(IApplicationBuilder app)
        {
#if NETCOREAPP3_0
            app.UseRouting();
            UseCors(app);
#else   
            UseCors(app);
          //todo  app.UseMvcWithAreaRoute();
#endif

            IsEnabled = true;
        }

        /// <summary>
        /// 重写以实现添加Cors服务
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        protected virtual IServiceCollection AddCors(IServiceCollection services)
        {
            return services;
        }

        /// <summary>
        /// 重写以应用Cors
        /// </summary>
        protected virtual IApplicationBuilder UseCors(IApplicationBuilder app)
        {
            return app;
        }
    }
}