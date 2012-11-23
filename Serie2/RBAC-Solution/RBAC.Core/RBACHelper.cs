using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBAC.Core.Entities;
using RBAC.Core.Repository;

namespace RBAC.Core
{
    public class RBACHelper
    {
        private readonly PermissionAssigmentRepository permissionAssigmentRepository;
        private readonly UserAssigmentRepository userAssigmentRepository;
        private readonly UserRepository userRepository;
        private readonly RoleRepository roleRepository;

        public RBACHelper()
        {
            permissionAssigmentRepository = new PermissionAssigmentRepository();
            userAssigmentRepository = new UserAssigmentRepository();
            userRepository = new UserRepository();
            roleRepository = new RoleRepository();
        }

        public Boolean IsUserInRole(Int32 userId, Int32 roleId)
        {
            List<Role> roles = GetUserRolesHierarchy(userId);

            return roles.Count((r) => { return r.Id == roleId; }) != 0;
        }

        public Boolean UserIsAllowed(Session userSession, Permission permission)
        {

            // Check if all roles in session are assigned to user

            List<Role> allUserRoles = null;
            try
            {
                allUserRoles = GetUserRolesHierarchy(userSession.User.Id);

            }
            catch (Exception ex)
            {

            }

            foreach (Role role in userSession.Roles)
            {
                if (allUserRoles.FirstOrDefault((r) => { return r.Id == role.Id;}) == null)
                {
                    return false;
                }
            }

            // Check if roles in session have access to the permission
            foreach(Role role in userSession.Roles)
            {
                foreach (Role childRole in GetAllChildRoles(role))
                {
                    // Checking if we have the pair Role <-> Permission
                    foreach (PermissionAssigment pa in permissionAssigmentRepository.LoadByRoleId(role.Id))
                    {
                        if (pa.Permission.Id == permission.Id)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public List<Role> GetUserRolesHierarchy(Int32 userId)
        {
            List<Role> roles = new List<Role>();

            List<UserAssigment> userRoles = userAssigmentRepository.LoadByUserId(userId);

            foreach (UserAssigment ua in userRoles)
            {
                GetRolesRecursive(ua.Role, ref roles);
            }
            return roles.Distinct<Role>().ToList();
        }

        private List<Role> GetAllChildRoles(Role[] roles)
        {
            List<Role> allRoles = new List<Role>();

            foreach(Role r in roles)
            {
                GetRolesRecursive(r, ref allRoles);
            }

            return allRoles;
        }

        private List<Role> GetAllChildRoles(Role role)
        {
            List<Role> roles = new List<Role>();

            GetRolesRecursive(role, ref roles);

            return roles;
        }

        private Role GetRolesRecursive(Role currentRole, ref List<Role> roles)
        {
            roles.Add(currentRole);

            foreach (Role role in currentRole.Childs)
            {
                GetRolesRecursive(role, ref roles);
            }

            return null;
        }

    }
}
