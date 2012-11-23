using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RBAC.Core.Entities;

namespace RBAC.Core.Tests.Entities
{
    [TestClass]
    public class RestrictionTests
    {
        [TestMethod]
        public void Roles_Defined_In_Restriction_Object_Cant_Exist_At_Same_Time()
        {

            Role[] roles = new Role[] 
            {
                Factory.Role("Role 1"),
                Factory.Role("Role 3"),
                Factory.Role("Role 7"),
                Factory.Role("Role 10")
            };

            Restriction r1 = new Restriction(1, 3, 7, 10);
            Assert.IsFalse(r1.isValid(roles));

            Restriction r2 = new Restriction(1, 10);
            Assert.IsFalse(r2.isValid(roles));

            Restriction r3 = new Restriction(15, 20);
            Assert.IsTrue(r3.isValid(roles));

            Restriction r4 = new Restriction(1);
            Assert.IsTrue(r4.isValid(roles));
        }
    }
}
