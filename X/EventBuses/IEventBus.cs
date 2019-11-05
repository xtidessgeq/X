using System;
using System.Collections.Generic;
using System.Text;

namespace X.EventBuses
{
    /// <summary>
    /// 定义线程总线
    /// </summary>
    public interface IEventBus : IEventSubscriber, IEventPublisher
    {

    }
}