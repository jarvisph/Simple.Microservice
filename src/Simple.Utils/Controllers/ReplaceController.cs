//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Simple.Core.Domain.Enums;
//using Simple.Core.Extensions;
//using Simple.Core.Logger;
//using Simple.Web.Mvc;
//using System.Text;

//namespace Simple.Utils.Controllers
//{
//    /// <summary>
//    /// 替换文本
//    /// </summary>
//    public class ReplaceController : SimpleControllerBase
//    {
//        /// <summary>
//        /// 替换文件中的文字
//        /// </summary>
//        /// <param name="source"></param>
//        /// <param name="replace">replace</param>
//        /// <returns></returns>
//        [HttpPost, AllowAnonymous]
//        public new FileContentResult File([FromForm] string source, [FromForm] string replace)
//        {
//            if (HttpContext.Request.Form.Files.Count == 0) throw new MessageException("请上传文件");
//            IFormFile file = HttpContext.Request.Form.Files[0];
//            string text = Encoding.UTF8.GetString(file.GetData());
//            string fileExt = file.GetFileExt();
//            ContentType contentType = fileExt.ToUpper().ToEnum<ContentType>();
//            text = text.Replace(source, replace);
//            return File(Encoding.UTF8.GetBytes(text), contentType.GetDescription(), file.FileName);
//        }
//    }
//}
