using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZSZ.DTO;
using ZSZ.Services;

namespace TestServices
{
    [TestClass]
    public class UnitTestAdminLogService
    {
        [TestMethod]
        public void TestAddNew()
        {
            AdminLogService adsService = new AdminLogService();
            long id = adsService.AddNew(2, "测试信息");
            AdminLogDTO adminLogDto = adsService.GetById(id);
            Assert.AreEqual(id, adminLogDto.Id);
            Assert.AreEqual("测试信息", adminLogDto.Msg);
        }
    }
}
