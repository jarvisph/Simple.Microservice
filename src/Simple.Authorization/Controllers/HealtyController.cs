using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Simple.Authorization.Controllers
{
    [Route("[controller]")]
    public class HealtyController : AuthorizationControllerBase
    {
        [AllowAnonymous]
        public ActionResult Get()
        {
            return Ok(DateTime.Now.ToString());
        }
    }
}
