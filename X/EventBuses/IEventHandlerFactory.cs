using System;
using System.Collections.Generic;
using System.Text;
using X.EventBuses.Internal;

namespace X.EventBuses
{
    /// <summary>
    /// 定义获取<see cref="IEventHandler"/>实例的方式
    /// </summary>
    public interface IEventHandlerFactory
    {
        /// <summary>
        /// 获取事件处理器实例
        /// </summary>
        /// <returns></returns>
        EventHandlerDisposeWrapper GetHandler();
    }
}