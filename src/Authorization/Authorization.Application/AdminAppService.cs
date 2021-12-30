using Authorization.Domain.Caching;
using Authorization.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Application
{
    internal class AdminAppService : ApplicationBase, IAdminAppService
    {
        private readonly IAdminCaching _adminCaching;
        public AdminAppService(IAdminCaching adminCaching)
        {
            _adminCaching = adminCaching;
        }
        public string Test()
        {
            return "我是测试";
        }
    }
}
