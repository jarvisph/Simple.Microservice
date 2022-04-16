using Microsoft.AspNetCore.Mvc.Filters;
using Simple.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Simple.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Simple.Core.Authorization;

namespace Simple.Authorization.Domain.Filter
{
    /// <summary>
    /// 权限过滤器
    /// </summary>
    public class AuthorizationFilter : ActionFilterAttribute
    {
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
                string token = context.HttpContext.GetHeader("Authorization");
                if (string.IsNullOrWhiteSpace(token))
                    throw new AuthorizationException();

            }
        }
    }
}
