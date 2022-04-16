using Simple.Core.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Authorization.Domain.Auth
{
    internal class PermissionProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var auth = context.CreatePermission(PermissionNames.Authorization, "权限管理", PermissionType.Memu, new PermissionMeta() { { "href", "" }, { "icon", "" } });
            var admin = auth.CreateChildPermission(PermissionNames.Authorization_Admin, "管理员列表", PermissionType.Memu, new PermissionMeta() { { "href", "" }, { "icon", "" } });
            admin.CreateChildPermission(PermissionNames.Authorization_Admin_Create, "创建");
            admin.CreateChildPermission(PermissionNames.Authorization_Admin_Edit, "编辑");
            admin.CreateChildPermission(PermissionNames.Authorization_Admin_Delete, "删除");

            var role = auth.CreateChildPermission(PermissionNames.Authorization_Role, "角色列表", PermissionType.Memu, new PermissionMeta() { { "href", "" }, { "icon", "" } });
            role.CreateChildPermission(PermissionNames.Authorization_Role_Create, "创建");
            role.CreateChildPermission(PermissionNames.Authorization_Role_Edit, "编辑");
            role.CreateChildPermission(PermissionNames.Authorization_Role_Delete, "删除");
        }
    }
}
