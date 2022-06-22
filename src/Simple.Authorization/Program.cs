using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Simple.Authorization.Application.Filter;
using Simple.Authorization.Entity.DBContext;
using Simple.Core.Data;
using Simple.Core.Http;
using Simple.Core.Localization;
using Simple.Redis;
using Simple.Web.Extensions;
using Simple.Web.Jwt;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;


services.AddControllers(options =>
{
    options.Filters.Add<AuthorizationFilter>();
});
services.AddSimple();
services.AddSqlServer(AppsettingConfig.GetConnectionString("DbConnection"));
services.AddJwt(AppsettingConfig.GetConnectionString("JwtConnection"));
services.AddRedis(AppsettingConfig.GetConnectionString("RedisConnection"));
services.AddDbContext<AuthorizationDbContext>(d => d.UseSqlServer(AppsettingConfig.GetConnectionString("DbConnection")));


WebApplication app = builder.Build();

app.UseRouting();
app.UseHttpContext();
app.UseSimple();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run("http://localhost:5000");