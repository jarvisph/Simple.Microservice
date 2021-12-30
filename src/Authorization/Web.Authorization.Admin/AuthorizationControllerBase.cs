using Microsoft.AspNetCore.Mvc;
using Simple.dotNet.Web.Mvc;

namespace Web.Authorization.Admin
{

    [Route("[controller]/[action]")]
    public abstract class AuthorizationControllerBase : SimpleControllerBase
    {

    }
}
