using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using X.Core;
using X.AspNetCore;

namespace X.Tests
{
    public class TestStartup
    {
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddLogging();
            services.AddOSharp<AspOsharpPackManager>();
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            app.UseOSharp();
        }
    }
}
