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
    /// 推送日志
    /// </summary>
    [Table("hub_PushLog")]
    public class PushLog : IEntity
    {
        [Column("LogID"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 频道
        /// </summary>
        public string Channel { get; set; } = string.Empty;
        /// <summary>
        /// 应用key
        /// </summary>
        public Guid AppKey { get; set; }
        /// <summary>
        /// 连接ID
        /// </summary>
        public string ConnectionID { get; set; } = string.Empty;
        /// <summary>
        /// 消息体
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// 推送时间
        /// </summary>
        public DateTime CreateAt { get; set; }
    }
}
