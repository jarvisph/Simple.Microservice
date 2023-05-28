using Microsoft.AspNetCore.Mvc;
using Simple.Utils.Domain.Model;
using Simple.Utils.Domain.Queues;
using Simple.Web.Mvc;

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
            TelegramQueue.Send(new TelegramModel { Message = message, Token = token, ChatID = chatId });
            return Ok("success");
        }
    }
}
