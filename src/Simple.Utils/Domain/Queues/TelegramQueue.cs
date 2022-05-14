using Simple.Utils.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Utils.Domain.Queues
{
    public static class TelegramQueue
    {
        private static Queue<TelegramModel> Queue = new Queue<TelegramModel>();
        public static void Send(TelegramModel message)
        {
            Queue.Enqueue(message);
        }
        public static void Consumer(Action<TelegramModel> action)
        {
            while (true)
            {
                if (Queue.Count > 0)
                {
                    TelegramModel msg = Queue.Dequeue();
                    if (msg == null) continue;
                    if (string.IsNullOrWhiteSpace(msg.Token) || string.IsNullOrWhiteSpace(msg.Message) || string.IsNullOrWhiteSpace(msg.ChatID)) continue;
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
