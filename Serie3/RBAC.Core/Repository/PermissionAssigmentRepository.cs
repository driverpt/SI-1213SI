using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBAC.Core.Entities;
using System.Xml.Linq;

namespace RBAC.Core.Repository
{
    public class PermissionAssigmentRepository : Repository<PermissionAssigment>
    {

        public PermissionAssigmentRepository() 
            : base("PermissionAssigment.xml")
        {

        }

        public override List<PermissionAssigment> LoadAll()
        {
            List<PermissionAssigment> pa = new List<PermissionAssigment>();

            XElement root = document.Root;
            foreach (XElement elem in root.Descendants())
            {
                Int32 userId = Int32.Parse(elem.Attribute("RoleId").Value);
                Int32 roleId = Int32.Parse(elem.Attribute("PermissionId").Value);
                pa.Add(new PermissionAssigment(userId, roleId));
            }

            return pa;
        }

        public List<PermissionAssigment> LoadByRoleId(Int32 id)
        {
            List<PermissionAssigment> pa = new List<PermissionAssigment>();

            XElement root = document.Root;
            foreach (XElement elem in root.Descendants())
            {
                Int32 roleId = Int32.Parse(elem.Attribute("RoleId").Value);
                Int32 permissionId = Int32.Parse(elem.Attribute("PermissionId").Value);

                if (id == roleId)
                {
                    pa.Add(new PermissionAssigment(roleId, permissionId));
                }
            }

            return pa;
        }
    }
}
