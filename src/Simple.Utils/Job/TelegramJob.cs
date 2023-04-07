using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Simple.Core.Helper;
using Simple.Core.Jobs;
using Simple.Utils.Domain.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Utils.Consumers
{
    public class TelegramJob : JobServiceBase
    {
        public override int Time => throw new NotImplementedException();

        public override int Invoke()
        {
            TelegramQueue.Consumer(message =>
            {
                string api = "https://api.telegram.org/bot" + message.Token;
                string json = JsonConvert.SerializeObject(new { chat_id = message.ChatID, text = message.Message, parse_mode = "HTML" });
                string response = string.Empty;
                try
                {
                    response = NetHelper.Post(api, json, new Dictionary<string, string> { });
                    JObject info = JObject.Parse(response);
                    bool success = info["ok"]?.Value<bool>() ?? false;
                    Console.WriteLine($"[{DateTime.Now}]推送状态：{success}");
                }
                catch (Exception ex)
                {
                    ConsoleHelper.WriteLine($"[{DateTime.Now}]发送失败\n{response ?? ex.Message}", ConsoleColor.Red);
                }
            });
            return -0;
        }
    }
}
