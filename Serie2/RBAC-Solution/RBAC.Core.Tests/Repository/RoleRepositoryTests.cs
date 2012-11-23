using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RBAC.Core.Repository;
using RBAC.Core.Entities;

namespace RBAC.Core.Tests.Repository
{
    [TestClass]
    public class RoleRepositoryTests
    {
        [TestMethod]
        public void RoleRepository_LoadAll()
        {

            RoleRepository roleRepository = new RoleRepository();
            
            List<Role> roles = roleRepository.LoadAll();

            Assert.AreEqual(12, roles.Count);

        }

        [TestMethod]
        public void RoleRepository_LoadById()
        {
            Int32 roleId = 1;

            RoleRepository roleRepository = new RoleRepository();

            Role role = roleRepository.LoadById(roleId);

            Assert.IsNotNull(role);
            Assert.AreEqual(roleId, role.Id);
        }
    }
}
