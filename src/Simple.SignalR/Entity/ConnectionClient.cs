using Simple.Core.Data.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.SignalR.Entity
{
    /// <summary>
    /// 客户端连接表
    /// </summary>
    [Table("hub_Connection")]
    public class ConnectionClient : IEntity
    {
        /// <summary>
        /// 连接ID
        /// </summary>
        [Key]
        public string ConnectionID { get; set; } = string.Empty;
        /// <summary>
        /// 连接时IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 连接内容
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// appkey
        /// </summary>
        public Guid AppKey { get; set; } 
        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline { get; set; }
        /// <summary>
        /// 连接时间
        /// </summary>
        public DateTime CreateAt { get; set; }
        /// <summary>
        /// 断开时间
        /// </summary>
        public DateTime DisconnectAt { get; set; }


    }
}
