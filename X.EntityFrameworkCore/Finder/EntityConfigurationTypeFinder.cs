using Microsoft.Extensions.DependencyInjection;

using X.Dependency;
using X.Reflection;
using X.Reflection.Finder;

namespace X.EntityFrameworkCore.Finder
{
    /// <summary>
    /// 实体类配置类型查找器
    /// </summary>
    [Dependency(ServiceLifetime.Singleton, TryAdd = true)]
    public class EntityConfigurationTypeFinder : BaseTypeFinderBase<IEntityRegister>, IEntityConfigurationTypeFinder
    {
        /// <summary>
        /// 初始化一个<see cref="BaseTypeFinderBase{TBaseType}"/>类型的新实例
        /// </summary>
        public EntityConfigurationTypeFinder(IAllAssemblyFinder allAssemblyFinder)
            : base(allAssemblyFinder)
        { }
    }
}