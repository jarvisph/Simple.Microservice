using Simple.Authorization.Entity.Model.Admin;
using Simple.Core.Data.Schema;
using Simple.Core.Domain.Enums;
using Simple.Core.Mapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Authorization.Entity.DB
{
    /// <summary>
    /// 管理员表
    /// </summary>
    [Table("auth_Admin")]
    public class Admin : IEntity
    {
        [Column("AdminID"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 管理员名称
        /// </summary>
        public string AdminName { get; set; } = string.Empty;
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; } = string.Empty;
        /// <summary>
        /// 头像
        /// </summary>
        public string Face { get; set; } = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }
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
        public long LoginAt { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateAt { get; set; }
        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsAdmin { get; set; }
        public static implicit operator AdminRedis(Admin admin)
        {
            return admin.Map<AdminRedis>();
        }
    }
}
