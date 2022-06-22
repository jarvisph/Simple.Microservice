using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Core.Domain;
using Simple.Core.Localization;

namespace Simple.Authorization.Application.Services
{
    public abstract class AuthorizationAppServiceBase : AppServiceBase
    {
        protected AuthorizationAppServiceBase() : base(AppsettingConfig.GetConnectionString("DbConnection"))
        {

        }
    }
}
