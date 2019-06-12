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
    public class UnitTestRoleService
    {
        private RoleService roleService = new RoleService();

        [TestMethod]
        public void TestAddNewRole()
        {
            string name = Guid.NewGuid().ToString();
            long id = roleService.AddNewRole(name);

            RoleDTO role = roleService.GetById(id);
            Assert.AreEqual(role.Name,name);

            string newName = Guid.NewGuid().ToString();
            roleService.UpdateRole(id,newName);
            RoleDTO role2 = roleService.GetById(id);
            Assert.AreEqual(role2.Name,newName);
            Assert.AreNotEqual(role2.Name, name);

            RoleDTO role3 = roleService.GetByName(newName);
            Assert.AreEqual(role3.Name,newName);
            Assert.AreNotEqual(role3.Name,name);

            RoleDTO[] roles = roleService.GetByAdminUserId(12);
            Assert.AreEqual(0,roles.Length);
            

        }
        [TestMethod]
        public void TestAddRoleIds()
        {
            roleService.AddRoleIds(11,new long[]{1});

        }
    }
}
