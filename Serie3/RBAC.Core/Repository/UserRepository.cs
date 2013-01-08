using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using RBAC.Core.Entities;

namespace RBAC.Core.Repository
{
    public class UserRepository : Repository<User>
    {

        public UserRepository() : base("Users.xml")
        {
      
        }

        public override List<User> LoadAll()
        {
            List<User> users = new List<User>();
            XElement root = document.Root;
            foreach (XElement elem in root.Descendants())
            {
                users.Add(new Entities.User() { Id = Int32.Parse(elem.Attribute("Id").Value), Name=elem.Attribute("Name").Value });
            }
            return users;
        }

        public User LoadById(Int32 userId)
        {
            XElement root = document.Root;
            foreach (XElement elem in root.Descendants())
            {
                Int32 id = Int32.Parse(elem.Attribute("Id").Value);
                String name = elem.Attribute("Name").Value;
                
                if(id == userId)
                {
                    return new User() { Id = id, Name = name};
                }
            }
            return null;
        }
    }
}
