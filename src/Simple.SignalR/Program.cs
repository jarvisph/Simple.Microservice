using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Simple.Core.Data;
using Simple.Core.Extensions;
using Simple.Core.Localization;
using Simple.RabbitMQ;
using Simple.SignalR;
using Simple.Web.Extensions;

if (args.Contains("-consumer"))
{
    IServiceCollection services = new ServiceCollection();
    services.AddSimple();
    services.AddSqlServerProvider();
    services.AddRabbitMQ(new RabbitOption(AppsettingConfig.GetConnectionString("RabbitConnection")));
    Thread.Sleep(-1);
}
else
{
    string url = args.Get("-url", "http://localhost:5000");
    Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseUrls(url);
                     webBuilder.UseStartup<Startup>();
                 }).Build().Run();
}
