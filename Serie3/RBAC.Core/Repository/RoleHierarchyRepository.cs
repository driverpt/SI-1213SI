using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBAC.Core.Entities;
using System.Xml.Linq;
using System.Xml;

namespace RBAC.Core.Repository
{
    public class RoleHierarchyRepository : Repository<Role>
    {

        private readonly RoleRepository roleRepository;

        public RoleHierarchyRepository()
            : base("RoleHierarchy.xml")
        {
            this.roleRepository = new RoleRepository();
        }

        public override List<Role> LoadAll()
        {
            throw new NotImplementedException();
        }

        public List<Role> LoadChildRolesByRoleId(Int32 roleId)
        {
            List<Role> childRoles = new List<Role>();

            XElement root = document.Root;
            foreach (XElement elem in root.Descendants("RoleChilds"))
            {
                Int32 id = Int32.Parse(elem.Attribute("Id").Value);

                if (id == roleId)
                {
                    IEnumerable<XElement> childElements = elem.Descendants("Role");

                    foreach (XElement r in childElements)
                    {
                        Int32 childId = Int32.Parse(r.Attribute("Id").Value);
                        childRoles.Add(roleRepository.LoadById(childId));
                    }
                }
            }
            return childRoles;
        }

    }
}
