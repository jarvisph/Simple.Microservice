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
using Simple.SignalR.Domain.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Simple.Core.Data;
using Simple.RabbitMQ;
using Simple.Core.Localization;

namespace Simple.SignalR
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSimple();
            services.AddSqlServerProvider();
            services.AddSingleton(c => new RabbitOption(AppsettingConfig.GetConnectionString("RabbitConnection")));
            services.AddSignalR(options =>
            {
                options.AddFilter<AuthorizationHubFilter>();
            }).AddHubOptions<PushHub>(options =>
            {
                options.AddFilter<PushHubFitler>();
            });
            services.AddControllers(options =>
            {
                options.Filters.Add<AuthorizationControllerFilter>();
            });
            services.AddDbContext<SignalRDbContext>(d => d.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSimple();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<PushHub>("/hub");
                endpoints.MapControllers();
            });
        }
    }
}
