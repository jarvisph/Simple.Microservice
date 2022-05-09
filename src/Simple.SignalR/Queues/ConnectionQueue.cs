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
    [Producer(ExchangeName.ConnectionHub)]
    public class ConnectionQueue : IMessageQueue
    {
        public ConnectionQueue()
        {

        }
        public ConnectionQueue(string connectionId, HttpContext? context)
        {
            this.ConnectionId = connectionId;
            this.AppKey = context.GetHeader("appkey").ToString().GetValue<Guid>();
            this.IP = context.GetIp();
            this.Content = context.GetRequestInfo();
            this.CreateAt = DateTime.Now;
        }
        /// <summary>
        /// 连接ID
        /// </summary>
        public string ConnectionId { get; set; }
        /// <summary>
        /// 连接IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 连接时间
        /// </summary>
        public DateTime CreateAt { get; set; }
        /// <summary>
        /// 连接Key
        /// </summary>
        public Guid AppKey { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        public int ErrorCount { get; set; }
    }
}
