using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBAC.Core.Contracts;

namespace RBAC.Core.Entities
{
    public class Session
    {
        private readonly DateTime sessionInitTime;
        private readonly User user;
        private readonly Role[] roles;

        public Session(User user, Role[] roles)
        {
            this.user = user;
            this.roles = roles;
            this.sessionInitTime = DateTime.Now;
        }

        public Int32 Duration 
        { 
            get 
            { 
                TimeSpan timeSpan = DateTime.Now - sessionInitTime;
                return timeSpan.Minutes;
            } 
        }
        public User User { get { return user; } }
        public Role[] Roles { get { return roles; } }

    }
}
