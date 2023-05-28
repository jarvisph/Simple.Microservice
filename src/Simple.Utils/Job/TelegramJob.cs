using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Simple.Core.Helper;
using Simple.Core.Jobs;
using Simple.Utils.Domain.Model;
using Simple.Utils.Domain.Queues;

namespace Simple.Utils.Consumers
{
    public class TelegramJob : JobServiceBase
    {
        public override int Time => 1000;

        public override void Invoke()
        {
            if (TelegramQueue.Queue.Count > 0)
            {
                TelegramModel message = TelegramQueue.Queue.Dequeue();
                Console.WriteLine(JsonConvert.SerializeObject(message));
                string url = $"https://api.telegram.org/{message.Token}/sendMessage";
                string json = JsonConvert.SerializeObject(new { chat_id = message.ChatID, text = message.Message, parse_mode = "HTML" });
                try
                {
                    string response = NetHelper.Post(url, json, new Dictionary<string, string> { });
                    JObject info = JObject.Parse(response);
                    bool success = info["ok"]?.Value<bool>() ?? false;
                    Console.WriteLine($"[{DateTime.Now}]推送状态：{success}");
                }
                catch (Exception ex)
                {
                    ConsoleHelper.WriteLine($"[{DateTime.Now}]发送失败\n{ex.Message}", ConsoleColor.Red);
                }
            }

        }
    }
}
