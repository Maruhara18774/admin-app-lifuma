using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AdminApp.Models;

namespace AdminApp.Controllers
{
    public class AccountController : Controller
    {
        LIMUPAStoreEntities db = new LIMUPAStoreEntities();
        // GET: Login
        public ActionResult Login()
        {
            ACCOUNT _account = new ACCOUNT();
            return View(_account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ACCOUNT _account)
        {
            //if (!ModelState.IsValid)
            //{
            //var f_password = GetMD5(_account.ACCOUNT_PASSWORD);
            //var check = db.ACCOUNTs.Where(s => s.EMAIL == _account.EMAIL && s.ACCOUNT_PASSWORD == f_password).FirstOrDefault();
            var check = db.ACCOUNTs.Where(s => s.EMAIL == _account.EMAIL && s.ACCOUNT_PASSWORD == _account.ACCOUNT_PASSWORD).FirstOrDefault();
            if (check != null)
                {
                    if(check.ROLE.ROLE_NAME == "Admin")
                {
                    return RedirectToRoute(new { area = "Admin", controller = "ManageAccount", action = "ListAccount" });
                }
                    db.Configuration.ValidateOnSaveEnabled = false;
                    //add session
                    Session["FULLNAME"] = _account.ACCOUNT_FIRSTNAME + " " + _account.ACCOUNT_LASTNAME;
                    Session["EMAIL"] = _account.EMAIL;
                    Session["ACCOUNT_ID"] = _account.ACCOUNT_ID;
                    // Lấy dữ liệu Account
                    ACCOUNT accProfile = db.ACCOUNTs.FirstOrDefault(s => s.EMAIL == _account.EMAIL);
                    Session["ACCOUNT"] = accProfile;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Your email or password is not correct!";
                    return View(_account);
                }
            //}
            //return View(_account);
        }
    
        public ActionResult Register()
        {
            ACCOUNT _account = new ACCOUNT();
            return View(_account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(ACCOUNT _account)
        {
            if (ModelState.IsValid)
            {
                var check = db.ACCOUNTs.FirstOrDefault(s => s.EMAIL == _account.EMAIL);
                if (check == null) // Email chưa tồn tại
                {
                   _account.ACCOUNT_PASSWORD = GetMD5(_account.ACCOUNT_PASSWORD);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.ACCOUNTs.Add(_account);
                    db.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                }
            }
            return View(_account);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        //create a string MD5
        public static string GetMD5(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

    }
}