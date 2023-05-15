using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Simple.Core.Extensions;
using Simple.Utils;


string url = args.Get("-url", "http://*:5000");
Host.CreateDefaultBuilder(args)
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.UseUrls(url);
                 webBuilder.UseStartup<Startup>();
             }).Build().Run();