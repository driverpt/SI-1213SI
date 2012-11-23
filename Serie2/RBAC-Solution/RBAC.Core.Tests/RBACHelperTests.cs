using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RBAC.Core.Entities;
using RBAC.Core.Repository;

namespace RBAC.Core.Tests
{
    [TestClass]
    public class RBACHelperTests
    {
        private readonly RBACHelper rbac = new RBACHelper();

        [TestMethod]
        public void User_Session_With_Role_And_Permission_To_Access()
        {

            Session session = Factory.Session(
                Factory.User("User 1"),
                Factory.Role("Role 1")
            );

            Permission permission = Factory.Permission("Permission 1");

            Boolean result = rbac.UserIsAllowed(session, permission);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void User_Session_Without_Role_Cant_Access()
        {

            Session session = Factory.Session(
                Factory.User("User 1"),
                Factory.Role("Role 2")
            );

            Permission permission = Factory.Permission("Permission 1");

            Boolean result = rbac.UserIsAllowed(session, permission);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void User_Session_With_Child_Role_And_Permission_To_Access()
        {

            Session session = Factory.Session(
                Factory.User("User 1"),
                Factory.Role("Role 11")
            );

            Permission permission = Factory.Permission("Permission 1");

            Boolean result = rbac.UserIsAllowed(session, permission);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void User_Session_Without_Child_Role_Cant_Access()
        {

            Session session = Factory.Session(
                Factory.User("User 1"),
                Factory.Role("Role 12")
            );

            Permission permission = Factory.Permission("Permission 1");

            Boolean result = rbac.UserIsAllowed(session, permission);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Factory_CreateSession_()
        {

            Session session = Factory.Session(
                Factory.User("User 1"),
                Factory.Role("Role 1"),
                Factory.Role("Role 3")
            );

            Assert.IsNull(session);

        }

        [TestMethod]
        public void UserManager_Returns_All_Roles_Hierarchy()
        {
            List<Role> roles = rbac.GetUserRolesHierarchy(2);

            Assert.AreEqual(6, roles.Count);

            Assert.AreEqual(3, roles[0].Id);
            Assert.AreEqual(6, roles[1].Id);
            Assert.AreEqual(7, roles[2].Id);
            Assert.AreEqual(11, roles[3].Id);
            Assert.AreEqual(12, roles[4].Id);
            Assert.AreEqual(13, roles[5].Id);
        }

    }
}
