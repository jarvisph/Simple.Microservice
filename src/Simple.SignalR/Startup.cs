using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Simple.SignalR.Hubs;
using Simple.Web.Extensions;
using Simple.SignalR.Filter;

namespace Simple.SignalR
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(options =>
            {
                options.AddFilter<AuthorizationHubFilter>();
            }).AddHubOptions<PushHub>(options =>
            {
                options.AddFilter<PushHubFitler>();
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSimple();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<PushHub>("/hub");
            });
        }
    }
}
