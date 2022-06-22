using Simple.Authorization.Application.Caching;
using Simple.Authorization.Entity.DB;
using Simple.Authorization.Entity.Model.Role;
using Simple.Core.Dapper;
using Simple.Core.Logger;

namespace Simple.Authorization.Application.Services
{
    internal class RoleAppService : AuthorizationAppServiceBase, IRoleAppService
    {
        private readonly IAdminCaching _adminCaching;
        public RoleAppService(IAdminCaching adminCaching)
        {
            _adminCaching = adminCaching;
        }
        public bool Authorize(int roleId, string[] permissions)
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                var role = db.FirstOrDefault<Role>(c => c.ID == roleId);
                if (role == null) throw new MessageException("角色不存在");
                role.Permission = string.Join(",", permissions);
                db.Update(role, c => c.ID == roleId, c => c.Permission);
                _adminCaching.SavePermission(roleId, permissions);

            }
            return Logger.Log($"角色授权");
        }

        public bool DeleteRoleInfo(int roleId)
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                if (db.Any<Admin>(c => c.RoleID == roleId))
                {
                    throw new MessageException("管理员中存在此角色");
                }
                db.Delete<Role>(c => c.ID == roleId);
                _adminCaching.DeletePermission(roleId);
            }
            return Logger.Log($"删除角色");
        }

        public bool SaveRoleInfo(RoleInput input)
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                if (input.ID == 0)
                {

                }
                else
                {

                }
            }
            return Logger.Log($"保存角色信息");
        }
    }
}
