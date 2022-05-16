using Simple.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Authorization.Caching
{
    public abstract class AuthorizationCacheBase : RedisDatabase
    {
        protected AuthorizationCacheBase() : base("")
        {

        }
    }
}
