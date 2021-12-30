using Simple.dotNet.Redis;
using System;

namespace Authorization.Caching
{
    /// <summary>
    /// 缓冲基类
    /// </summary>
    internal abstract class CacheBase : RedisDatabase
    {
        protected override int Db => 0;
        public CacheBase() : base("")
        {

        }
    }
}
