using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IServices;
using ZSZ.Services.Entities;
using ZSZ.Web.Common;

namespace ZSZ.Services
{
    public class UserService : IUser
    {
        public long AddNewUser(string phoneNum, string password, long? cityId)
        {
            UserEntity user = new UserEntity();
            user.PhoneNum = phoneNum;
            user.PasswordSalt = WebCommonHelper.CreateVerifyCode(4);
            user.PasswordHash = Common.CommonHelper.CalcMD5(user.PasswordSalt + password);
            user.CityId = cityId;
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<UserEntity> userService = new CommonService<UserEntity>(ctx);
                bool exists = userService.GetAll().Any(u => u.PhoneNum == phoneNum);
                if (exists)
                {
                    throw new ArgumentException("手机号已存在" + phoneNum);
                }
                ctx.Users.Add(user);
                ctx.SaveChanges();
                return user.Id;
            }
        }

        public long UpdateUser(long id, string phoneNum, string password, long? cityId)
        {
            throw new NotImplementedException();
        }

        public void SetUserCityId(long id, long cityId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<UserEntity> userService = new CommonService<UserEntity>(ctx);
                UserEntity user = userService.GetById(id);
                if (user == null)
                {
                    throw new ArgumentException("用户不存在" + id);
                }

                user.CityId = cityId;
                ctx.SaveChanges();
            }
        }

        private UserDTO ToDTO(UserEntity user)
        {
            UserDTO userDto = new UserDTO();
            userDto.PhoneNum = user.PhoneNum;
            userDto.CityId = user.CityId;
            userDto.CityName = user.CityEntity.Name;
            userDto.LastLoginErrorDateTime = user.LastLoginErrorDateTime;
            userDto.LoginErrorTimes = user.LoginErrorTimes;
            userDto.PasswordHash = user.PasswordHash;
            userDto.PasswordSalt = user.PasswordSalt;
            userDto.CreateDateTime = user.CreateDateTime;
            userDto.Id = user.Id;
            return userDto;
        }

        public UserDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<UserEntity> userService = new CommonService<UserEntity>(ctx);
                UserEntity user = userService.GetById(id);
                if (user == null)
                {
                    throw new ArgumentException("用户不存在" + id);
                }

                return ToDTO(user);
            }
        }



        public UserDTO GetByPhoneNum(string phoneNum)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<UserEntity> userService = new CommonService<UserEntity>(ctx);
                UserEntity user = userService.GetAll().SingleOrDefault(u => u.PhoneNum == phoneNum);
                if (user == null)
                {
                    throw new ArgumentException("手机号不存在" + phoneNum);
                }

                return ToDTO(user);
            }
        }

        public bool CheckLogin(string phoneNum, string userPwd)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<UserEntity> userService = new CommonService<UserEntity>(ctx);
                UserEntity user = userService.GetAll().SingleOrDefault(u => u.PhoneNum == phoneNum);
                if (user == null)
                {
                    return false;
                }

                string salt = user.PasswordSalt;
                string password = Common.CommonHelper.CalcMD5(salt + userPwd);
                if (password != user.PasswordHash)
                {
                    return false;
                }

                return true;
            }
        }

        public void RecordLoginError(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<UserEntity> userService = new CommonService<UserEntity>(ctx);
                var user = userService.GetById(id);
                if (user == null)
                {
                    throw new ArgumentException("用户不存在"+id);
                }
                user.LastLoginErrorDateTime = DateTime.Now;
                user.LoginErrorTimes++;
                ctx.SaveChanges();

            }
        }

        public void ResetLoginError(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<UserEntity> userService = new CommonService<UserEntity>(ctx);
                var user = userService.GetById(id);
                if (user == null)
                {
                    throw new ArgumentException("用户不存在" + id);
                }
                
                user.LoginErrorTimes = 0;
                user.LastLoginErrorDateTime = null;
                ctx.SaveChanges();

            }
        }

        public bool IsLocked(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<UserEntity> userService = new CommonService<UserEntity>(ctx);
                var user = userService.GetById(id);
                if (user == null)
                {
                    throw new ArgumentException("用户不存在" + id);
                }

                if (user.LoginErrorTimes >= 5 && user.LastLoginErrorDateTime > DateTime.Now.AddMinutes(-30))
                {
                    return true;
                }
                return false;
            }
        }
    }
}
