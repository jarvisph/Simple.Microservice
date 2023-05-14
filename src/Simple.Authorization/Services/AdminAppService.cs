using Simple.Authorization.Caching;
using Simple.Authorization.Entity;
using Simple.Authorization.Interface;
using Simple.Core.Dapper;
using Simple.Core.Domain;
using Simple.Core.Domain.Enums;
using Simple.Core.Encryption;
using Simple.Core.Helper;
using Simple.Core.Logger;
using System.Data;

namespace Simple.Authorization.Services
{
    public class AdminAppService : ServiceBase, IAdminAppService
    {
        private readonly IAdminCaching _adminCaching;
        public AdminAppService(IAdminCaching adminCaching)
        {
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
            token = string.Empty;
            if (!CheckHelper.CheckUserName(username, out string message)) throw new MessageException(message);
            if (!CheckHelper.CheckPassword(password, out message, 8, 16)) throw new MessageException(message);
            Admin admin;
            using (IDapperDatabase db = CreateDatabase(IsolationLevel.ReadUncommitted))
            {
                if (db.Count<Admin>() == 0)
                {
                    long createAt = WebAgent.GetTimestamps();
                    db.InsertIdentity(new Admin
                    {
                        AdminName = username,
                        CreateAt = createAt,
                        LoginIP = IPHelper.IP,
                        Password = PwdEncryption.Encryption(password, createAt),
                        Status = UserStatus.Normal,
                        IsAdmin = true,
                    });
                }
                admin = db.FirstOrDefault<Admin>(c => c.AdminName == username);
                if (admin == null) throw new MessageException("管理员不存在");
                if (admin.Status != UserStatus.Normal) throw new MessageException("管理员已禁止登录");
                if (admin.Password != PwdEncryption.Encryption(password, admin.CreateAt))
                {
                    if (PasswordErrorCount(admin.ID) > 5)
                    {
                        admin.Status = UserStatus.Lock;
                        db.Update(admin, c => c.ID == admin.ID, c => c.Status);
                        throw new MessageException($"密码错误超过：5次，账号已锁定，请联系管理员");
                    }
                    throw new MessageException("密码错误");
                }
                admin.LoginAt = WebAgent.GetTimestamps();
                admin.LoginIP = IPHelper.IP;
                token = _adminCaching.Login(admin.ID);
                db.Update(admin, c => c.ID == admin.ID, c => c.LoginIP, c => c.LoginAt);
                db.Collback(() =>
                {
                    _adminCaching.SaveAdminInfo(admin);
                });
                db.Commit();
            }
            return Logger.Log($"登录成功/{username}", admin.ID);
        }

        public bool CreateAdminInfo(string adminname, int roleId, out string password)
        {
            if (!CheckHelper.CheckUserName(adminname, out string message)) throw new MessageException(message);
            using (IDapperDatabase db = CreateDatabase())
            {
                long timestamp = WebAgent.GetTimestamps();
                password = PwdEncryption.RandomPassword();
                string encryption = MD5Encryption.Encryption(password);
                db.Insert(new Admin()
                {
                    RoleID = roleId,
                    AdminName = adminname,
                    CreateAt = timestamp,
                    Password = PwdEncryption.Encryption(encryption, timestamp),
                    NickName = adminname,
                    Status = UserStatus.Normal
                });
            }
            return Logger.Log($"保存管理员信息");
        }

        public bool UpdateAdminInfo(int adminId, string nickname, int roleId, UserStatus status)
        {
            if (!CheckHelper.CheckName(nickname, out string message)) throw new MessageException(message);
            using (IDapperDatabase db = CreateDatabase())
            {
                Admin admin = db.FirstOrDefault<Admin>(c => c.ID == adminId);
                if (admin == null) throw new MessageException($"管理员不存在");
                admin.NickName = nickname;
                admin.RoleID = roleId;
                admin.Status = status;
                db.Update(admin, c => c.ID == adminId, c => c.NickName, c => c.RoleID, c => c.Status);
            }
            return Logger.Log($"修改管理员资料/{adminId}");
        }

        public bool ResetPassword(int adminId, out string password)
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                Admin admin = db.FirstOrDefault<Admin>(c => c.ID == adminId);
                if (admin == null) throw new MessageException($"管理员不存在");
                password = PwdEncryption.RandomPassword();
                admin.Password = PwdEncryption.Encryption(password, admin.CreateAt);
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

        public bool Logout(int adminId)
        {
            _adminCaching.RemoveToken(adminId);
            return Logger.Log($"登出账号/{adminId}");
        }

        public IEnumerable<Admin> GetAdmins()
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                return db.GetAll<Admin>();
            }
        }

        public Admin GetAdminInfo(int adminId)
        {
            using (IDapperDatabase db = CreateDatabase())
            {
                return db.FirstOrDefault<Admin>(c => c.ID == adminId);
            }
        }
    }
}
