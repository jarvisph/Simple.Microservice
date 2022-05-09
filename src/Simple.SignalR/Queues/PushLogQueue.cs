using Microsoft.AspNetCore.Http;
using Simple.Core.Extensions;
using Simple.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.SignalR.Queues
{
    [Producer(ExchangeName.PushLog)]
    public class PushLogQueue : IMessageQueue
    {
        public PushLogQueue()
        {

        }
        public PushLogQueue(string connectionId, string chanenl, string message, string appkey)
        {
            this.ConnectionId = connectionId;
            this.Channel = chanenl;
            this.AppKey = appkey.GetValue<Guid>();
            this.Message = message;
            this.CreateAt = DateTime.Now;
        }
        /// <summary>
        /// 频道
        /// </summary>
        public string Channel { get; set; } = string.Empty;
        public Guid AppKey { get; set; }
        public string ConnectionId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public int ErrorCount { get; set; }
    }
}
