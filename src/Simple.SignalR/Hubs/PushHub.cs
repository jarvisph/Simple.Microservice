using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Core.Extensions;
using Simple.SignalR.Queues;
using Simple.RabbitMQ;

namespace Simple.SignalR.Hubs
{
    /// <summary>
    /// 推送通信
    /// </summary>
    public class PushHub : Hub
    {
        /// <summary>
        /// 连接时触发
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            new ConnectionQueue(Context.ConnectionId, Context.GetHttpContext()).Send();
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// 断开时触发
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            new ConnectionQueue(Context.ConnectionId, Context.GetHttpContext()).Send();
            await base.OnDisconnectedAsync(exception);
        }
        /// <summary>
        /// 对所有连接的客户端调用方法
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string channel, string message)
        {
            HttpContext? context = Context.GetHttpContext();
            string appkey = context.GetHeader("appkey");
            Console.WriteLine($"发送成功=>频道：{channel}，appKey=>{appkey}");
            new PushLogQueue(Context.ConnectionId, channel, message, appkey).Send();
            await Clients.All.SendAsync(channel, message);
        }

        /// <summary>
        /// 对调用了中心方法的客户端调用方法
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessageToCaller(string user, string message)
            => await Clients.Caller.SendAsync("ReceiveMessage", user, message);

        /// <summary>
        /// 对指定组中的所有连接调用方法
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessageToGroup(string user, string message)
            => await Clients.Group("SignalR Users").SendAsync("ReceiveMessage", user, message);
    }
}
