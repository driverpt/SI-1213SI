using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBAC.Core.Entities;
using RBAC.Core.Repository;
using RBAC.Core.Contracts;

namespace RBAC.Core
{
    public static class Factory
    {
        private static readonly RoleRestrictionsRepository roleRestrictionsRepository = new RoleRestrictionsRepository();
        private static readonly PermissionRepository permissionRepository = new PermissionRepository();
        private static readonly RoleRepository roleRepository = new RoleRepository();
        private static readonly UserRepository userRepository = new UserRepository();
        
        public static Role Role(String name)
        {
            return roleRepository.LoadAll().FirstOrDefault((r) => { return r.Name == name; });
        }

        public static User User(String name)
        {
            return userRepository.LoadAll().FirstOrDefault((u) => { return u.Name == name; });
        }

        public static Permission Permission(String name)
        {
            return permissionRepository.LoadAll().FirstOrDefault((p) => { return p.Description == name; });
        }

        public static Session Session(User user, params Role[] roles)
        {
            foreach (IRestriction r in roleRestrictionsRepository.LoadAll())
            {
                if (!r.isValid(roles))
                {
                    return null;
                }
            }

            return new Session(user, roles);
        }

    }
}
