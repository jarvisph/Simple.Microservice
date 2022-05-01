using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Core.Extensions;
using Simple.Web.Jwt;
using System.Security.Claims;
using Simple.Authorization.Domain.Services;
using Microsoft.OpenApi.Models;

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
