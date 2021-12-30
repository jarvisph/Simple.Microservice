using Authorization.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Authorization.Admin.Controllers
{
    public class AdminController : AuthorizationControllerBase
    {
        private readonly IAdminAppService _adminAppService;
        public AdminController(IAdminAppService adminAppService)
        {
            _adminAppService = adminAppService;
        }
        public IActionResult Get()
        {
            return Ok(_adminAppService.Test());
        }
    }
}
