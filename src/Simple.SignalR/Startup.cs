using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simple.Core.Data;
using Simple.RabbitMQ;
using Simple.SignalR.Domain.DbContext;
using Simple.SignalR.Filter;
using Simple.SignalR.Hubs;
using Simple.Web.Extensions;

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
            services.AddSqlServer(Configuration.GetConnectionString("DbConnection"));
            services.AddSingleton(c => new RabbitOption(Configuration.GetConnectionString("RabbitConnection")));
            services.AddDbContext<SignalRDbContext>(d => d.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

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
