using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Authorization.Model.Role;
using Simple.Core.Dependency;

namespace Simple.Authorization.Domain.Services
{
    public interface IRoleAppService : ISingletonDependency
    {
        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool SaveRoleInfo(RoleInput input);
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        bool DeleteRoleInfo(int roleId);
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permssions"></param>
        /// <returns></returns>
        bool Authorize(int roleId, string[] permssions);

    }
}
