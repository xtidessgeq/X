﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using X.Demo.Core.Blogs.Entities;
using X.Entity;
using X.Mapping.Finder;

namespace X.Demo.Core.Blogs.Dtos
{
    /// <summary>
    /// 输入DTO：博客信息
    /// </summary>
    [MapTo(typeof(Blog))]
    public class BlogInputDto : IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 博客编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 博客地址
        /// </summary>
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// 获取或设置 显示名称
        /// </summary>
        [Required]
        public string Display { get; set; }
    }
}