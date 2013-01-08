using RBAC.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RBAC.Core
{
    public class PermissionAccessAttribute : AuthorizeAttribute
    {
        private readonly RBACHelper helper;
        private readonly String[] permissions;

        public PermissionAccessAttribute(params String[] permissions)
        {
            this.helper = new RBACHelper();
            this.permissions = permissions;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            String userName = HttpContext.Current.User.Identity.Name;

            if (!String.IsNullOrEmpty(userName))
            {
                User user = Factory.User(userName);

                List<Role> roles = ParseClaimsRoles((ClaimsPrincipal)httpContext.User);
                
                if (roles != null && roles.Count != 0)
                {
                    foreach (String p in permissions)
                    {
                        Permission permission = Factory.Permission(p);

                        if (helper.UserIsAllowed(Factory.Session(user, roles.ToArray()), permission))
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            return false;
        }

        private List<Role> ParseClaimsRoles(ClaimsPrincipal claimsPrincipal)
        {
            List<Role> roles = new List<Role>();

            String[] roleNames = new String[0];
            
            foreach (Claim claim in claimsPrincipal.Claims)
            {
                if (claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/role")
                {
                    roleNames = claim.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            
            foreach (String r in roleNames)
            {
                roles.Add(Factory.Role(r));
            }

            return roles;
        }
    }
}
