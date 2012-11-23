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
    public class RoleRestrictionsRepositoryTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            RoleRestrictionsRepository roleRestrictionsRepository = new RoleRestrictionsRepository();

            List<Restriction> restrictions = roleRestrictionsRepository.LoadAll();

            Assert.AreEqual(1, restrictions.Count);
        }
    }
}
