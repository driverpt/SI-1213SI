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
    public class UserRepositoryTests
    {
        [TestMethod]
        public void TestMethod1()
        {

            UserRepository userRepository = new UserRepository();

            List<User> users = userRepository.LoadAll();


        }
    }
}
