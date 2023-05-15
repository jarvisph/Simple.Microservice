using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Simple.Core.Helper;
using Simple.Utils.Domain.Model;
using Simple.Utils.Domain.Queues;
using Simple.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Utils.Controllers
{
    /// <summary>
    /// 纸飞机相关
    /// </summary>
    [Route("[controller]/[action]")]
    public class TelegramController : SimpleControllerBase
    {
        public ActionResult Send([FromForm] string message, [FromForm] string token, [FromForm] string chatId)
        {
            string url = $"https://api.telegram.org/{token}/sendMessage";
            string json = JsonConvert.SerializeObject(new { chat_id = chatId, text = message, parse_mode = "HTML" });
            string response = string.Empty;
            try
            {
                response = NetHelper.Post(url, json, new Dictionary<string, string> { });
                JObject info = JObject.Parse(response);
                bool success = info["ok"]?.Value<bool>() ?? false;
                Console.WriteLine($"[{DateTime.Now}]推送状态：{success}");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteLine($"[{DateTime.Now}]发送失败\n{response ?? ex.Message}", ConsoleColor.Red);
            }
            return Ok(response);
        }
    }
}
