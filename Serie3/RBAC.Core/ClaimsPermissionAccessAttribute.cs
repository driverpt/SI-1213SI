using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RBAC.Core
{
    public class ClaimsPermissionAccessAttribute : AuthorizeAttribute
    {
        const String userPermissionClaimName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/permissions";
        private readonly String[] permissions;

        public ClaimsPermissionAccessAttribute(params String[] permissions)
        {
            this.permissions = permissions;
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            ClaimsPrincipal claimsPrincipal = (ClaimsPrincipal)HttpContext.Current.User;

            String[] userPermissions = null;
            foreach (Claim claim in claimsPrincipal.Claims)
            {
                if (claim.Type == userPermissionClaimName)
                {
                    userPermissions = claim.Value.Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                }
            }

            if (userPermissions != null)
            {
                foreach (String p in permissions)
                {
                    if (userPermissions.Contains(p))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
