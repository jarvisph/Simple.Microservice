using Simple.Utils.Domain.Model;

namespace Simple.Utils.Domain.Queues
{
    public static class DingdingQueue
    {
        public static Queue<DingdingModel> Queue = new Queue<DingdingModel>();
        public static void Send(DingdingModel message)
        {
            Queue.Enqueue(message);
        }
    }
}
