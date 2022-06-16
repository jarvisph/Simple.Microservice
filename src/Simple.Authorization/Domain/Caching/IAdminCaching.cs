using Simple.Authorization.Domain.Model.Admin;
using Simple.Core.Dependency;

namespace Simple.Authorization.Domain.Caching
{
    /// <summary>
    /// 管理员缓存相关#0
    /// </summary>
    public interface IAdminCaching : ISingletonDependency
    {
        /// <summary>
        /// 获取管理员权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        IEnumerable<string> GetPermission(int roleId);
        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="permissions"></param>
        void SavePermission(int roleId, IEnumerable<string> permissions);
        /// <summary>
        /// 删除角色权限
        /// </summary>
        /// <param name="roleId"></param>
        void DeletePermission(int roleId);
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="permission"></param>
        /// <returns></returns>
        bool CheckPermission(int roleId, string permission);
        /// <summary>
        /// 保存管理员信息
        /// </summary>
        /// <param name="admin"></param>
        void SaveAdminInfo(AdminRedis admin);
        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        AdminRedis GetAdminInfo(int adminId);
        /// <summary>
        /// 保存管理员token
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="token"></param>
        void SaveAdminToken(int adminId, string token);
        /// <summary>
        /// 检查管理员token
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        bool CheckToken(int adminId, string token);
        /// <summary>
        /// 移除token
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        bool RemoveToken(int adminId);

    }
}
