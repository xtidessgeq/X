﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using X.Collections;

using X.Data;
using X.Dependency;
using X.Entity.Database;
using X.Exceptions;

namespace X.EntityFrameworkCore
{
    /// <summary>
    /// 数据上下文扩展方法
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// 当前上下文是否是关系型数据库
        /// </summary>
        public static bool IsRelationalTransaction(this DbContext context)
        {
            return context.Database.GetService<IDbContextTransactionManager>() is IRelationalTransactionManager;
        }

        /// <summary>
        /// 检测关系型数据库是否存在
        /// </summary>
        public static bool ExistsRelationalDatabase(this DbContext context)
        {
            RelationalDatabaseCreator creator = context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            return creator != null && creator.Exists();
        }

        /// <summary>
        /// 获取未提交的迁移记录并提交迁移
        /// </summary>
        public static void CheckAndMigration(this DbContext dbContext)
        {
            string[] migrations = dbContext.Database.GetPendingMigrations().ToArray();
            if (migrations.Length > 0)
            {
                dbContext.Database.Migrate();
                ILoggerFactory loggerFactory = dbContext.GetService<ILoggerFactory>();
                ILogger logger = loggerFactory.CreateLogger("OSharp.Entity.DbContextExtensions");
                logger.LogInformation($"已提交{migrations.Length}条挂起的迁移记录：{migrations.ExpandAndToString()}");
            }
        }

        /// <summary>
        /// 执行指定的Sql语句
        /// </summary>
        public static int ExecuteSqlCommand(this IDbContext dbContext, string sql, params object[] parameters)
        {
            if (!(dbContext is DbContext context))
            {
                throw new OsharpException($"参数dbContext类型为“{dbContext.GetType()}”，不能转换为 DbContext");
            }
            return context.Database.ExecuteSqlCommand(new RawSqlString(sql), parameters);
        }

        /// <summary>
        /// 异步执行指定的Sql语句
        /// </summary>
        public static Task<int> ExecuteSqlCommandAsync(this IDbContext dbContext, string sql, params object[] parameters)
        {
            if (!(dbContext is DbContext context))
            {
                throw new OsharpException($"参数dbContext类型为“{dbContext.GetType()}”，不能转换为 DbContext");
            }
            return context.Database.ExecuteSqlCommandAsync(new RawSqlString(sql), parameters);
        }

        /// <summary>
        /// 更新上下文中指定实体的状态
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="context">上下文对象</param>
        /// <param name="entities">要更新的实体类型</param>
        public static void Update<TEntity, TKey>(this DbContext context, params TEntity[] entities)
            where TEntity : class, IEntity<TKey>
        {
            Check.NotNull(entities, nameof(entities));

            DbSet<TEntity> set = context.Set<TEntity>();
            foreach (TEntity entity in entities)
            {
                try
                {
                    EntityEntry<TEntity> entry = context.Entry(entity);
                    if (entry.State == EntityState.Detached)
                    {
                        set.Attach(entity);
                        entry.State = EntityState.Modified;
                    }
                }
                catch (InvalidOperationException)
                {
                    TEntity oldEntity = set.Find(entity.Id);
                    context.Entry(oldEntity).CurrentValues.SetValues(entity);
                }
            }
        }
    }
}