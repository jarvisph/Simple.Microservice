using Simple.Authorization.Entity.Model.Admin;
using Simple.Redis;
using StackExchange.Redis;

namespace Simple.Authorization.Application.Caching
{
    internal class AdminCaching : RedisDatabase, IAdminCaching
    {
        protected override int Db => 0;
        /// <summary>
        /// 管理员信息
        /// </summary>
        private const string ADMIN_INFO = "ADMIN_INFO";
        /// <summary>
        /// 管理员权限
        /// </summary>
        private const string ADMIN_PERMISSION = "ADMIN_PERMISSION:";
        /// <summary>
        /// 管理员token
        /// </summary>
        private const string ADMIN_TOKEN = "ADMIN_TOKEN";

        public IEnumerable<string> GetPermission(int roleId)
        {
            string key = ADMIN_PERMISSION + roleId;
            foreach (var item in Redis.SetMembers(key))
            {
                yield return item.GetRedisValue<string>();
            }
        }
        public void SavePermission(int roleId, IEnumerable<string> permissions)
        {
            string key = ADMIN_PERMISSION + roleId;
            IBatch batch = Redis.CreateBatch();
            batch.KeyDeleteAsync(key);
            foreach (var item in permissions)
            {
                batch.SetAddAsync(key, item);
            }
            batch.Execute();
        }
        public void DeletePermission(int roleId)
        {
            string key = ADMIN_PERMISSION + roleId;
            Redis.KeyDelete(key);
        }

        public AdminRedis GetAdminInfo(int adminId)
        {
            return Redis.HashGet(ADMIN_INFO, adminId);
        }

        public void SaveAdminInfo(AdminRedis admin)
        {
            Redis.HashSet(ADMIN_INFO, admin.ID, admin);
        }

        public bool CheckPermission(int adminId, string permission)
        {
            string key = ADMIN_PERMISSION + adminId;
            return Redis.SetContains(key, permission);
        }

        public void SaveAdminToken(int adminId, string token)
        {
            Redis.HashSet(ADMIN_TOKEN, adminId, token);
        }

        public bool CheckToken(int adminId, string token)
        {
            return Redis.HashGet(ADMIN_TOKEN, adminId).GetRedisValue<string>() == token;
        }
        public bool RemoveToken(int adminId)
        {
            return Redis.HashDelete(ADMIN_TOKEN, adminId);
        }


    }
}
