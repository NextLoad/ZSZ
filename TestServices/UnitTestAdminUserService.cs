using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZSZ.Common;
using ZSZ.DTO;
using ZSZ.Services;

namespace TestServices
{
    [TestClass]
    public class UnitTestAdminUserService
    {
        [TestMethod]
        public void TestAddAdminUser()
        {
            AdminUserService auService = new AdminUserService();
            long id = auService.AddAdminUser("wuhuajie", "12312312312", "123456", "qwe@qq.com", null);
            try
            {
                AdminUserDTO userDto = auService.GetById(id);
                Assert.AreEqual(id, userDto.Id);
                Assert.AreEqual("wuhuajie", userDto.Name);
                Assert.AreEqual("12312312312", userDto.PhoneNum);
                Assert.AreEqual(CommonHelper.CalcMD5(userDto.PasswordSalt + "123456"), userDto.PasswordHash);
                Assert.AreEqual("qwe@qq.com", userDto.EMail);

                AdminUserDTO[] userDtos = auService.GetAll();
                //AminUserDTO[] userDtos2 = auService.GetAll();
                Assert.IsNotNull(userDtos);
                AdminUserDTO userDto2 = auService.GetByPhoneNum("12312312312");
                Assert.IsNotNull(userDto2);
                AdminUserDTO userDto3 = auService.GetByPhoneNum("1231231221312");
                Assert.IsNull(userDto3);
                bool login = auService.CheckLogin("12312312312", "123456");
                Assert.IsTrue(login);
                bool login2 = auService.CheckLogin("123412312312", "123456");
                Assert.IsFalse(login2);
                bool login3 = auService.CheckLogin("12312312312", "3422");
                Assert.IsFalse(login3);
                bool login4 = auService.CheckLogin("3242323423", "3422");
                Assert.IsFalse(login4);

                auService.MarkDeleted(id);
            }
            catch (Exception ex)
            {
                auService.MarkDeleted(id);
                throw ex;
            }

        }
    }
}
