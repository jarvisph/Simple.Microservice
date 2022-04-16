using Simple.Authorization.Model.Role;
using Simple.Core.Data.Schema;
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
    /// 角色表
    /// </summary>
    [Table("auth_Role")]
    public class Role : RoleBase, IEntity
    {
        [Column("RoleID"), DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public override int ID { get; set; }
        [Column("RoleName")]
        public override string Name { get; set; } = string.Empty;
        public override string Description { get; set; } = string.Empty;
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
