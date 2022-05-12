using Simple.Core.Data.Schema;
using Simple.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Logger.Entity
{
    /// <summary>
    /// 应用
    /// </summary>
    [Table("log_Appsetting")]
    public class ApplicationSetting : IEntity
    {
        /// <summary>
        /// 秘钥
        /// </summary>
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
        /// 状态
        /// </summary>
        public UserStatus Status { get; set; }
    }
}
