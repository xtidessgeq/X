﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace X.Core.Packs
{
    /// <summary>
    /// 定义Osharp模块管理器
    /// </summary>
    public interface IOsharpPackManager
    {
        /// <summary>
        /// 获取 自动检索到的所有模块信息
        /// </summary>
        IEnumerable<OsharpPack> SourcePacks { get; }

        /// <summary>
        /// 获取 最终加载的模块信息集合
        /// </summary>
        IEnumerable<OsharpPack> LoadedPacks { get; }

        /// <summary>
        /// 加载模块服务
        /// </summary>
        /// <param name="services">服务容器</param>
        /// <returns>服务容器</returns>
        IServiceCollection LoadPacks(IServiceCollection services);

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        void UsePack(IServiceProvider provider);
    }
}