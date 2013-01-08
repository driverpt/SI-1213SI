using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBAC.Core.Entities;
using System.Xml.Linq;

namespace RBAC.Core.Repository
{
    public class RoleRestrictionsRepository : Repository<Restriction>
    {

        public RoleRestrictionsRepository()
            : base("RoleRestrictions.xml")
        {

        }

        public override List<Restriction> LoadAll()
        {
            List<Restriction> restrictions = new List<Restriction>();

            XElement root = document.Root;
            foreach (XElement elem in root.Descendants("Restriction"))
            {
                List<Int32> roleIds = new List<Int32>();

                IEnumerable<XElement> roles = elem.Descendants("Role");

                foreach (XElement r in roles)
                {
                    roleIds.Add(Int32.Parse(r.Attribute("Id").Value));
                }

                restrictions.Add(new Restriction(roleIds.ToArray<Int32>()));
            }

            return restrictions;
        }
    }
}
