using RabbitMQ.Client.Events;
using Simple.Core.Dependency;
using Simple.RabbitMQ;
using Simple.SignalR.Domain.Services;
using Simple.SignalR.Queues;

namespace Simple.SignalR.Consumers
{
    [Consumer(ExchangeName.PushLog)]
    public class PushLogConsumer : RabbitConsumerBase<PushLogQueue>
    {
        private readonly IConnectionAppService _connectionAppService = IocCollection.Resolve<IConnectionAppService>();
        public override void Invoke(PushLogQueue message, object sender, BasicDeliverEventArgs args)
        {
            _connectionAppService.SavePushLog(message);
        }
    }
}
