using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Simple.Authorization;

Host.CreateDefaultBuilder(args)
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.UseUrls("http://localhost:5000");
                 webBuilder.UseStartup<Startup>();
             }).Build().Run();