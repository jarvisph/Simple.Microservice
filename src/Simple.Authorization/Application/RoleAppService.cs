using Simple.Authorization.Domain.Services;
using Simple.Authorization.Entity;
using Simple.Authorization.Domain.Model.Role;
using Simple.Core.Authorization;
using Simple.Core.Dapper;
using Simple.Core.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Authorization.Application
{
    internal class RoleAppService : AuthorizationAppServiceBase, IRoleAppService
    {
        public bool Authorize(int roleId, string[] permssions)
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                var role = db.FirstOrDefault<Role>(c => c.ID == roleId);
                if (role == null) throw new MessageException("角色不存在");
                role.Permission = string.Join(",", permssions);
                db.Update(role, c => c.ID == roleId, c => c.Permission);
            }
            return Logger.Log($"授权");
        }

        public bool DeleteRoleInfo(int roleId)
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                db.Delete<Role>(c => c.ID == roleId);
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
