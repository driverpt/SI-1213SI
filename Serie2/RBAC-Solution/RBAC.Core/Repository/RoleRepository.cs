using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBAC.Core.Entities;
using System.Xml.Linq;

namespace RBAC.Core.Repository
{
    public class RoleRepository : Repository<Role>
    {

        public RoleRepository() 
            : base("Roles.xml")
        {

        }

        public override List<Role> LoadAll()
        {
            List<Role> roles = new List<Role>();

            XElement root = document.Root;
            foreach (XElement elem in root.Descendants())
            {
                roles.Add(
                    new Role() 
                    { 
                        Id = Int32.Parse(elem.Attribute("Id").Value), 
                        Name = elem.Attribute("Name").Value 
                    });
            }
            return roles;
        }                                                              
        public Role LoadById(Int32 roleId)
        {
            XElement root = document.Root;
            foreach (XElement elem in root.Descendants())
            {
                Int32 id = Int32.Parse(elem.Attribute("Id").Value);
                String name = elem.Attribute("Name").Value;

                if (roleId == id)
                {
                    return new Role() { Id = Int32.Parse(elem.Attribute("Id").Value), Name = name };
                }
            }
            return null;
        }
    }
}
