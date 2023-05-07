using Microsoft.AspNetCore.Mvc;
using Simple.Authorization.DBContext;
using Simple.Authorization.Model;
using Simple.Core.Dependency;
using Simple.Core.Extensions;
using Simple.Web.Mvc;

namespace Simple.Authorization.Controllers
{
    /// <summary>
    /// 授权相关
    /// </summary>
    [Route("auth/[controller]/[action]")]
    public abstract class AuthControllerBase : SimpleControllerBase
    {
        public AuthControllerBase()
        {
            this.ADB = IocCollection.Resolve<AuthDbContext>();
        }
        public AuthDbContext ADB { get; private set; }

        public AccountModel Account => HttpContext.GetItem<AccountModel>();
    }
}
