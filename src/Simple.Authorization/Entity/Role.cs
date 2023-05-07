using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Simple.Core.Data.Schema;

namespace Simple.Authorization.Entity
{
    ///<summary>
    ///角色表
    ///</summary>
    [Table("auth_Role")]
    public class Role : IEntity
    {

        #region ======== 构造函数 ===========
        public Role() { }




        #endregion

        #region ======== 数据库字段 ========

        ///<summary>
        ///[ID]角色ID
        ///</summary>
        [Column("RoleID"), DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int ID { get; set; }


        ///<summary>
        ///[Name]角色名称
        ///</summary>
        [Column("RoleName")]
        public string Name { get; set; } = string.Empty;


        ///<summary>
        ///描述
        ///</summary>
        [Column("Description")]
        public string Description { get; set; } = string.Empty;


        ///<summary>
        ///权限
        ///</summary>
        [Column("Permission")]
        public string Permission { get; set; } = string.Empty;

        #endregion


        #region ======== 扩展方法 =========

        [NotMapped]
        public string[] Permissions
        {
            get
            {
                return this.Permission.Split(',');
            }
        }
        #endregion

    }

}
