using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Simple.Core.Extensions;
using Simple.Translate;

string url = args.Get("-url", "http://localhost:5000");
Host.CreateDefaultBuilder(args)
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.UseUrls(url);
                 webBuilder.UseStartup<Startup>();
             }).Build().Run();