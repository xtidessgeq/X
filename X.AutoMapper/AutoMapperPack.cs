using System;
using System.ComponentModel;
using System.Linq;

using AutoMapper;
using AutoMapper.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using X.Core.Packs;
using X.Mapping;

using IMapper = X.Mapping.IMapper;

namespace X.AutoMapper
{
    /// <summary>
    /// AutoMapper模块
    /// </summary>
    [Description("AutoMapper模块")]
    public class AutoMapperPack : OsharpPack
    {
        /// <summary>
        /// 获取 模块级别
        /// </summary>
        public override PackLevel Level => PackLevel.Framework;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.TryAddSingleton<MapperConfigurationExpression>(new MapperConfigurationExpression());

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void UsePack(IServiceProvider provider)
        {
            MapperConfigurationExpression cfg = provider.GetService<MapperConfigurationExpression>();

            //获取已注册到IoC的所有Profile
            IMapTuple[] tuples = provider.GetServices<IMapTuple>().ToArray();
            foreach (IMapTuple mapTuple in tuples)
            {
                mapTuple.CreateMap();
                cfg.AddProfile(mapTuple as Profile);
            }

            //各个模块DTO的 IAutoMapperConfiguration 映射实现类
            IAutoMapperConfiguration[] configs = provider.GetServices<IAutoMapperConfiguration>().ToArray();
            foreach (IAutoMapperConfiguration config in configs)
            {
                config.CreateMaps(cfg);
            }

            MapperConfiguration configuration = new MapperConfiguration(cfg);

            IMapper mapper = new AutoMapperMapper(configuration);
            MapperExtensions.SetMapper(mapper);

            IsEnabled = true;
        }
    }
}