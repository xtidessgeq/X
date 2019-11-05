using System;
using System.Collections.Generic;
using System.Text;
using X.Dependency;

namespace X.Mapping
{
    /// <summary>
    /// 定义对象映射源与目标配对
    /// </summary>
    [MultipleDependency]
    public interface IMapTuple
    {
        /// <summary>
        /// 执行对象映射构造
        /// </summary>
        void CreateMap();
    }
}