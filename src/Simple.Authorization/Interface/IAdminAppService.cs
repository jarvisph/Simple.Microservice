using Simple.Core.Dependency;
using Simple.Core.Domain.Enums;

namespace Simple.Authorization.Interface
{
    public interface IAdminAppService : ISingletonDependency
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool Login(string username, string password, long time, string code, out string token);
        /// <summary>
        /// 创建管理员
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool CreateAdminInfo(string username, int roleId, out string password);
        /// <summary>
        /// 修改管理员信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool UpdateAdminInfo(string nickname, int roleId, UserStatus status);
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        bool DeleteAdminInfo(int adminId);
        /// <summary>
        /// 重置管理员密码
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool ResetPassword(int adminId, out string password);
        /// <summary>
        /// 修改管理员密码
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="oldpassword"></param>
        /// <param name="newpassword"></param>
        /// <returns></returns>
        bool UpdatePassword(int adminId, string oldpassword, string newpassword);

    }
}
