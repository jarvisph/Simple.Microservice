using Microsoft.AspNetCore.SignalR.Client;
using Simple.Core.Localization;
using Simple.SignalR.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.SignalR.Domain.Utils
{
    public static class SignalRConnection
    {
        private static Dictionary<string, HubConnection> _connection = new Dictionary<string, HubConnection>();
        public static void Send(PushMessageQueue message)
        {
            HubConnection connection;
            if (_connection.ContainsKey(message.AppKey))
            {
                connection = _connection[message.AppKey];
            }
            else
            {
                string connnection = AppsettingConfig.GetConnectionString("SignalRConnection");
                connection = new HubConnectionBuilder()
                         .WithUrl($"{connnection}/hub", options =>
                         {
                             options.Headers = new Dictionary<string, string>
                            {
                                {"appkey",message.AppKey}
                            };
                         })
                         .Build();
                _connection.Add(message.AppKey, connection);
            }
            //重连机制
            connection.Closed += async (error) =>
            {
                Console.WriteLine(error);
                Console.WriteLine("连接已关闭，正在尝试重连");
                await connection.StartAsync();
            };
            connection.StartAsync().ConfigureAwait(false);
            connection.InvokeAsync("SendMessage", message.Chanenl, message.Message).ConfigureAwait(false);
        }
    }
}
