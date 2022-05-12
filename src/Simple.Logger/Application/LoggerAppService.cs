using Simple.Core.Dapper;
using Simple.Core.Domain;
using Simple.Core.Domain.Enums;
using Simple.Core.Helper;
using Simple.Core.Localization;
using Simple.Logger.Domain.Services;
using Simple.Logger.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Logger.Application
{
    public class LoggerAppService : AppServiceBase, ILoggerAppService
    {
        public LoggerAppService() : base(AppsettingConfig.GetConnectionString("DbConnection"))
        {

        }

        public bool CheckAppKey(Guid appKey)
        {
            if (appKey == Guid.Empty) return false;
            return MemoryHelper.GetOrCreate(appKey.ToString("N"), TimeSpan.MaxValue, () =>
            {
                using (IDapperDatabase db = CreateDatabase())
                {
                    return db.Any<ApplicationSetting>(c => c.AppKey == appKey);
                }
            });
        }

        /// <summary>
        /// 创建应用
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CreateApplicationInfo(string name)
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                db.Insert(new ApplicationSetting
                {
                    AppKey = Guid.NewGuid(),
                    Name = name,
                    CreateAt = DateTime.Now,
                    Status = UserStatus.Normal
                });
            }
            return Logger.Log($"创建应用");
        }
    }
}
