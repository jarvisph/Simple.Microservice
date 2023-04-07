using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Simple.Core.Extensions;
using Simple.Core.Helper;
using Simple.Core.Localization;
using Simple.Translate;
using Simple.Web.Extensions;
using Simple.Core.Data;
using System.Text.RegularExpressions;
using Simple.Translate.Application;
using Newtonsoft.Json;

if (args.Contains("test"))
{
    IServiceCollection services = new ServiceCollection();
    services.AddSimple();
    services.AddSqlServer(AppsettingConfig.GetConnectionString("DbConnection"));
    TranslateAppService translateAppService = new TranslateAppService();

    string file = File.ReadAllText("D:\\Project\\Simple.Microservice\\src\\Simple.Translate\\wwwroot\\test.html");
    List<string> words = new List<string>();
    ////第一种，标签内的中文
    //{
    //    ConsoleHelper.WriteLine("验证标签内的文字：", ConsoleColor.Red);
    //    Regex reg = new Regex("<[a-zA-Z]+.*?>(?<TEXT>[\\n\\s*\\r\u4e00-\u9fa5]*?)</[a-zA-Z]*?>");
    //    MatchCollection match = reg.Matches(file);

    //    foreach (Match item in match)
    //    {
    //        string text = item.Groups["TEXT"].Value.Trim();
    //        if (!string.IsNullOrWhiteSpace(text))
    //        {
    //            words.Add(text);
    //        }
    //    }
    //}

    {
        ConsoleHelper.WriteLine("验证单双引号中的文字：", ConsoleColor.Red);
        //[\"\'\[\(\（](?<TEXT>[\u4e00-\u9fa5/]+)[\"\'\-\]\)\<\）]
        Regex reg = new Regex("[\\\"\\'\\[\\(\\（\\>](?<TEXT>[\\u4e00-\\u9fa5/]+)[\\\"\\'\\-\\]\\)\\<\\）\\：]");
        MatchCollection match = reg.Matches(file);

        foreach (Match item in match)
        {
            string text = item.Groups["TEXT"].Value.Trim();
            if (new Regex("[\\u4e00-\\u9fa5]").IsMatch(text))
            {
                words.Add(text);
            }
        }
    }


    //{
    //    ConsoleHelper.WriteLine("验证结尾:的文字：", ConsoleColor.Red);
    //    Regex reg = new Regex("\\>(?<TEXT>[\\u4e00-\\u9fa5a-zA-Z0-9]+)\\:");
    //    MatchCollection match = reg.Matches(file);

    //    foreach (Match item in match)
    //    {
    //        string text = item.Groups["TEXT"].Value.Trim();
    //        if (!string.IsNullOrWhiteSpace(text))
    //        {
    //            words.Add(text);
    //        }
    //    }
    //}

    //{
    //    ConsoleHelper.WriteLine("验证结尾中文：的文字：", ConsoleColor.Red);
    //    Regex reg = new Regex("\\>(?<TEXT>[\\u4e00-\\u9fa5a-zA-Z0-9]+)\\：");
    //    MatchCollection match = reg.Matches(file);

    //    foreach (Match item in match)
    //    {
    //        string text = item.Groups["TEXT"].Value.Trim();
    //        if (!string.IsNullOrWhiteSpace(text))
    //        {
    //            words.Add(text);
    //        }
    //    }
    //}

    Console.WriteLine(JsonConvert.SerializeObject(words));

    //{
    //    ConsoleHelper.WriteLine("验证HTML注释文字：", ConsoleColor.Red);
    //    Regex reg = new Regex("\\--(?<TEXT>[\\u4e00-\\u9fa5a-zA-Z0-9]+)\\--");
    //    MatchCollection match = reg.Matches(file);

    //    foreach (Match item in match)
    //    {
    //        string text = item.Groups["TEXT"].Value.Trim();
    //        if (!string.IsNullOrWhiteSpace(text))
    //        {
    //            Console.WriteLine(text);
    //        }
    //    }
    //}


    //{
    //    ConsoleHelper.WriteLine("验证JS注释文字：", ConsoleColor.Red);
    //    Regex reg = new Regex("\\//(?<TEXT>[\\u4e00-\\u9fa5a-zA-Z0-9]+)?");
    //    MatchCollection match = reg.Matches(file);

    //    foreach (Match item in match)
    //    {
    //        string text = item.Groups["TEXT"].Value.Trim();
    //        if (!string.IsNullOrWhiteSpace(text))
    //        {
    //            Console.WriteLine(text);
    //        }
    //    }
    //}

}
else
{
    string url = args.Get("-url", "http://*:4012");
    Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseUrls(url);
                     webBuilder.UseStartup<Startup>();
                 }).Build().Run();
}
