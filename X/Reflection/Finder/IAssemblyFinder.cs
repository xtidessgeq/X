using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using X.Dependency;

namespace X.Reflection.Finder
{
    /// <summary>
    /// 定义程序集查找器
    /// </summary>
    [IgnoreDependency]
    public interface IAssemblyFinder : IFinder<Assembly>
    { }
}