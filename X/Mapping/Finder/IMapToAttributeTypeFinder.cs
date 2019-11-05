using System;
using System.Collections.Generic;
using System.Text;
using X.Reflection.Finder;

namespace X.Mapping.Finder
{
    /// <summary>
    /// 标注了<see cref="MapToAttribute"/>标签的类型查找器
    /// </summary>
    public interface IMapToAttributeTypeFinder : ITypeFinder
    { }
}