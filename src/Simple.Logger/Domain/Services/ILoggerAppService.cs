using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Core.Dependency;

namespace Simple.Logger.Domain.Services
{
    public interface ILoggerAppService : ISingletonDependency
    {
        /// <summary>
        /// 创建应用
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool CreateApplicationInfo(string name);
        /// <summary>
        /// 检查key
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        bool CheckAppKey(Guid appKey);
    }
}
