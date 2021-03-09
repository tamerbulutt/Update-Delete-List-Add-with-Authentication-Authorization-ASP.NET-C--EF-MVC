using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FormWorks.Models.EntityFramework;

namespace FormWorks.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        // GET: Security
        MVCFormWorksEntities db = new MVCFormWorksEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tblUsers user)
        {
            var userAuth = db.tblUsers.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            if (userAuth!=null)
            {
                FormsAuthentication.SetAuthCookie(userAuth.UserName, false);
                return RedirectToAction("List", "Personal");
            }
            else
                return View();

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Security");
        }
    }
}