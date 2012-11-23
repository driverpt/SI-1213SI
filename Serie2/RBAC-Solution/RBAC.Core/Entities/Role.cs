using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBAC.Core.Repository;

namespace RBAC.Core.Entities
{
    public class Role : Model
    {
        private readonly RoleHierarchyRepository roleHierarchyRepository;

        public Role()
        {
            this.roleHierarchyRepository = new RoleHierarchyRepository();
        }

        public Int32 Id { get; set; }
        public String Name { get; set; }

        public List<Role> Childs
        {
            get
            {
                return roleHierarchyRepository.LoadChildRolesByRoleId(Id);
            }
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            Role r = obj as Role;

            if (r == null)
                return false;

            return r.Id == Id && r.Name == Name;
        }
    }
}
