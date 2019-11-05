﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using X.Demo.Core.Blogs.Entities;
using X.EntityFrameworkCore;

namespace X.Demo.EntityConfiguration.Blogs
{
    /// <summary>
    /// 实体映射配置类：文章信息
    /// </summary>
    public class PostConfiguration : EntityTypeConfigurationBase<Post, int>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            //builder.HasOne(m => m.Blog).WithMany().HasForeignKey(m => m.BlogId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            //builder.HasOne(m => m.User).WithMany().HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        }
    }
}