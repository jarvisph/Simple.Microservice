using Simple.Utils.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Utils.Domain.Queues
{
    public static class DingdingQueue
    {
        private static Queue<DingdingModel> Queue = new Queue<DingdingModel>();
        public static void Send(DingdingModel message)
        {
            Queue.Enqueue(message);
        }
        public static void Consumer(Action<DingdingModel> action)
        {
            while (true)
            {
                if (Queue.Count > 0)
                {
                    DingdingModel msg = Queue.Dequeue();
                    if (msg == null) continue;
                    if (string.IsNullOrWhiteSpace(msg.Secret) || string.IsNullOrWhiteSpace(msg.Access_Token)) continue;
                    try
                    {
                        action(msg);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
