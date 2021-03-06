﻿using System;
using System.Collections.Generic;
using System.Text;

namespace X.EventBuses
{
    /// <summary>
    /// 定义事件总线构建器
    /// </summary>
    public interface IEventBusBuilder
    {
        /// <summary>
        /// 构建事件总线
        /// </summary>
        void Build();
    }
}