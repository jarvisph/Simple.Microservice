using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Core.Dependency;
using Simple.SignalR.Queues;

namespace Simple.SignalR.Domain.Services
{
    public interface IConnectionAppService : ISingletonDependency
    {
        /// <summary>
        /// 保存连接信息
        /// </summary>
        /// <param name="queue"></param>
        void SaveConnectionInfo(ConnectionQueue queue);
        /// <summary>
        /// 创建应用
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool CreateApplicationInfo(string name);
        /// <summary>
        /// 保存推送日志
        /// </summary>
        /// <param name="queue"></param>
        void SavePushLog(PushLogQueue queue);
        /// <summary>
        /// 检查key
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        bool CheckAppKey(Guid appKey);
    }
}
