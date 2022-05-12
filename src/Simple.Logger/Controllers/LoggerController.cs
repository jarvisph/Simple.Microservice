using Microsoft.AspNetCore.Mvc;
using Simple.Core.Extensions;
using Simple.Logger.Domain.Queues;
using Simple.Logger.Domain.Services;
using Simple.RabbitMQ;
using Simple.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Core.Authorization;

namespace Simple.Logger.Controllers
{
    public class LoggerController : SimpleControllerBase
    {
        private readonly ILoggerAppService _loggerAppService;
        public LoggerController(ILoggerAppService loggerAppService)
        {
            _loggerAppService = loggerAppService;
        }

        /// <summary>
        /// 创建一个应用
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("application/create")]
        public ActionResult CreateApplicationSetting([FromForm] string name)
        {
            return JsonResult(_loggerAppService.CreateApplicationInfo(name));
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        /// <exception cref="AuthorizationException"></exception>
        [Route("log")]
        public ActionResult Log(LoggerQueue queue)
        {
            if (!_loggerAppService.CheckAppKey(queue.AppKey.GetValue<Guid>())) throw new AuthorizationException();
            queue.Send();
            return Ok("success");
        }
    }
}
