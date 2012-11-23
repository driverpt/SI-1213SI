using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBAC.Core.Entities;
using System.Xml.Linq;

namespace RBAC.Core.Repository
{
    public class PermissionRepository : Repository<Permission>
    {

        public PermissionRepository()
            : base("Permission.xml")
        {

        }

        public override List<Permission> LoadAll()
        {
            List<Permission> permissions = new List<Permission>();

            XElement root = document.Root;
            foreach (XElement elem in root.Descendants())
            {
                permissions.Add(new Permission() { Id = Int32.Parse(elem.Attribute("Id").Value), Description = elem.Attribute("Description").Value });
            }

            return permissions;
        }

        public Permission LoadById(Int32 key)
        {
            XElement root = document.Root;
            foreach (XElement elem in root.Descendants())
            {
                Int32 id = Int32.Parse(elem.Attribute("Id").Value);
                String description = elem.Attribute("Description").Value;

                if(id == key)
                {
                    return new Permission() { Id = Int32.Parse(elem.Attribute("Id").Value), Description = elem.Attribute("Description").Value };
                }
            }

            return null;
        }
    }
}
