using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IServices;
using ZSZ.Services.Entities;

namespace ZSZ.Services
{
    public class AdminUserService : IAdminUser
    {
        public long AddAdminUser(string name, string phoneNum, string password, string email, long? cityId)
        {
            AdminUserEntity adminUser = new AdminUserEntity();
            adminUser.Name = name;
            adminUser.PhoneNum = phoneNum;
            string salt = Web.Common.WebCommonHelper.CreateVerifyCode(5);
            adminUser.PasswordSalt = salt;
            adminUser.CityId = cityId;
            adminUser.EMail = email;
            adminUser.PasswordHash = Common.CommonHelper.CalcMD5(salt + password);
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> cs = new CommonService<AdminUserEntity>(ctx);
                bool exists = cs.GetAll().Any(u => u.PhoneNum == phoneNum);
                if (exists)
                {
                    throw new ArgumentException("手机号已存在" + phoneNum);
                }
                ctx.AdminUsers.Add(adminUser);
                ctx.SaveChanges();
                return adminUser.Id;
            }
        }

        public void UpdateAdminUser(long id, string name, string phoneNum, string password, string email, long? cityId)
        {
            AdminUserEntity adminUser = new AdminUserEntity();
            adminUser.Name = name;
            adminUser.PhoneNum = phoneNum;
            string salt = Web.Common.WebCommonHelper.CreateVerifyCode(5);
            adminUser.PasswordSalt = salt;
            adminUser.CityId = cityId;
            adminUser.EMail = email;
            adminUser.PasswordHash = Common.CommonHelper.CalcMD5(salt + password);
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> cs = new CommonService<AdminUserEntity>(ctx);
                bool exists = cs.GetAll().Any(u => u.Id == id);
                if (!exists)
                {
                    throw new ArgumentException("用户不存在" + id);
                }
                ctx.AdminUsers.Add(adminUser);
                ctx.SaveChanges();
            }
        }

        public AdminUserDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> cs = new CommonService<AdminUserEntity>(ctx);
                return cs.GetAll().Include(u => u.CityEntity).AsNoTracking().ToList().Select(u => ToDTO(u)).ToArray();
            }
        }

        private AdminUserDTO ToDTO(AdminUserEntity adminUserEntity)
        {
            AdminUserDTO auDTO = new AdminUserDTO();
            auDTO.PhoneNum = adminUserEntity.PhoneNum;
            auDTO.CityId = adminUserEntity.CityId;
            if (adminUserEntity.CityEntity != null)
            {
                auDTO.CityName = adminUserEntity.CityEntity.Name;
            }
            auDTO.EMail = adminUserEntity.EMail;
            auDTO.Name = adminUserEntity.Name;
            auDTO.PasswordSalt = adminUserEntity.PasswordSalt;
            auDTO.PasswordHash = adminUserEntity.PasswordHash;
            auDTO.LastLoginErrorDateTime = adminUserEntity.LastLoginErrorDateTime;
            auDTO.LoginErrorTimes = adminUserEntity.LoginErrorTimes;
            auDTO.CreateDateTime = adminUserEntity.CreateDateTime;
            auDTO.Id = adminUserEntity.Id;
            return auDTO;
        }

        public AdminUserDTO[] GetAll(long? cityId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> cs = new CommonService<AdminUserEntity>(ctx);
                return cs.GetAll().Include(u => u.CityEntity).AsNoTracking().Where(u => u.CityId == cityId).Select(u => ToDTO(u)).ToArray();
            }
        }

        public AdminUserDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> cs = new CommonService<AdminUserEntity>(ctx);
                //AdminUserEntity adminUser = cs.GetAll().Include(u => u.CityEntity).SingleOrDefault(u => u.Id == id);
                AdminUserEntity adminUser = cs.GetById(id);
                return ToDTO(adminUser);
            }
        }

        public AdminUserDTO GetByPhoneNum(string phoneNum)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> cs = new CommonService<AdminUserEntity>(ctx);
                AdminUserEntity adminUser = cs.GetAll().SingleOrDefault(a => a.PhoneNum == phoneNum);
                if (adminUser == null)
                {
                    return null;
                }
                return ToDTO(adminUser);
            }
        }

        public bool CheckLogin(string phoneNum, string userPwd)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> cs = new CommonService<AdminUserEntity>(ctx);
                AdminUserEntity adminUser = cs.GetAll().SingleOrDefault(a => a.PhoneNum == phoneNum);
                if (adminUser == null)
                {
                    return false;
                }

                string pwd = Common.CommonHelper.CalcMD5(adminUser.PasswordSalt + userPwd);
                if (pwd != adminUser.PasswordHash)
                {
                    return false;
                }

                return true;
            }
        }

        public bool MarkDeleted(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> cs = new CommonService<AdminUserEntity>(ctx);
                AdminUserEntity adminUser = cs.GetById(id);
                if (adminUser != null)
                {
                    adminUser.IsDelete = true;
                    ctx.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        public bool HasPermission(long adminUserId, string permissionName)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> cs = new CommonService<AdminUserEntity>(ctx);
                AdminUserEntity user = cs.GetById(adminUserId);
                if (user == null)
                {
                    throw new ArgumentException("用户不存在" + adminUserId);
                }

                return user.RoleEntities.SelectMany(r => r.PermissionEntities).Any(p => p.Name == permissionName);

                //foreach (RoleEntity role in user.RoleEntities)
                //{
                //    foreach (PermissionEntity permission in role.PermissionEntities)
                //    {
                //        if (permission.Name == permissionName)
                //        {
                //            return true;
                //        }
                //    }
                //}

                //return false;

            }
        }

        public void RecordLoginError(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> cs = new CommonService<AdminUserEntity>(ctx);
                AdminUserEntity user = cs.GetById(id);
                if (user == null)
                {
                    throw new ArgumentException("用户不存在" + id);
                }

                user.LoginErrorTimes++;
                user.LastLoginErrorDateTime = DateTime.Now;
                ctx.SaveChanges();
            }
        }

        public void ResetLoginError(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> cs = new CommonService<AdminUserEntity>(ctx);
                AdminUserEntity user = cs.GetById(id);
                if (user == null)
                {
                    throw new ArgumentException("用户不存在" + id);
                }

                user.LoginErrorTimes = 0;
                ctx.SaveChanges();
            }
        }
    }
}
