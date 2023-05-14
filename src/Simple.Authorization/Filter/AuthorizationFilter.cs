using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Simple.Authorization.Auth;
using Simple.Authorization.Caching;
using Simple.Core.Authorization;
using Simple.Core.Domain.Enums;
using Simple.Core.Domain.Model;
using Simple.Core.Extensions;
using Simple.Web.Mvc;
using System.Reflection;

namespace Simple.Authorization.Filter
{
    /// <summary>
    /// 权限过滤器
    /// </summary>
    public class AuthorizationFilter : ActionFilterAttribute
    {
        private readonly IAdminCaching _adminAppCache;
        public AuthorizationFilter(IAdminCaching adminCaching)
        {
            _adminAppCache = adminCaching;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            MethodInfo action = context.GetActionMethodInfo();
            if (action.HasAttribute<AllowAnonymousAttribute>())
            {
                base.OnActionExecuting(context);
                context.HttpContext.SetItem(new AccountModel
                {
                    Type = OperateType.Admin
                });
            }
            else
            {
                string token = context.HttpContext.GetHeader("TOKEN");
                if (string.IsNullOrWhiteSpace(token)) throw new AuthorizationException();
                int adminId = _adminAppCache.GetTokenID(token);
                if (adminId == 0) throw new AuthorizationException();
                var admin = _adminAppCache.GetAdminInfo(adminId);
                if (admin == null) throw new AuthorizationException();
                if (!admin.IsAdmin)
                {
                    PermissionAttribute permission = action.GetAttribute<PermissionAttribute>();
                    if (permission != null)
                    {
                        if (!_adminAppCache.CheckPermission(admin.RoleID, permission.Permission))
                        {
                            throw new AuthorizationException(PermissionFinder.GetDescription(typeof(PermissionNames), permission.Permission));
                        }
                    }
                }
                context.HttpContext.SetItem(new AccountModel
                {
                    AccountID = adminId,
                    AccountName = admin.AdminName,
                    Type = OperateType.Admin
                });
                base.OnActionExecuting(context);
            }
        }
    }
}
