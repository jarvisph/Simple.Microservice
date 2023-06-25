using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Utils.Domain.Model
{

    public class DingdingModel
    {
        /// <summary>
        /// token
        /// </summary>
        public string Access_Token { get; set; } = string.Empty;
        /// <summary>
        /// 密钥
        /// </summary>
        public string Secret { get; set; } = string.Empty;

        /// <summary>
        /// 消息类型
        /// text\link\markdown\actionCard\feedCard
        /// </summary>
        public string MsgType { get; set; } = "text";
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; } = string.Empty;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// 显示图片
        /// </summary>
        public string PicUrl { get; set; } = string.Empty;
        /// <summary>
        /// 消息连接
        /// </summary>
        public string MessageUrl { get; set; } = string.Empty;
        /// <summary>
        /// 0：按钮竖直排列
        /// 1：按钮横向排列
        /// </summary>
        public int BtnOrientation { get; set; } = 0;
        /// <summary>
        /// 跳转按钮
        /// </summary>
        public string Btns { get; set; } = string.Empty;
        /// <summary>
        /// 多链接排版（feedCard类型）
        /// </summary>
        public string Links { get; set; } = string.Empty;

    }
}
