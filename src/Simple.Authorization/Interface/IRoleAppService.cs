using Simple.Authorization.Entity;
using Simple.Core.Dependency;

namespace Simple.Authorization.Interface
{
    public interface IRoleAppService : ISingletonDependency
    {
        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        bool SaveRoleInfo(Role role);
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
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        IEnumerable<Role> GetRoles();
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Role GetRoleInfo(int roleId);
    }
}
