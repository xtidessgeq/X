using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using X.Core.Packs;
using X.Core;

namespace X.Tests
{
    public class AppUnitTestBase
    {
        public AppUnitTestBase()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpContextAccessor().AddLogging();
            services.AddOSharp<OsharpPackManager>();
            IServiceProvider provider = services.BuildServiceProvider();
            provider.UseOsharp();
            ServiceProvider = provider;
        }

        /// <summary>
        /// ��ȡ �����ṩ��
        /// </summary>
        protected IServiceProvider ServiceProvider { get; private set; }
    }
}
