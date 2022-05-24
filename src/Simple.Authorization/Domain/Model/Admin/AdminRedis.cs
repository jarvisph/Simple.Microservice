using Newtonsoft.Json;
using Simple.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Authorization.Domain.Model.Admin
{
    public class AdminRedis : AdminBase
    {
        public override int ID { get; set; }
        public override int RoleID { get; set; }
        public override string AdminName { get; set; } = string.Empty;
        public override string NickName { get; set; } = string.Empty;
        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        public static implicit operator AdminRedis(RedisValue value)
        {
            return JsonConvert.DeserializeObject<AdminRedis>(value.GetRedisValue<string>());
        }
        public static implicit operator RedisValue(AdminRedis admin)
        {
            return JsonConvert.SerializeObject(admin).ToRedisValue();
        }
    }
}
