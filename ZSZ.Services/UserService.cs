using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IServices;

namespace ZSZ.Services
{
    public class UserService: IUser
    {
        public long AddNewUser(string name, string phoneNum, string password, long? cityId)
        {
            throw new NotImplementedException();
        }

        public long UpdateUser(long id, string name, string phoneNum, string password, long? cityId)
        {
            throw new NotImplementedException();
        }

        public void SetUserCityId(long id, long cityId)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetById(long id)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetByPhoneNum(string phoneNum)
        {
            throw new NotImplementedException();
        }

        public bool CheckLogin(string userName, string userPwd)
        {
            throw new NotImplementedException();
        }

        public void RecordLoginError(long id)
        {
            throw new NotImplementedException();
        }

        public void ResetLoginError(long id)
        {
            throw new NotImplementedException();
        }

        public bool IsLocked(long id)
        {
            throw new NotImplementedException();
        }
    }
}
