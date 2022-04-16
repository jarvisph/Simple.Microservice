using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Authorization.Model.Admin
{
    /// <summary>
    /// 管理员公共基类
    /// </summary>
    public abstract class AdminBase
    {
        /// <summary>
        /// 管理员ID
        /// </summary>
        public abstract int ID { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public abstract string AdminName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public abstract string NickName { get; set; }

    }
}
