using Simple.Authorization.Caching;
using Simple.Authorization.Entity;
using Simple.Authorization.Interface;
using Simple.Core.Dapper;
using Simple.Core.Helper;
using Simple.Core.Logger;
using System.Data;

namespace Simple.Authorization.Services
{
    internal class RoleAppService : ServiceBase, IRoleAppService
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

        public IEnumerable<Role> GetRoles()
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                return db.GetAll<Role>(c => c.ID != 0).ToList();
            }
        }

        public bool DeleteRoleInfo(int roleId)
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                if (db.Any<Admin>(c => c.RoleID == roleId)) throw new MessageException("管理员中存在此角色");
                db.Delete<Role>(c => c.ID == roleId);
                _adminCaching.DeletePermission(roleId);
            }
            return Logger.Log($"删除角色");
        }

        public bool SaveRoleInfo(Role role)
        {
            if (!CheckHelper.CheckName(role.Name, out string message, 2, 16)) throw new MessageException(message);
            if (!CheckHelper.CheckName(role.Description, out message, 2, 64)) throw new MessageException(message);
            using (IDapperDatabase db = CreateDatabase(IsolationLevel.ReadUncommitted))
            {
                if (role.ID == 0)
                {
                    db.InsertIdentity(role);
                }
                else
                {
                    db.Update(role, c => c.ID == role.ID, c => c.Permission, c => c.Name, c => c.Description);
                }
                db.Collback(() =>
                {
                    //保存权限进缓存
                    _adminCaching.SavePermission(role.ID, role.Permissions);
                });
                db.Commit();
            }
            return Logger.Log($"保存角色信息/{role.ID}");
        }

        public Role GetRoleInfo(int roleId)
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                return db.FirstOrDefault<Role>(c => c.ID == roleId);
            }
        }
    }
}
