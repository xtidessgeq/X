using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using X.AspNetCore.UI;
using X.Data;
using X.Demo.Core.Blogs;
using X.Demo.Core.Blogs.Dtos;
using X.Demo.Core.Blogs.Entities;

namespace X.Web.Controllers
{
    [Description("管理-博客信息")]
    public class BlogController : ControllerBase
    {
        /// <summary>
        /// 初始化一个<see cref="BlogController"/>类型的新实例
        /// </summary>
        public BlogController(IBlogsContract blogsContract)
        {
            BlogsContract = blogsContract;
        }
        /// <summary>
        /// 获取或设置 博客模块业务契约对象
        /// </summary>
        protected IBlogsContract BlogsContract { get; }

        /// <summary>
        /// 读取博客列表信息
        /// </summary>
        /// <param name="request">页请求信息</param>
        /// <returns>博客列表分页信息</returns>
        [HttpPost]
        [Description("读取")]
        public IEnumerable<Blog> Read( )
        {
            var page = BlogsContract.Blogs.ToList();

            return page;
        }

        /// <summary>
        /// 申请开通博客
        /// </summary>
        /// <param name="dto">博客输入DTO</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [Description("申请")]
        public async Task<AjaxResult> Apply(BlogInputDto dto)
        {
            Check.NotNull(dto, nameof(dto));
            OperationResult result = await BlogsContract.ApplyForBlog(dto);
            return result.ToAjaxResult();
        }

        /// <summary>
        /// 审核博客
        /// </summary>
        /// <param name="dto">博客输入DTO</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [Description("审核")]
        public async Task<AjaxResult> Verify(BlogVerifyDto dto)
        {
            Check.NotNull(dto, nameof(dto));
            OperationResult result = await BlogsContract.VerifyBlog(dto);
            return result.ToAjaxResult();
        }

        /// <summary>
        /// 更新博客信息
        /// </summary>
        /// <param name="dtos">博客信息输入DTO</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [Description("更新")]
        public async Task<AjaxResult> Update(BlogInputDto[] dtos)
        {
            Check.NotNull(dtos, nameof(dtos));
            OperationResult result = await BlogsContract.UpdateBlogs(dtos);
            return result.ToAjaxResult();
        }
    }
}