using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simple.Authorization.Domain.Services;
using Simple.Authorization.Entity;
using Simple.Authorization.Domain.Model.Admin;
using Simple.Core.Extensions;
using Simple.Web.Mvc;

namespace Simple.Authorization.Controllers
{
    public class AdminController : AuthorizationControllerBase
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
        [HttpPost]
        public ActionResult List([FromForm] string? adminname, [FromForm] string? nickname)
        {
            var query = DB.Admin.Where(adminname, c => c.AdminName == adminname)
                                .Where(nickname, c => c.NickName == nickname);
            return PageResult(query.OrderByDescending(c => c.CreateAt), c => new
            {
                c.ID,
                c.AdminName,
                c.NickName,
                c.LoginIP,
                c.LoginAt,
                c.Status,
                c.CreateAt
            });
        }
        /// <summary>
        /// 保存管理员信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save([FromModel] AdminInput input)
        {
            if (input.ID == 0)
            {
                return JsonResult(_adminAppService.CreateAdminInfo(input, out string password), new
                {
                    Password = password
                });
            }
            else
            {
                return JsonResult(_adminAppService.UpdateAdminInfo(input));
            }
        }
        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Info([FromForm] int adminId)
        {
            Admin admin = DB.Admin.FirstOrDefault(c => c.ID == adminId, new Admin());
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
        [HttpDelete]
        public ActionResult Delete([FromForm] int adminId)
        {
            return JsonResult(_adminAppService.DeleteAdminInfo(adminId));
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public ActionResult ResetPassword([FromForm] int adminId)
        {
            return JsonResult(_adminAppService.ResetPassword(adminId, out string password), new
            {
                Password = password
            });
        }
    }
}
