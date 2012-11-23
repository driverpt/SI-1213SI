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
    public class UserAssigmentRepositoryTests
    {
        [TestMethod]
        public void UserAssigment_LoadAll()
        {

            UserAssigmentRepository uaRepository = new UserAssigmentRepository();

            List<UserAssigment> userAssigments = uaRepository.LoadAll();

        }

        [TestMethod]
        public void UserAssigment_Load_User_Role()
        {

            Int32 userId = 1;

            UserAssigmentRepository uaRepository = new UserAssigmentRepository();

            List<UserAssigment> userRoles = uaRepository.LoadByUserId(userId);

            Assert.AreEqual(3, userRoles.Count);

        }
    }
}
