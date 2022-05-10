using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simple.Core.Authorization;
using Simple.Core.Extensions;
using Simple.Core.Logger;
using Simple.RabbitMQ;
using Simple.SignalR.Domain.DbContext;
using Simple.SignalR.Domain.Services;
using Simple.SignalR.Queues;
using Simple.Web.Mvc;

namespace Simple.SignalR.Controllers
{
    public class SignalRController : SimpleControllerBase
    {
        private readonly SignalRDbContext _context;
        private readonly ISignalRAppService _connectionAppService;
        public SignalRController(SignalRDbContext context, ISignalRAppService connectionAppService)
        {
            this._context = context;
            this._connectionAppService = connectionAppService;
        }
        /// <summary>
        /// 发送接口
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        /// <exception cref="AuthorizationException"></exception>
        [Route("send"), AllowAnonymous, HttpPost]
        public ActionResult Send(PushMessageQueue queue)
        {
            if (string.IsNullOrWhiteSpace(queue.AppKey)) throw new AuthorizationException();
            if (string.IsNullOrWhiteSpace(queue.Chanenl)) throw new MessageException("频道不能为空");
            if (!_connectionAppService.CheckAppKey(queue.AppKey.GetValue<Guid>())) throw new AuthorizationException();
            queue.Send();
            return Ok("success");
        }
        /// <summary>
        /// 应用程序
        /// </summary>
        /// <returns></returns>
        [Route("application/list"), HttpGet]
        public ActionResult ApplicationSettingList([FromForm] Guid? appKey)
        {
            var query = _context.Application.Where(appKey, c => c.AppKey == appKey);
            return PageResult(query.OrderByDescending(c => c.CreateAt), t => new
            {
                t.AppKey,
                t.CreateAt,
                t.Status,
                t.Name
            });
        }
        /// <summary>
        /// 创建一个应用
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("application/create")]
        public ActionResult CreateApplicationSetting([FromForm] string name)
        {
            return JsonResult(_connectionAppService.CreateApplicationInfo(name));
        }
        /// <summary>
        /// 推送日志
        /// </summary>
        /// <returns></returns>
        [Route("push/list"), HttpGet]
        public ActionResult PushLog([FromForm] Guid? appKey, [FromForm] string connectionId)
        {
            var query = _context.PushLog.Where(appKey, t => t.AppKey == appKey)
                                        .Where(connectionId, t => t.ConnectionID == connectionId);
            return PageResult(query.OrderByDescending(c => c.CreateAt), t => new
            {
                t.AppKey,
                t.CreateAt,
                t.Channel,
                t.ConnectionID,
            });
        }

        /// <summary>
        /// 连接日志
        /// </summary>
        /// <returns></returns>
        [Route("connection/list"), HttpGet]
        public ActionResult ConnectionList([FromForm] string connectionId, [FromForm] Guid? appkey)
        {
            var query = _context.Connection.Where(connectionId, c => c.ConnectionID == connectionId)
                                           .Where(appkey, c => c.AppKey == appkey);
            return PageResult(query.OrderByDescending(c => c.CreateAt), t => new
            {
                t.IP,
                t.ConnectionID,
                t.CreateAt,
                t.DisconnectAt,
                t.IsOnline
            });
        }
    }
}
