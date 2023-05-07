using Microsoft.AspNetCore.Mvc;
using Simple.Authorization.Auth;
using Simple.Authorization.Entity;
using Simple.Authorization.Interface;
using Simple.Core.Authorization;

namespace Simple.Authorization.Controllers
{
    /// <summary>
    /// 角色相关
    /// </summary>
    public class RoleController : AuthControllerBase
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
        [HttpPost, Permission(PermissionNames.Authorization_Role)]
        public ActionResult GetRoles()
        {
            return JsonResult(ADB.Role.ToList());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost, Permission(PermissionNames.Authorization_Role)]
        public ActionResult Save([FromForm] int? id, [FromForm] string name, [FromForm] string description, [FromForm] string permission)
        {
            return JsonResult(_roleAppService.SaveRoleInfo(new Role
            {
                Description = description,
                Permission = permission,
                Name = name,
                ID = id ?? 0
            }));
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost, Permission(PermissionNames.Authorization_Role)]
        public ActionResult Delete([FromForm] int roleId)
        {
            return JsonResult(_roleAppService.DeleteRoleInfo(roleId));
        }
    }
}
