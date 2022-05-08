using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Simple.Core.Extensions;
using Simple.SignalR;

if (args.Contains("-consumer"))
{

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
