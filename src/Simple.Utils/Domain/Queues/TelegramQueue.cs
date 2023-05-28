using Simple.Utils.Domain.Model;

namespace Simple.Utils.Domain.Queues
{
    public static class TelegramQueue
    {
        public static Queue<TelegramModel> Queue = new Queue<TelegramModel>();
        public static void Send(TelegramModel message)
        {
            Queue.Enqueue(message);
        }
    }
}
