﻿using System.Data;
using System.Data.SqlClient;
#if NETSTANDARD2_1
using Microsoft.Data.SqlClient;
using X.Entity.Database;
#endif

namespace X.EntityFrameworkCore.SqlServer
{
    /// <summary>
    /// SqlServer的Dapper-Sql功能执行
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">编号类型</typeparam>
    public class SqlServerDapperSqlExecutor<TEntity, TKey> : SqlExecutorBase<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// 初始化一个<see cref="SqlExecutorBase{TEntity,TKey}"/>类型的新实例
        /// </summary>
        public SqlServerDapperSqlExecutor(IUnitOfWorkManager unitOfWorkManager)
            : base(unitOfWorkManager)
        { }

        /// <summary>
        /// 获取 数据库类型
        /// </summary>
        public override DatabaseType DatabaseType => DatabaseType.SqlServer;

        /// <summary>
        /// 重写以获取数据连接对象
        /// </summary>
        /// <param name="connectionString">数据连接字符串</param>
        /// <returns></returns>
        protected override IDbConnection GetDbConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}