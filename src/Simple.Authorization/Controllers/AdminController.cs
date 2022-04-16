using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simple.Authorization.Domain.Services;
using Simple.Authorization.Entity;
using Simple.Authorization.Model.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [HttpGet]
        public ActionResult List()
        {
            return PageResult(DB.Admin.OrderByDescending(c => c.CreateAt), c => new
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
        public ActionResult Save([FromForm] AdminInput input)
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
    }
}
