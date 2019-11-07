
using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

using X.Dependency;
using X.Entity;


namespace X.AspNetCore.Mvc.Filters
{
    /// <summary>
    /// 自动事务提交过滤器，在<see cref="ActionFilterAttribute.OnResultExecuted"/>方法中执行<see cref="IUnitOfWork.Commit()"/>进行事务提交
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    [Dependency(ServiceLifetime.Scoped, AddSelf = true)]
    public class UnitOfWorkAttribute : ServiceFilterAttribute
    {
        /// <summary>
        /// 初始化一个<see cref="UnitOfWorkAttribute"/>类型的新实例
        /// </summary>
        public UnitOfWorkAttribute()
            : base(typeof(UnitOfWorkFilterImpl))
        { }
    }
}