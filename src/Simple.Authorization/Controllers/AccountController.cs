using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simple.Authorization.Domain.Auth;
using Simple.Authorization.Domain.Services;
using Simple.Core.Authorization;
using Simple.Core.Extensions;

namespace Simple.Authorization.Controllers
{
    public class AccountController : AuthorizationControllerBase
    {
        private readonly IAdminAppService _adminAppService;
        public AccountController(IAdminAppService adminAppService)
        {
            _adminAppService = adminAppService;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public ActionResult Login([FromForm] string username, [FromForm] string password)
        {
            return JsonResult(_adminAppService.Login(username, password, out string token), new { access_token = token });
        }
        /// <summary>
        /// 登录信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Info()
        {
            return Ok(new
            {
                UserID = HttpContext.GetClaimValue("ID"),
            });
        }
        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Menu()
        {
            string[] permissions = PermissionFinder.GetPermission(typeof(PermissionNames)).ToArray();
            return JsonResult(PermissionFinder.GetMemu(permissions, new PermissionProvider()));
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Logout()
        {
            return Ok("登出");
        }
    }
}
