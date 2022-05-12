using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Simple.Logger.Domain.Filter;
using Simple.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Logger
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSimple();
            services.AddControllers(options =>
            {
                options.Filters.Add<AuthorizationFilter>();
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSimple();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
