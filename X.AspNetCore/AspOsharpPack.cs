using System;

using Microsoft.AspNetCore.Builder;

using X.Core.Packs;


namespace X.AspNetCore
{
    /// <summary>
    ///  基于AspNetCore环境的Pack模块基类
    /// </summary>
    public abstract class AspOsharpPack : OsharpPack
    {
        /// <summary>
        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">Asp应用程序构建器</param>
        public virtual void UsePack(IApplicationBuilder app)
        {
            base.UsePack(app.ApplicationServices);
        }
    }
}