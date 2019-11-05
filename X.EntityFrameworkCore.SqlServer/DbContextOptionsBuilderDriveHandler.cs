using System;
using System.Data.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using X.Dependency;
using X.Entity.Database;

namespace X.EntityFrameworkCore.SqlServer
{
    /// <summary>
    /// SqlServer的<see cref="DbContextOptionsBuilder"/>数据库驱动差异处理器
    /// </summary>
    [Dependency(ServiceLifetime.Singleton)]
    public class DbContextOptionsBuilderDriveHandler : IDbContextOptionsBuilderDriveHandler
    {
        /// <summary>
        /// 获取 数据库类型名称，如 SQLSERVER，MYSQL，SQLITE等
        /// </summary>
        public DatabaseType Type { get; } = DatabaseType.SqlServer;

        /// <summary>
        /// 处理<see cref="DbContextOptionsBuilder"/>驱动差异
        /// </summary>
        /// <param name="builder">创建器</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="existingConnection">已存在的连接对象</param>
        /// <returns></returns>
        public DbContextOptionsBuilder Handle(DbContextOptionsBuilder builder, string connectionString, DbConnection existingConnection)
        {
            if (existingConnection == null)
            {
                return builder.UseSqlServer(connectionString, opts => opts.UseRowNumberForPaging());
            }
            return builder.UseSqlServer(existingConnection, opts => opts.UseRowNumberForPaging());
        }
    }
}