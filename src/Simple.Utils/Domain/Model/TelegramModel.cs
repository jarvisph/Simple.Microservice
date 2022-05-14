using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Utils.Domain.Model
{
    public class TelegramModel
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// 聊天ID
        /// </summary>
        public string ChatID { get; set; } = string.Empty;
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}
