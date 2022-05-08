using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Simple.Core.Authorization;
using Simple.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.SignalR.Filter
{
    /// <summary>
    /// 授权过滤器
    /// </summary>
    public class AuthorizationHubFilter : IHubFilter
    {
        public Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
        {
            HttpContext? httpcontext = context.Context.GetHttpContext();
            if (httpcontext == null) throw new AuthorizationException();
            string appKey = httpcontext.GetHeader("appkey");
            if (string.IsNullOrWhiteSpace(appKey))
            {
                Console.WriteLine("授权失败");
                throw new AuthorizationException();
            }
            else
            {
                Console.WriteLine($"授权成功：{appKey}");
                return next(context);
            }
        }
    }
}
