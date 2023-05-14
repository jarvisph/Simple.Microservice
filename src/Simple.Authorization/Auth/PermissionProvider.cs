using Simple.Core.Authorization;

namespace Simple.Authorization.Auth
{
    public class PermissionProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var auth = context.CreatePermission(PermissionNames.Authorization, "权限管理", new PermissionMeta() { { "icon", "am-icon-lock" } });
            auth.CreateChildPermission(PermissionNames.Authorization_Admin, "管理列表", new PermissionMeta() { { "path", "/auth/admin" } });
            auth.CreateChildPermission(PermissionNames.Authorization_Role, "权限分组", new PermissionMeta() { { "path", "/auth/role" } });
        }
    }
}
