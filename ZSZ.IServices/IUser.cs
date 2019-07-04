using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IServices
{
    public interface IUser : IServiceAutofac
    {
        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        /// <param name="password"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        long AddNewUser( string phoneNum, string password, long? cityId);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        /// <param name="password"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        long UpdateUser( string phoneNum, string password, long? cityId);

        /// <summary>
        /// 设置用户城市id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cityId"></param>
        void SetUserCityId(long id, long cityId);

        /// <summary>
        /// 获取指定用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserDTO GetById(long id);

        /// <summary>
        /// 获取指定用户信息
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <returns></returns>
        UserDTO GetByPhoneNum(string phoneNum);

        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        bool CheckLogin(string phoneNum, string userPwd);

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

        /// <summary>
        /// 是否锁定
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsLocked(long id);
    }
}
