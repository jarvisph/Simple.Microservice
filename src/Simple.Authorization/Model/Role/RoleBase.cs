using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Authorization.Model.Role
{
    public abstract class RoleBase
    {
        /// <summary>
        /// ID
        /// </summary>
        public abstract int ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public abstract string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public abstract string Description { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public abstract string Permission { get; set; }
    }
}
