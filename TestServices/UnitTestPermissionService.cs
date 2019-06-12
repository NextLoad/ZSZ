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
    public class UnitTestPermissionService
    {
        private PermissionService permService = new PermissionService();
        [TestMethod]
        public void TestAddNewPermission()
        {
            string name = Guid.NewGuid().ToString();
            long id = permService.AddNewPermission(name, name);
            PermissionDTO permissionDto1 = permService.GetById(id);
            Assert.AreEqual(permissionDto1.Name, name);
            Assert.AreEqual(permissionDto1.Description, name);


            PermissionDTO permissionDto2 = permService.GetByName(name);
            Assert.AreEqual(permissionDto2.Name, name);
            Assert.AreEqual(permissionDto2.Description, name);

            var permissions = permService.GetAll();
            Assert.IsNotNull(permissions);

            string newName = Guid.NewGuid().ToString();
            permService.UpdatePermission(id, newName, newName);
            PermissionDTO permissionDto3 = permService.GetById(id);
            Assert.AreEqual(permissionDto3.Name, newName);
            Assert.AreEqual(permissionDto3.Description, newName);
            Assert.AreNotEqual(permissionDto3.Name, name);
            Assert.AreNotEqual(permissionDto3.Description, name);

        }

        [TestMethod]
        public void TestAddPermIds()
        {
            permService.AddPermIds(1, new long[] { 1, 2 });
        }
    }
}
