using System;
using System.ComponentModel;

using X.Core.Packs;
using X.Entity;
using X.Entity.Database;
using X.EntityFrameworkCore;
using X.EntityFrameworkCore.Defaults;
using X.EntityFrameworkCore.Migration;
using X.EntityFrameworkCore.SqlServer;

namespace X.Web.Startups
{
    /// <summary>
    /// SqlServer-DefaultDbContext迁移模块
    /// </summary>
    [DependsOnPacks(typeof(SqlServerEntityFrameworkCorePack))]
    [Description("SqlServer-DefaultDbContext迁移模块")]
    public class SqlServerDefaultDbContextMigrationPack : MigrationPackBase<DefaultDbContext>
    {
        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 2;

        /// <summary>
        /// 获取 数据库类型
        /// </summary>
        protected override DatabaseType DatabaseType => DatabaseType.SqlServer;

        protected override DefaultDbContext CreateDbContext(IServiceProvider scopedProvider)
        {

          //todo 为什么不使用这个 return (DefaultDbContext)scopedProvider.GetService(typeof(DefaultDbContext));
           return new SqlServerDesignTimeDefaultDbContextFactory(scopedProvider).CreateDbContext(new string[0]);
        }
    }
}