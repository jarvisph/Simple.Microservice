using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Simple.Authorization.Entity.DBContext;
using Simple.Core.Data;
using Simple.Core.Http;
using Simple.Web.Extensions;
using Simple.Web.Jwt;
using Simple.Redis;
using Simple.Authorization.Application.Filter;

namespace Simple.Authorization
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
            services.AddControllers(options =>
            {
                options.Filters.Add<AuthorizationFilter>();
            });
            services.AddSimple();
            services.AddSqlServer(Configuration.GetConnectionString("DbConnection"));
            services.AddJwt(Configuration.GetConnectionString("JwtConnection"));
            services.AddRedis(Configuration.GetConnectionString("RedisConnection"));
            services.AddDbContext<AuthorizationDbContext>(d => d.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseHttpContext();
            app.UseSimple();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
