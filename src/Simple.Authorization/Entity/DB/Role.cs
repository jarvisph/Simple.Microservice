using Simple.Authorization.Entity.Model.Role;
using Simple.Core.Data.Schema;
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
    /// 角色表
    /// </summary>
    [Table("auth_Role")]
    public class Role : RoleBase, IEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Column("RoleID"), DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public override int ID { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        [Column("RoleName")]
        public override string Name { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public override string Description { get; set; } = string.Empty;
        /// <summary>
        /// 权限
        /// </summary>
        public override string Permission { get; set; } = string.Empty;

        [NotMapped]
        public string[] Permissions
        {
            get
            {
                return Permission.Split(',').ToArray();
            }
        }
    }
}
