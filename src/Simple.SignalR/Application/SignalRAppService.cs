using Simple.Core.Dapper;
using Simple.Core.Domain;
using Simple.SignalR.Domain.Services;
using Simple.SignalR.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.SignalR.Entity;
using Simple.Core.Domain.Enums;
using Simple.Core.Localization;
using Simple.Core.Helper;
using System.Data;

namespace Simple.SignalR.Application
{
    public class SignalRAppServiceBase : AppServiceBase, ISignalRAppService
    {
        public SignalRAppServiceBase() : base(AppsettingConfig.GetConnectionString("DbConnection"))
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

        public bool CreateApplicationInfo(string name)
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                db.Insert(new ApplicationSetting
                {
                    CreateAt = DateTime.Now,
                    Name = name,
                    AppKey = Guid.NewGuid(),
                    Status = UserStatus.Normal
                });
            }
            return Logger.Log("创建应用");
        }

        public void SaveConnectionInfo(ConnectionQueue queue)
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                ConnectionClient connection = new ConnectionClient
                {
                    AppKey = queue.AppKey,
                    ConnectionID = queue.ConnectionId,
                    Content = queue.Content,
                    CreateAt = queue.CreateAt,
                    IP = queue.IP,
                    IsOnline = true
                };
                if (db.Any<ConnectionClient>(c => c.ConnectionID == queue.ConnectionId))
                {
                    connection.IsOnline = false;
                    connection.DisconnectAt = queue.CreateAt;
                    db.Update(connection, c => c.ConnectionID == queue.ConnectionId, c => c.IsOnline, c => c.DisconnectAt);
                }
                else
                {
                    db.Insert(connection);
                }
            }
        }

        public void SavePushLog(PushLogQueue queue)
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                db.Insert(new PushLog
                {
                    AppKey = queue.AppKey,
                    Channel = queue.Channel,
                    ConnectionID = queue.ConnectionId,
                    CreateAt = queue.CreateAt,
                    Message = queue.Message,
                });
            }
        }
    }
}
