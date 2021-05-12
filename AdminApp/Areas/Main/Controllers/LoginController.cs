using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminApp.Models;

namespace AdminApp.Controllers
{
    public class LoginController : Controller
    {
        LIMUPAStoreEntities db = new LIMUPAStoreEntities();
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(ACCOUNT inputAccount)
        {
            var check = db.ACCOUNTs.Where(acc => acc.EMAIL == inputAccount.EMAIL&& acc.ACCOUNT_PASSWORD == inputAccount.ACCOUNT_PASSWORD).FirstOrDefault();
            if(check == null)
            {
                return View(inputAccount);
            }
            if(check.ROLE.ROLE_NAME == "Admin")
            {
                return RedirectToRoute(new { area = "Admin", controller = "ManageAccount", action = "ListAccount" });
            }
            return RedirectToRoute(new { area = "Customer", controller = "Login", action = "Login" });
        }
    }
}