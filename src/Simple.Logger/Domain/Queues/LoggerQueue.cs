using Simple.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Logger.Domain.Queues
{
    public class LoggerQueue : IMessageQueue
    {
        public string AppKey { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int ErrorCount { get; set; }
    }
}
