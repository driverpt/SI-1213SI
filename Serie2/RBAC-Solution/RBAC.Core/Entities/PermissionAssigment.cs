using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBAC.Core.Repository;

namespace RBAC.Core.Entities
{
    public class PermissionAssigment
    {
        private readonly RoleRepository roleRepository;
        private readonly PermissionRepository permissionRepository;

        private readonly Int32 roleId;
        private readonly Int32 permissionId;

        public PermissionAssigment(Int32 roleId, Int32 permissionId)
        {
            this.roleId = roleId;
            this.permissionId = permissionId;

            this.roleRepository = new RoleRepository();
            this.permissionRepository = new PermissionRepository();
        }

        public Role Role
        {
            get
            {
                return roleRepository.LoadById(roleId);
            }
        }

        public Permission Permission
        {
            get
            {
                return permissionRepository.LoadById(permissionId);
            }
        }

    }
}
