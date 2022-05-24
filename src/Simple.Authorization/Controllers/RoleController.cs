using Microsoft.AspNetCore.Mvc;
using Simple.Authorization.Domain.Auth;
using Simple.Authorization.Domain.Model.Role;
using Simple.Authorization.Domain.Services;
using Simple.Core.Authorization;
using Simple.Web.Mvc;

namespace Simple.Authorization.Controllers
{
    /// <summary>
    /// 角色相关
    /// </summary>
    public class RoleController : AuthorizationControllerBase
    {
        private readonly IRoleAppService _roleAppService;
        public RoleController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetRoles()
        {
            return JsonResult(DB.Role.ToList());
        }
        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPermissions()
        {
            return JsonResult(PermissionFinder.GetAllPermissionChildren(new PermissionProvider()));
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save([FromModel] RoleInput input)
        {
            return JsonResult(_roleAppService.SaveRoleInfo(input));
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult Delete([FromForm] int roleId)
        {
            return JsonResult(_roleAppService.DeleteRoleInfo(roleId));
        }
    }
}
