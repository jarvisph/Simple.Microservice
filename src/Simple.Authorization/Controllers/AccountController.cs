using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simple.Authorization.Domain.Auth;
using Simple.Authorization.Domain.Caching;
using Simple.Authorization.Domain.Model.Admin;
using Simple.Authorization.Domain.Services;
using Simple.Core.Authorization;
using Simple.Core.Extensions;

namespace Simple.Authorization.Controllers
{
    /// <summary>
    /// 当前账号相关
    /// </summary>
    public class AccountController : AuthorizationControllerBase
    {
        private readonly IAdminAppService _adminAppService;
        private readonly IAdminCaching _adminCaching;
        public AccountController(IAdminAppService adminAppService, IAdminCaching adminCaching)
        {
            _adminAppService = adminAppService;
            _adminCaching = adminCaching;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public ActionResult Login([FromForm] string username, [FromForm] string password, [FromForm] long time, [FromForm] string code)
        {
            return JsonResult(_adminAppService.Login(username, password, time, code, out string token), new { access_token = token });
        }
        /// <summary>
        /// 登录信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Info()
        {
            AdminRedis admin = _adminCaching.GetAdminInfo(UserID);
            string[] permissions = admin.IsAdmin ? PermissionFinder.GetPermission(typeof(PermissionNames)).ToArray() : _adminCaching.GetPermission(UserID).ToArray();
            return JsonResult(new
            {
                admin.ID,
                admin.AdminName,
                admin.RoleID,
                admin.NickName,
                Meuns = PermissionFinder.GetMemu(permissions, new PermissionProvider()),
                Permissions = permissions
            });
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldpassword"></param>
        /// <param name="newpassword"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdatePassword(string oldpassword, string newpassword)
        {
            return JsonResult(_adminAppService.UpdatePassword(UserID, oldpassword, newpassword));
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
