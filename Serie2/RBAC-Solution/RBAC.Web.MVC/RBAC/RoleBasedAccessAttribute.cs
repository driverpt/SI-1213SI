using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RBAC.Web.MVC.RBAC
{
    public class RoleBasedAccessAttribute : AuthorizeAttribute
    {


        private readonly String[] permissions;

        public RoleBasedAccessAttribute(params String[] permissions)
        {
            this.permissions = permissions;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            String userName = HttpContext.Current.User.Identity.Name;

            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            String userName = HttpContext.Current.User.Identity.Name;

            base.HandleUnauthorizedRequest(filterContext);
        }

        public override bool Match(object obj)
        {
            String userName = HttpContext.Current.User.Identity.Name;

            return base.Match(obj);
        }
    }
}