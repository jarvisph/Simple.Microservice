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
    /// 钉钉相关
    /// </summary>
    [Route("[controller]/[action]")]
    public class DingdingController : SimpleControllerBase
    {
        public ActionResult Send(DingdingModel model)
        {
            DingdingQueue.Send(model);
            return Ok("success");
        }
    }
}
