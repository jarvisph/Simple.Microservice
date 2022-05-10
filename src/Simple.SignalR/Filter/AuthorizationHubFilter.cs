using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Simple.Core.Authorization;
using Simple.Core.Extensions;
using Simple.SignalR.Domain.Services;
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
        private readonly ISignalRAppService _connectionAppService;
        public AuthorizationHubFilter(ISignalRAppService connectionAppService)
        {
            _connectionAppService = connectionAppService;
        }
        public Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
        {
            HttpContext? httpcontext = context.Context.GetHttpContext();
            if (httpcontext == null) throw new AuthorizationException();
            string appKey = httpcontext.GetHeader("appkey");
            if (!Guid.TryParse(appKey, out _)) throw new AuthorizationException("未知应用");
            if (!_connectionAppService.CheckAppKey(appKey.GetValue<Guid>())) throw new AuthorizationException("授权失败");
            else
            {
                Console.WriteLine($"授权成功：{appKey}");
                return next(context);
            }
        }
    }
}
