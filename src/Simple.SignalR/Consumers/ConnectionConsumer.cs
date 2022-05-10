using RabbitMQ.Client.Events;
using Simple.Core.Dependency;
using Simple.RabbitMQ;
using Simple.SignalR.Domain.Services;
using Simple.SignalR.Queues;

namespace Simple.SignalR.Consumers
{
    [Consumer(ExchangeName.ConnectionHub)]
    public class ConnectionConsumer : RabbitConsumerBase<ConnectionQueue>
    {
        private readonly ISignalRAppService _connectionAppService = IocCollection.Resolve<ISignalRAppService>();
        public override void Invoke(ConnectionQueue message, object sender, BasicDeliverEventArgs args)
        {
            _connectionAppService.SaveConnectionInfo(message);
        }
    }
}
