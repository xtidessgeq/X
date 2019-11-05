using System;
using System.Collections.Generic;
using System.Text;
using X.Dependency;

namespace X.Reflection.Finder
{
    /// <summary>
    /// 定义类型查找行为
    /// </summary>
    [IgnoreDependency]
    public interface ITypeFinder : IFinder<Type>
    { }
}
