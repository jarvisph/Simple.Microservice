using Simple.Core.Data.Schema;
using Simple.Core.Domain.Enums;
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
    /// 应用配置
    /// </summary>
    [Table("hub_Application")]
    public class ApplicationSetting : IEntity
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        [Key]
        public Guid AppKey { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        [Column("AppName")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateAt { get; set; }
        /// <summary>
        /// 应用状态
        /// </summary>
        public UserStatus Status { get; set; }
    }
}
