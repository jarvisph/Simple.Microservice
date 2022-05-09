using Simple.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.SignalR.Queues
{
    /// <summary>
    /// 推送消息体
    /// </summary>
    [Producer(ExchangeName.PushMessage)]
    public class PushMessageQueue : IMessageQueue
    {
        public PushMessageQueue()
        {

        }
        public PushMessageQueue(string appKey, string chanenl, string message)
        {
            this.AppKey = appKey;
            this.Chanenl = chanenl;
            this.Message = message;
        }
        /// <summary>
        /// 频道
        /// </summary>
        public string Chanenl { get; set; } = string.Empty;
        public string AppKey { get; set; } = string.Empty;
        /// <summary>
        /// 消息体
        /// </summary>
        public string Message { get; set; } = string.Empty;
        public int ErrorCount { get; set; }
    }
}
