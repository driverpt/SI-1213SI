using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBAC.Core.Repository;

namespace RBAC.Core.Entities
{
    public class UserAssigment
    {
        private readonly UserRepository userRepository;
        private readonly RoleRepository roleRepository;

        private readonly Int32 userId;
        private readonly Int32 roleId;

        public UserAssigment(Int32 userId, Int32 roleId)
        {
            this.userId = userId;
            this.roleId = roleId;

            this.userRepository = new UserRepository();
            this.roleRepository = new RoleRepository();
        }

        public User User 
        { 
            get
            {
                return userRepository.LoadById(userId);   
            } 
        }
        public Role Role 
        {
            get
            {
                return roleRepository.LoadById(roleId);
            }
        }

    }
}
