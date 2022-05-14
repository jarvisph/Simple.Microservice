using Microsoft.AspNetCore.Mvc;
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
        public ActionResult Send(TelegramModel model)
        {
            TelegramQueue.Send(model);
            return Ok("sccuess");
        }
    }
}
