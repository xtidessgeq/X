using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

using X.AspNetCore;
using X.Core.Packs;
using X.Exceptions;


namespace X.AspNetCore
{
    /// <summary>
    /// <see cref="IApplicationBuilder"/>辅助扩展方法
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// OSharp框架初始化，适用于AspNetCore环境
        /// </summary>
        public static IApplicationBuilder UseOSharp(this IApplicationBuilder app)
    {
        IServiceProvider provider = app.ApplicationServices;
        if (!(provider.GetService<IOsharpPackManager>() is IAspUsePack aspPackManager))
        {
            throw new OsharpException("接口 IOsharpPackManager 的注入类型不正确，该类型应同时实现接口 IAspUsePack");
        }
        aspPackManager.UsePack(app);

        return app;
    }

    /// <summary>
    /// 添加MVC并Area路由支持
    /// </summary>
    public static IApplicationBuilder UseMvcWithAreaRoute(this IApplicationBuilder app, bool area = true)
    {
        return app.UseMvc(builder =>
        {
            if (area)
            {
                builder.MapRoute("area", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            }
            builder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
        });
    }

#if NETCOREAPP3_0

        /// <summary>
        /// 添加Endpoint并Area路由支持
        /// </summary>
        public static IEndpointRouteBuilder MvcEndpointsWithAreaRoute(this IEndpointRouteBuilder endpoints, bool area = true)
        {
            if (area)
            {
                endpoints.MapControllerRoute("area", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            }

            endpoints.MapDefaultControllerRoute();
            return endpoints;
        }

#endif
}
}