using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RBAC.Web.MVC.RBAC;

namespace RBAC.Web.MVC.Controllers
{
    public class HomeController : Controller
    {

        [RoleBasedAccess("Permission1", "Permission2")]
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            String userName = User.Identity.Name;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
