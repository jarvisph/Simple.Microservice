using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simple.Core.Data;
using Simple.Translate.Domain.DBContext;
using Simple.Translate.Domain.Fitler;
using Simple.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Translate
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
            services.AddControllers(options =>
            {
                options.Filters.Add<AuthorizationFilter>();
            });
            services.AddDbContext<TranslateDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
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
