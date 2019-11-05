﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using X.AspNetCore.Mvc;

namespace X.Web.Startups
{
    /// <summary>
    /// MVC模块，此模块需要在Identity之后启动
    /// </summary>
    [Description("MVC模块")]
    public class AspNetCoreMvcPack : MvcPackBase
    {
        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 1;
    }
}