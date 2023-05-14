using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simple.Core.Domain.Model;
using Simple.Core.Extensions;
using Simple.Core.Localization;
using Simple.Web.Mvc;

namespace Simple.Authorization.Controllers
{
    /// <summary>
    /// 授权相关
    /// </summary>
    [Route("auth/[controller]/[action]")]
    public abstract class AuthControllerBase : SimpleControllerBase
    {
        public DbContextOptions DbContextOptions()
        {
            return new DbContextOptionsBuilder().UseSqlServer(AppsettingConfig.GetConnectionString("DBConnection")).Options;
        }
        public AccountModel Account => HttpContext.GetItem<AccountModel>();
    }
}
