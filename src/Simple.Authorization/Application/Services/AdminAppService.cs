using Simple.Authorization.Application.Caching;
using Simple.Authorization.Entity.DB;
using Simple.Authorization.Entity.Model.Admin;
using Simple.Core.Dapper;
using Simple.Core.Domain.Enums;
using Simple.Core.Encryption;
using Simple.Core.Extensions;
using Simple.Core.Helper;
using Simple.Core.Logger;
using Simple.Web.Jwt;
using System.Security.Claims;

namespace Simple.Authorization.Application.Services
{
    public class AdminAppService : AuthorizationAppServiceBase, IAdminAppService
    {
        private readonly JWTOption _options;
        private readonly IAdminCaching _adminCaching;
        public AdminAppService(JWTOption option, IAdminCaching adminCaching)
        {
            _options = option;
            _adminCaching = adminCaching;
        }
        public bool DeleteAdminInfo(int adminId)
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                db.Update<Admin, UserStatus>(c => c.ID == adminId, c => c.Status, UserStatus.Deleted);
            }
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
        public bool Login(string username, string password, long time, string code, out string token)
        {
            if (!CheckHelper.CheckUserName(username, out string msg)) throw new MessageException(msg);
            if (!CheckHelper.CheckPassword(password, out msg)) throw new MessageException(msg);
            password = password.ToUpper();
            using (IDapperDatabase db = CreateDatabase())
            {
                token = password;
                if (!db.Any<Admin>())
                {
                    long timestamp = DateTime.Now.GetTimestamp();
                    string encryption = MD5Encryption.Encryption(password);
                    db.Insert(new Admin
                    {
                        AdminName = username,
                        CreateAt = timestamp,
                        NickName = "超级管理员",
                        Password = PwdEncryption.Encryption(encryption, timestamp),
                        IsAdmin = true
                    });
                }
                else
                {
                    Admin admin = db.FirstOrDefault<Admin>(c => c.AdminName == username);
                    if (admin == null) throw new MessageException("登录名不存在");
                    if (admin.Status != UserStatus.Normal) throw new MessageException("管理员状态有误，请联系超级管理员");
                    if (admin.Password != PwdEncryption.Encryption(password, admin.CreateAt))
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
                    admin.LoginAt = DateTime.Now.GetTimestamp();
                    db.Update(admin, c => c.ID == admin.ID, c => c.LoginAt);
                    var claims = new[] {
                                       new Claim(nameof(admin.ID), admin.ID.ToString()),
                                      };
                    token = JWTHelper.CreateToken(_options, claims);
                    _adminCaching.SaveAdminInfo(admin);
                    _adminCaching.SaveAdminToken(admin.ID, token);
                }
            }
            return Logger.Log($"登录成功");
        }

        public bool CreateAdminInfo(AdminInput input, out string password)
        {
            if (!CheckHelper.CheckUserName(input.AdminName, out string msg))
                throw new MessageException(msg);
            using (IDapperDatabase db = CreateDatabase())
            {
                long timestamp = DateTime.Now.GetTimestamp();
                password = PwdEncryption.RandomPassword();
                string encryption = MD5Encryption.Encryption(password);
                db.Insert(new Admin()
                {
                    AdminName = input.AdminName,
                    CreateAt = timestamp,
                    Password = PwdEncryption.Encryption(encryption, timestamp),
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
                string encryption = MD5Encryption.Encryption(password);
                admin.Password = PwdEncryption.Encryption(encryption, admin.CreateAt);
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
                if (admin.Password != PwdEncryption.Encryption(oldpassword, admin.CreateAt)) throw new MessageException($"旧密码错误");
                string encryption = MD5Encryption.Encryption(newpassword);
                admin.Password = PwdEncryption.Encryption(encryption, admin.CreateAt);
                db.Update(admin, c => c.ID == adminId, c => c.Password);
            }
            return Logger.Log($"修改密码");
        }
    }
}
