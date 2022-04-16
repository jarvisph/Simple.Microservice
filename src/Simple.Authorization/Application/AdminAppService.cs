using Simple.Authorization.Domain.Services;
using Simple.Authorization.Entity;
using Simple.Authorization.Model.Admin;
using Simple.Core.Dapper;
using Simple.Core.Domain.Enums;
using Simple.Core.Logger;
using Simple.Core.Encryption;
using Simple.Core.Helper;
using Simple.Web.Jwt;
using Simple.Core.Extensions;
using System.Security.Claims;
using System.Data;

namespace Simple.Authorization.Application
{
    public class AdminAppService : AuthorizationAppServiceBase, IAdminAppService
    {
        private readonly JWTOption _options;
        public AdminAppService(JWTOption option)
        {
            _options = option;
        }
        public bool DeleteAdminInfo(int adminId)
        {
            return Logger.Log($"删除管理员信息");
        }
        /// <summary>
        /// 密码错误次数
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        private int PasswordErrorCount(int adminId)
        {
            string key = $"PASSWORDERRORCOUNT:{adminId}";
            int count = MemoryHelper.GetOrCreate(key, TimeSpan.FromSeconds(60), () =>
            {
                return 0;
            });
            count++;
            MemoryHelper.Set(key, count);
            return count;
        }
        public bool Login(string username, string password, out string token)
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                Admin admin = db.FirstOrDefault<Admin>(c => c.AdminName == username);
                if (admin == null) throw new MessageException("登录名不存在");
                if (admin.Status != UserStatus.Normal) throw new MessageException("管理员状态有误，请联系超级管理员");
                if (admin.Password != PwdEncryption.Encryption(password))
                {
                    int error = PasswordErrorCount(admin.ID);
                    if (error > 5)
                    {
                        throw new MessageException("密码错误超过5次，账号已被锁定，请联系超级管理员");
                    }
                    else
                    {
                        throw new MessageException("密码错误");
                    }
                }
                admin.LoginAt = DateTime.Now;
                db.Update(admin, c => c.ID == admin.ID, c => c.LoginAt);
                var claims = new[] {
                    new Claim(nameof(admin.ID), admin.ID.ToString()),
                };
                token = JWTHelper.CreateToken(_options, claims);
            }
            return Logger.Log($"登录成功");
        }

        public bool CreateAdminInfo(AdminInput input, out string password)
        {
            if (!CheckHelper.CheckUserName(input.AdminName, out string msg))
                throw new MessageException(msg);
            using (IDapperDatabase db = CreateDatabase())
            {
                DateTime time = DateTime.Now;
                password = PwdEncryption.RandomPassword();
                db.Insert(new Admin()
                {
                    AdminName = input.AdminName,
                    CreateAt = time,
                    Password = PwdEncryption.Encryption(password, time.GetTimestamp().ToString()),
                    NickName = input.NickName,
                    Status = UserStatus.Normal
                });
            }
            return Logger.Log($"保存管理员信息");
        }

        public bool UpdateAdminInfo(AdminInput input)
        {
            throw new NotImplementedException();
        }

        public bool ResetPassword(int adminId, out string password)
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                Admin admin = db.FirstOrDefault<Admin>(c => c.ID == adminId);
                if (admin == null) throw new MessageException($"管理员不存在");
                password = PwdEncryption.RandomPassword();
                admin.Password = PwdEncryption.Encryption(password, admin.CreateAt.GetTimestamp().ToString());
                db.Update(admin, c => c.ID == admin.ID, c => c.Password);
            }
            return Logger.Log($"重置管理员密码");
        }

        public bool UpdatePassword(int adminId, string oldpassword, string newpassword)
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                Admin admin = db.FirstOrDefault<Admin>(c => c.ID == adminId);
                if (admin == null) throw new MessageException($"管理员不存在");
                if (admin.Password != PwdEncryption.Encryption(oldpassword, admin.CreateAt.GetTimestamp().ToString())) throw new MessageException($"旧密码错误");
                admin.Password = PwdEncryption.Encryption(newpassword, admin.CreateAt.GetTimestamp().ToString());
                db.Update(admin, c => c.ID == adminId, c => c.Password);
            }
            return Logger.Log($"修改密码");
        }
    }
}
