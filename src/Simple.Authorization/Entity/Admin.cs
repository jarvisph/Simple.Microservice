using Simple.Authorization.Model.Admin;
using Simple.Core.Data.Schema;
using Simple.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Authorization.Entity
{
    /// <summary>
    /// 管理员表
    /// </summary>
    [Table("auth_Admin")]
    public class Admin : AdminBase, IEntity
    {
        [Column("AdminID"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int ID { get; set; }
        public override string AdminName { get; set; } = string.Empty;
        public override string NickName { get; set; } = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// 状态
        /// </summary>
        public UserStatus Status { get; set; }
        /// <summary>
        /// 最后一次登录IP
        /// </summary>
        public long LoginIP { get; set; }
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime LoginAt { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateAt { get; set; }

    }
}
