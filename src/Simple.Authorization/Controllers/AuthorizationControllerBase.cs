using Microsoft.AspNetCore.Mvc;
using Simple.Authorization.DBContext;
using Simple.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Core.Dependency;

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
    }
}
