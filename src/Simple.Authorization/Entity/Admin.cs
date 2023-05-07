using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Simple.Core.Data.Schema;
using Simple.Core.Domain.Enums;

namespace Simple.Authorization.Entity
{
    ///<summary>
    ///管理表
    ///</summary>
    [Table("auth_Admin")]
    public class Admin : IEntity
    {

        #region ======== 构造函数 ===========
        public Admin() { }




        #endregion

        #region ======== 数据库字段 ========

        ///<summary>
        ///[ID]管理员ID
        ///</summary>
        [Column("AdminID"), DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int ID { get; set; }


        ///<summary>
        ///角色ID
        ///</summary>
        [Column("RoleID")]
        public int RoleID { get; set; }


        ///<summary>
        ///管理账号
        ///</summary>
        [Column("AdminName")]
        public string AdminName { get; set; } = string.Empty;


        ///<summary>
        ///昵称
        ///</summary>
        [Column("NickName")]
        public string NickName { get; set; } = string.Empty;


        ///<summary>
        ///头像
        ///</summary>
        [Column("Face")]
        public string Face { get; set; } = string.Empty;


        ///<summary>
        ///密码
        ///</summary>
        [Column("Password")]
        public string Password { get; set; } = string.Empty;


        ///<summary>
        ///状态
        ///</summary>
        [Column("Status")]
        public UserStatus Status { get; set; }


        ///<summary>
        ///登录IP
        ///</summary>
        [Column("LoginIP")]
        public string LoginIP { get; set; } = string.Empty;


        ///<summary>
        ///登录时间
        ///</summary>
        [Column("LoginAt")]
        public long LoginAt { get; set; }


        ///<summary>
        ///创建时间
        ///</summary>
        [Column("CreateAt")]
        public long CreateAt { get; set; }


        ///<summary>
        ///是否超级管理员
        ///</summary>
        [Column("IsAdmin")]
        public bool IsAdmin { get; set; }

        #endregion


        #region ======== 扩展方法 =========

        #endregion

    }

}
