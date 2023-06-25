using Microsoft.AspNetCore.Mvc;
using Simple.Utils.Domain.Model;
using Simple.Utils.Domain.Queues;
using Simple.Web.Mvc;

namespace Simple.Utils.Controllers
{
    /// <summary>
    /// 钉钉相关
    /// </summary>
    [Route("[controller]/[action]")]
    public class DingdingController : SimpleControllerBase
    {
        public ActionResult Send([FromForm] string content, [FromForm] string title, [FromForm] string token, [FromForm] string secret)
        {
            DingdingQueue.Send(new DingdingModel()
            {
                Access_Token = token,
                Secret = secret,
                Text = content,
                Title = title,
            });
            return Ok("success");
        }
    }
}
