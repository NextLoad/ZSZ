using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IServices
{
    public interface IAdminUser : IServiceAutofac
    {
        /// <summary>
        /// 新增一个用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        long AddAdminUser(string name, string phoneNum, string password, string email, long? cityId);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="cityId"></param>
        void UpdateAdminUser(long id, string name, string phoneNum, string password, string email, long? cityId);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        /// <param name="email"></param>
        /// <param name="cityId"></param>
        void UpdateAdminUser(long id, string name, string phoneNum, string email, long? cityId);

        /// <summary>
        /// 获取所有的管理员
        /// </summary>
        /// <returns></returns>
        AdminUserDTO[] GetAll();

        /// <summary>
        /// 获取某个城市下的所有管理员
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        AdminUserDTO[] GetAll(long? cityId);

        /// <summary>
        /// 获取指定的管理员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AdminUserDTO GetById(long id);

        /// <summary>
        /// 根据手机号获取管理员
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <returns></returns>
        AdminUserDTO GetByPhoneNum(string phoneNum);

        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        bool CheckLogin(string phoneNum, string userPwd);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool MarkDeleted(long id);

        /// <summary>
        /// 检查用户是否拥有权限
        /// </summary>
        /// <param name="adminUserId"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        bool HasPermission(long adminUserId, string permissionName);

        /// <summary>
        /// 记录登录错误
        /// </summary>
        /// <param name="id"></param>
        void RecordLoginError(long id);

        /// <summary>
        /// 重置登录错误
        /// </summary>
        /// <param name="id"></param>
        void ResetLoginError(long id);
    }
}
