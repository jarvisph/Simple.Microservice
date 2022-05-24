using Microsoft.AspNetCore.Mvc;
using Simple.Authorization.Entity.DBContext;
using Simple.Core.Dependency;
using Simple.Core.Extensions;
using Simple.Web.Mvc;

namespace Simple.Authorization.Controllers
{
    [Route("[controller]/[action]")]
    public abstract class AuthorizationControllerBase : SimpleControllerBase
    {
        public AuthorizationDbContext DB { get; }
        public AuthorizationControllerBase()
        {
            DB = IocCollection.Resolve<AuthorizationDbContext>();
        }

        public int UserID
        {
            get
            {
                return HttpContext.GetClaimValue("ID").GetValue<int>();
            }
        }
    }
}
