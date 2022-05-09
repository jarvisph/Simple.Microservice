using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.SignalR.Domain.Utils
{
    public static class SignalRConnection
    {
        public static Dictionary<string, HubConnection> _connection = new Dictionary<string, HubConnection>();
        public static HubConnection GetHubConnection(string appKey)
        {
            if (_connection.ContainsKey(appKey))
                return _connection[appKey];
            HubConnection connection = new HubConnectionBuilder()
                           .WithUrl("http://localhost:5000/hub", options =>
                           {
                               options.Headers = new Dictionary<string, string>
                              {
                                {"appkey",appKey}
                              };
                           })
                           .Build();
            //重连机制
            connection.Closed += async (error) =>
            {
                Console.WriteLine(error);
                Console.WriteLine("连接已关闭，正在尝试重连");
                _connection.Clear();
                await connection.StartAsync();
            };
            connection.StartAsync().ConfigureAwait(false);
            _connection.Add(appKey, connection);
            return connection;
        }
    }
}
