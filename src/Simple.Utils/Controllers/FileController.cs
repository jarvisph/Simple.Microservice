using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple.Core.Extensions;
using Simple.Core.Logger;
using Simple.Web.Mvc;

namespace Simple.Utils.Controllers
{
    [Route("[controller]/[action]")]
    public class FileController : SimpleControllerBase
    {
        /// <summary>
        /// root
        /// </summary>
        public static string RootPath = Directory.GetCurrentDirectory() + "/wwwroot/";

        /// <summary>
        /// 允许上传的文件类型
        /// </summary>
        public static string[] FileType = { "jpg", "JPG", "png", "PNG", "jpge", "JPGE", "gif", "GIF" };

        public ActionResult Upload()
        {
            try
            {

                string path = DateTime.Now.ToString("yyMMdd");
                if (HttpContext.Request.Form.Files.Count == 0)
                {
                    return ErrorResult("请上传文件");
                }
                else if (HttpContext.Request.Form.Files.Count == 1)
                {

                    string message = ImageUpload(HttpContext.Request.Form.Files[0], path);
                    return JsonResult(message);
                }
                else
                {
                    IEnumerable<string> images = ImageUpload(HttpContext.Request.Form.Files, path);
                    return JsonResult(images);
                }
            }
            catch (Exception ex)
            {
                return ErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 批量上传
        /// </summary>
        /// <param name="files"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private IEnumerable<string> ImageUpload(IFormFileCollection files, string path)
        {
            foreach (IFormFile file in files)
            {
                yield return this.ImageUpload(file, path);
            }
        }

        /// <summary>
        /// 生成图片
        /// </summary>
        /// <param name="file"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private string ImageUpload(IFormFile file, string path)
        {
            path += "/";
            string rootPath = RootPath + path;
            byte[] stream = file.GetData();
            string fileExt = file.GetFileExt();
            if (string.IsNullOrWhiteSpace(fileExt)) throw new MessageException("此文件来自外星");
            if (!FileType.Contains(fileExt)) throw new MessageException($"不支持“{fileExt}”类型");
            //生成随机文件名
            string fileName = Guid.NewGuid().ToString("N");
            //查询目录是否存在
            if (!Directory.Exists(rootPath)) Directory.CreateDirectory(rootPath);
            //文件名+后缀
            string fileNameExt = fileName + "." + fileExt;
            //拼接绝对路径
            string filePath = rootPath + fileNameExt;
            System.IO.File.WriteAllBytes(filePath, stream);
            return string.Format("{0}", "/" + path + fileNameExt);
        }
    }
}
