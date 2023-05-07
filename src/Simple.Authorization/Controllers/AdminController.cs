using Microsoft.AspNetCore.Mvc;
using Simple.Authorization.Auth;
using Simple.Authorization.Entity;
using Simple.Authorization.Interface;
using Simple.Core.Authorization;
using Simple.Core.Domain.Enums;
using Simple.Core.Extensions;

namespace Simple.Authorization.Controllers
{
    /// <summary>
    /// 管理员相关
    /// </summary>
    public class AdminController : AuthControllerBase
    {
        private readonly IAdminAppService _adminAppService;
        public AdminController(IAdminAppService adminAppService)
        {
            _adminAppService = adminAppService;
        }
        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <returns></returns>
        [HttpPost, Permission(PermissionNames.Authorization_Admin)]
        public ActionResult List([FromForm] string? adminname, [FromForm] string? nickname)
        {
            var query = ADB.Admin.Where(adminname, c => c.AdminName == adminname)
                                .Where(nickname, c => c.NickName == nickname);
            return PageResult(query.OrderByDescending(c => c.CreateAt), c => new
            {
                c.ID,
                c.AdminName,
                c.NickName,
                c.LoginIP,
                c.IsAdmin,
                c.Face,
                c.RoleID,
                c.LoginAt,
                c.Status,
                c.CreateAt
            });
        }
        /// <summary>
        /// 保存管理员信息
        /// </summary>
        /// <returns></returns>
        [HttpPost, Permission(PermissionNames.Authorization_Admin)]
        public ActionResult Save([FromForm] int id, [FromForm] string username, [FromForm] string nickname, [FromForm] int roleId, [FromForm] UserStatus status)
        {
            if (id == 0)
            {
                return JsonResult(_adminAppService.CreateAdminInfo(username, roleId, out string password), new
                {
                    Password = password
                });
            }
            else
            {
                return JsonResult(_adminAppService.UpdateAdminInfo(nickname, roleId, status));
            }
        }
        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [HttpPost, Permission(PermissionNames.Authorization_Admin)]
        public ActionResult Info([FromForm] int adminId)
        {
            Admin admin = ADB.Admin.FirstOrDefault(c => c.ID == adminId, new Admin());
            return JsonResult(new
            {
                admin.ID,
                admin.AdminName,
                admin.NickName,
                admin.Status,
            });
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [HttpPost, Permission(PermissionNames.Authorization_Admin)]
        public ActionResult Delete([FromForm] int adminId)
        {
            return JsonResult(_adminAppService.DeleteAdminInfo(adminId));
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [HttpPost, Permission(PermissionNames.Authorization_Admin)]
        public ActionResult ResetPassword([FromForm] int adminId)
        {
            return JsonResult(_adminAppService.ResetPassword(adminId, out string password), new
            {
                Password = password
            });
        }
    }
}
