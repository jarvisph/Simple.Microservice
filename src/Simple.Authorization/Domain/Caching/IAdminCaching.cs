using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Core.Dependency;

namespace Simple.Authorization.Domain.Caching
{
    internal interface IAdminCaching : ISingletonDependency
    {
        public IEnumerable<string> GetPermission(int adminId);
    }
}
