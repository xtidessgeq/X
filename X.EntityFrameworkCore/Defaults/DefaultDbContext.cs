using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace X.EntityFrameworkCore.Defaults
{
    /// <summary>
    /// 默认EntityFramework数据上下文
    /// </summary>
    public class DefaultDbContext : DbContextBase
    {
        /// <summary>
        /// 初始化一个<see cref="DefaultDbContext"/>类型的新实例
        /// </summary>
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options, IEntityManager entityManager, IServiceProvider serviceProvider)
            : base(options, entityManager, serviceProvider)
        { }
    }
}