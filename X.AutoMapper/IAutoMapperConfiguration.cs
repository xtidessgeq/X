using AutoMapper.Configuration;
using X.Dependency;

namespace X.AutoMapper
{
    /// <summary>
    /// 定义通过<see cref="MapperConfigurationExpression"/>配置对象映射的功能
    /// </summary>
    [MultipleDependency]
    public interface IAutoMapperConfiguration
    {
        /// <summary>
        /// 创建对象映射
        /// </summary>
        /// <param name="mapper">映射配置表达</param>
        void CreateMaps(MapperConfigurationExpression mapper);
    }
}