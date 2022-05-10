using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client.Events;
using Simple.RabbitMQ;
using Simple.SignalR.Domain.Utils;
using Simple.SignalR.Queues;

namespace Simple.SignalR.Consumers
{
    [Consumer(ExchangeName.PushMessage)]
    public class PushMessageConsumer : RabbitConsumerBase<PushMessageQueue>
    {
        public override void Invoke(PushMessageQueue message, object sender, BasicDeliverEventArgs args)
        {
            SignalRConnection.Send(message);
        }
    }
}
