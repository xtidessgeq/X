using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using X.Demo.Core.Blogs.Entities;
using X.EntityFrameworkCore;

namespace X.Demo.EntityConfiguration.Blogs
{

    /// <summary>
    /// 实体映射配置类：博客信息
    /// </summary>
    public class BlogConfiguration : EntityTypeConfigurationBase<Blog, int>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<Blog> builder)
        {
            //builder.HasIndex(m => m.Url).HasName("BlogUrlIndex").IsUnique();
            //builder.HasOne(m => m.User).WithOne().HasForeignKey<Blog>(m => m.UserId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        }
    }
}