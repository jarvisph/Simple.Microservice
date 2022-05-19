using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple.Core.Domain.Enums;
using Simple.Core.Extensions;
using Simple.Core.Languages;
using Simple.Core.Logger;
using Simple.Translate.Domain.DBContext;
using Simple.Translate.Domain.Services;
using Simple.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Translate.Controllers
{
    /// <summary>
    /// 翻译
    /// </summary>
    [Route("[controller]/[action]")]
    public class TranslateController : SimpleControllerBase
    {
        private readonly ITranslateAppService _translateAppService;
        private readonly TranslateDbContext _context;
        public TranslateController(ITranslateAppService translateAppService, TranslateDbContext context)
        {
            _translateAppService = translateAppService;
            _context = context;
        }
        /// <summary>
        /// 文本翻译
        /// </summary>
        /// <param name="language"></param>
        /// <param name="content"></param>
        /// <param name="channel"></param>
        /// <returns>lanauge=chn&content=~你好~&channel=000000</returns>
        [HttpGet, AllowAnonymous]
        public ActionResult Text([FromQuery] LanguageType language, [FromQuery] string content, [FromQuery] string channel)
        {
            return JsonResult(_translateAppService.Translate(content, channel.GetValue<Guid>(), language));
        }

        /// <summary>
        /// 文件翻译
        /// </summary>
        /// <param name="language"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public FileContentResult File([FromForm] LanguageType language, [FromForm] string channel)
        {
            if (HttpContext.Request.Form.Files.Count == 0) throw new MessageException("请上传文件");
            IFormFile file = HttpContext.Request.Form.Files[0];
            byte[] data = file.GetData();
            string fileExt = file.GetFileExt();
            ContentType contentType = fileExt.ToUpper().ToEnum<ContentType>();
            var content = _translateAppService.Translate(data, channel.GetValue<Guid>(), language);
            return File(content, contentType.GetDescription(), file.FileName);
        }

        /// <summary>
        /// 频道列表
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="name"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost, ActionName("channel/list")]
        public ActionResult Channel_List([FromForm] Guid? channel, [FromForm] string name, [FromForm] UserStatus? status)
        {
            var query = _context.TranslateChannel.Where(channel, c => c.Channel == channel)
                                                 .Where(name, c => c.Name == name)
                                                 .Where(status, c => c.Status == status);
            return PageResult(query.OrderByDescending(c => c.CreateAt), c => new
            {
                c.Channel,
                c.CreateAt,
                c.Name,
                c.Status
            });
        }
        /// <summary>
        /// 创建频道
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost, ActionName("channel/create")]
        public ActionResult Channel_Create([FromForm] string name)
        {
            return JsonResult(_translateAppService.CreateChannel(name));
        }

        /// <summary>
        /// 内容列表
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        [HttpPost, ActionName("content/list")]
        public ActionResult Content_List([FromForm] Guid? channel)
        {
            var query = _context.TranslateContent.Where(channel, c => c.Channel == channel);
            return PageResult(query.OrderByDescending(c => c.Word), c => new
            {
                c.Word,
                c.Channel,
                c.Translate
            });
        }
        /// <summary>
        /// 保存翻译内容
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="word"></param>
        /// <param name="language"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost, ActionName("content/save")]
        public ActionResult Content_Save([FromForm] Guid channel, [FromForm] long word, [FromForm] LanguageType language, [FromForm] string content)
        {
            return JsonResult(_translateAppService.SaveContent(channel, word, language, content));
        }
    }
}
