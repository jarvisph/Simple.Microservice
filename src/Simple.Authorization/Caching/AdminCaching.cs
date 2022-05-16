using Simple.Authorization.Domain.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Authorization.Caching
{
    internal class AdminCaching : AuthorizationCacheBase, IAdminCaching
    {
        public IEnumerable<string> GetPermission(int adminId)
        {
            throw new NotImplementedException();
        }
    }
}
