using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Simple.Web.API
{
    [Route("[controller]")]
    public class HealtyController : ControllerBase
    {
        [AllowAnonymous]
        public ActionResult Get()
        {
            return Ok(DateTime.Now.ToString());
        }
    }
}
