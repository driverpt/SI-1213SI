using RBAC.Core;
using RP.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace RP.MVC.Controllers
{
    public class HomeController : Controller
    {
 
        public ActionResult Index()
        {
            ClaimsPrincipal claimsPrincipal = (ClaimsPrincipal)HttpContext.User;

            ClaimsPrincipalModel model = new ClaimsPrincipalModel();

            List<String> claimsList = new List<String>();
            foreach (Claim c in claimsPrincipal.Claims)
            {
                claimsList.Add(c.Value);
            }

            model.Claims = claimsList.ToArray<String>();

            return View(model);
        }

        [ClaimsPermissionAccess("Permission 1", "Permission 3")]
        public ActionResult Configuration()
        {

            return View();
        }

        [ClaimsPermissionAccess("Permission 4")]
        public ActionResult List()
        {

            return View();
        }

    }
}
