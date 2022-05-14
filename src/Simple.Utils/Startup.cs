using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Simple.Core.Jobs;
using Simple.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Utils
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSimple();
            services.AddJob();
            services.AddControllers(options =>
            {

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
