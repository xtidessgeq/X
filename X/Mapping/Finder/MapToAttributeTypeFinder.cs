
using Microsoft.Extensions.DependencyInjection;

using X.Dependency;
using X.Reflection;
using X.Reflection.Finder;

namespace X.Mapping.Finder
{
    /// <summary>
    /// 标注了<see cref="MapToAttribute"/>标签的类型查找器
    /// </summary>
    [Dependency(ServiceLifetime.Singleton, TryAdd = true)]
    public class MapToAttributeTypeFinder : AttributeTypeFinderBase<MapToAttribute>, IMapToAttributeTypeFinder
    {
        /// <summary>
        /// 初始化一个<see cref="MapToAttributeTypeFinder"/>类型的新实例
        /// </summary>
        public MapToAttributeTypeFinder(IAllAssemblyFinder allAssemblyFinder)
            : base(allAssemblyFinder)
        { }
    }
}
