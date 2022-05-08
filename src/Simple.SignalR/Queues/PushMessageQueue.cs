using Microsoft.AspNetCore.Http;
using Simple.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.SignalR.Queues
{
    public class PushMessageQueue : IMessageQueue
    {
        public PushMessageQueue(string connectionId, string chanenl, string message, string appkey)
        {
            this.ConnectionId = connectionId;
            this.Channel = chanenl;
            this.Key = appkey;
            this.Message = message;
            this.CreateAt = DateTime.Now;
        }
        /// <summary>
        /// 频道
        /// </summary>
        public string Channel { get; set; }
        public string Key { get; set; }
        public string ConnectionId { get; set; }
        public string Message { get; set; }
        public DateTime CreateAt { get; set; }
        public int ErrorCount { get; set; }
    }
}
