using Microsoft.AspNetCore.Mvc;
using Simple.Core.Domain.Enums;
using Simple.Core.Extensions;
using Simple.Core.Helper;
using Simple.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace Simple.Utils.Controllers
{
    /// <summary>
    /// 头像生成
    /// </summary>
    public class AvatarController : SimpleControllerBase
    {
        [Route("[controller]"), HttpGet]
        public FileContentResult Get([FromQuery] string content)
        {
            if (CheckHelper.IsChinese(content, out _))
            {
                if (content.Length > 2)
                {
                    content = content.Substring(content.Length - 2, 2);
                }
            }
            else
            {
                if (content.Length > 3)
                {
                    content = content[..3].ToUpper();
                }
            }
            int width = 200;
            int heigth = 200;
            Bitmap bitmap = new Bitmap(width, heigth);
            Graphics graphics = Graphics.FromImage(bitmap);
            //呈现的质量
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //背景色
            Brush brush = new SolidBrush(RandomHelper.RandomColor());
            graphics.FillEllipse(brush, new Rectangle(0, 0, width, heigth));
            //设置字体
            Font font = new Font("微软雅黑", 26, FontStyle.Regular);
            SizeF sizeF = graphics.MeasureString(content, font);
            graphics.DrawString(content, font, Brushes.White, (width - sizeF.Width) / 2, (heigth - sizeF.Height) / 2);
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            return File(ms.ToArray(), ContentType.PNG.GetDescription());
        }
    }
}
