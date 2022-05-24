using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Simple.Authorization.Domain.Auth;
using Simple.Authorization.Domain.Caching;
using Simple.Authorization.Domain.Model.Admin;
using Simple.Core.Authorization;
using Simple.Core.Extensions;
using Simple.Web.Jwt;
using Simple.Web.Mvc;
using System.Reflection;

namespace Simple.Authorization.Domain.Filter
{
    /// <summary>
    /// 权限过滤器
    /// </summary>
    public class AuthorizationFilter : ActionFilterAttribute
    {
        private readonly JWTOption _options;
        private readonly IAdminCaching _adminCaching;
        public AuthorizationFilter(JWTOption options, IAdminCaching adminCaching)
        {
            _options = options;
            _adminCaching = adminCaching;
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
            }
            else
            {
                string token = context.HttpContext.GetHeader(_options.TokenName);
                if (string.IsNullOrWhiteSpace(token)) throw new AuthorizationException();
                int adminId = context.HttpContext.GetClaimValue("ID").GetValue<int>();
                if (adminId == 0) throw new AuthorizationException();
                if (!_adminCaching.CheckToken(adminId, token)) throw new AuthorizationException();
                AdminRedis admin = _adminCaching.GetAdminInfo(adminId);
                if (admin == null) throw new AuthorizationException();
                if (!admin.IsAdmin)
                {
                    PermissionAttribute permission = action.GetAttribute<PermissionAttribute>();
                    if (permission != null)
                    {
                        if (!_adminCaching.CheckPermission(admin.RoleID, permission.Permission))
                        {
                            throw new AuthorizationException(PermissionFinder.GetDescription(typeof(PermissionNames), permission.Permission));
                        }
                    }
                }
                base.OnActionExecuting(context);
            }
        }
    }
}
