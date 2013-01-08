using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBAC.Core.Entities;
using System.Xml.Linq;

namespace RBAC.Core.Repository
{
    public class UserAssigmentRepository : Repository<UserAssigment>
    {

        public UserAssigmentRepository()
            : base("UserAssigment.xml")
        {

        }

        public override List<UserAssigment> LoadAll()
        {
            List<UserAssigment> ua = new List<UserAssigment>();

            XElement root = document.Root;
            foreach (XElement elem in root.Descendants())
            {
                Int32 userId = Int32.Parse(elem.Attribute("UserId").Value);
                Int32 roleId = Int32.Parse(elem.Attribute("RoleId").Value);
                ua.Add(new UserAssigment(userId, roleId));
            }
            return ua;
        }

        public List<UserAssigment> LoadByUserId(Int32 id)
        {
            List<UserAssigment> ua = new List<UserAssigment>();

            XElement root = document.Root;
            foreach (XElement elem in root.Descendants())
            {
                Int32 userId = Int32.Parse(elem.Attribute("UserId").Value);
                Int32 roleId = Int32.Parse(elem.Attribute("RoleId").Value);

                if (id == userId)
                {
                    ua.Add(new UserAssigment(userId, roleId));
                }
            }
            return ua;
        }
    }
}
