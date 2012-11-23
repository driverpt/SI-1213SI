using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace RBAC.Core.Attributes
{
    public class PermissionAccessControlAttribute : AuthorizeAttribute
    {

        private readonly String[] permissions;

        public PermissionAccessControlAttribute(params String[] permissions)
        {
            this.permissions = permissions;
        }


        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            String userName = httpContext.User.Identity.Name;



            return base.AuthorizeCore(httpContext);
        }

    }


}
