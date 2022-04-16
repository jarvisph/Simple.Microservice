using Microsoft.AspNetCore.Mvc;
using Simple.Authorization.Domain.Auth;
using Simple.Authorization.Domain.Services;
using Simple.Authorization.Model.Role;
using Simple.Core.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Authorization.Controllers
{
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
        public ActionResult Save([FromForm] RoleInput input)
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
