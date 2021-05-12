using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminApp.Models;

namespace AdminApp.Controllers
{
    public class ManageAccountController : Controller
    {
        LIMUPAStoreEntities db = new LIMUPAStoreEntities();
        // GET: Account
        public ActionResult Index()
        {
            return RedirectToAction("ListAccount");
        }

        public ActionResult ListAccount()
        {
            return View(db.ACCOUNTs.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ACCOUNT emptyAcc = new ACCOUNT();
            emptyAcc.lsMEMBERS = db.MEMBERs.ToList();
            emptyAcc.lsROLES = db.ROLES.ToList();
            return View(emptyAcc);
        }

        [HttpPost]
        public ActionResult Create(ACCOUNT newAccount)
        {
            var check = db.ACCOUNTs.Where(acc => acc.EMAIL == newAccount.EMAIL).FirstOrDefault();
            if(check== null)
            {
                try
                {
                    db.ACCOUNTs.Add(newAccount);
                    db.SaveChanges();
                    return RedirectToAction("ListAccount");
                }
                catch (Exception)
                {
                    newAccount.lsMEMBERS = db.MEMBERs.ToList();
                    newAccount.lsROLES = db.ROLES.ToList();
                    return View(newAccount);
                }
            }
            newAccount.lsMEMBERS = db.MEMBERs.ToList();
            newAccount.lsROLES = db.ROLES.ToList();
            return View(newAccount);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var acc = db.ACCOUNTs.Where(a => a.ACCOUNT_ID == id).FirstOrDefault();
            acc.lsMEMBERS = db.MEMBERs.ToList();
            acc.lsROLES = db.ROLES.ToList();
            return View(acc);
        }

        [HttpPost]
        public ActionResult Edit(ACCOUNT newAccount)
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            var check = db.ACCOUNTs.Where(acc => acc.EMAIL == newAccount.EMAIL && acc.ACCOUNT_ID!=id).FirstOrDefault();
            if (check == null)
            {
                var thisObj = db.ACCOUNTs.Where(acc => acc.ACCOUNT_ID == id).FirstOrDefault();
                newAccount.ACCOUNT_ID = id;
                try
                {
                    thisObj.ACCOUNT_FIRSTNAME = newAccount.ACCOUNT_FIRSTNAME;
                    thisObj.ACCOUNT_LASTNAME = newAccount.ACCOUNT_LASTNAME;
                    thisObj.EMAIL = newAccount.EMAIL;
                    thisObj.ACCOUNT_PASSWORD = newAccount.ACCOUNT_PASSWORD;
                    thisObj.PHONE = newAccount.PHONE;
                    thisObj.ACCOUNT_ADDRESS = newAccount.ACCOUNT_ADDRESS;
                    thisObj.ROLE_ID = newAccount.ROLE_ID;
                    thisObj.MEMBER_ID = newAccount.MEMBER_ID;
                    db.SaveChanges();
                    return RedirectToAction("ListAccount");
                }
                catch (Exception)
                {
                    newAccount.lsMEMBERS = db.MEMBERs.ToList();
                    newAccount.lsROLES = db.ROLES.ToList();
                    return View(newAccount);
                }
            }
            newAccount.lsMEMBERS = db.MEMBERs.ToList();
            newAccount.lsROLES = db.ROLES.ToList();
            return View(newAccount);
        }
        public ActionResult Delete(int id)
        {
            db.ACCOUNTs.Remove(db.ACCOUNTs.Where(acc => acc.ACCOUNT_ID == id).FirstOrDefault());
            db.SaveChanges();
            return RedirectToAction("ListAccount");
        }
    }
}