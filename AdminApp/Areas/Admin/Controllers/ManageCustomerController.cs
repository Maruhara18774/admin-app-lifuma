using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminApp.Models;

namespace AdminApp.Controllers
{
    public class ManageCustomerController : Controller
    {
        LIMUPAStoreEntities db = new LIMUPAStoreEntities();
        // GET: Admin/ManageCustomer
        public ActionResult Index()
        {
            return View(db.CUSTOMERs.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new CUSTOMER());
        }

        [HttpPost]
        public ActionResult Create(CUSTOMER newCus)
        {
            try
            {
                db.CUSTOMERs.Add(newCus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(newCus);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var cus = db.CUSTOMERs.Where(a => a.CUSTOMER_ID == id).FirstOrDefault();
            return View(cus);
        }

        [HttpPost]
        public ActionResult Edit(CUSTOMER newCus)
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            var check = db.CUSTOMERs.Where(c => c.CUSTOMER_ID == id).FirstOrDefault();
            try
            {
                check.CUSTOMER_FIRSTNAME = newCus.CUSTOMER_FIRSTNAME;
                check.CUSTOMER_LASTNAME = newCus.CUSTOMER_LASTNAME;
                check.PHONE = newCus.PHONE;
                check.CUSTOMER_ADDRESS = newCus.CUSTOMER_ADDRESS;
                check.ACCOUNT_ID = newCus.ACCOUNT_ID;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                newCus.CUSTOMER_ID = id;
                return View(newCus);
            }
        }
        public ActionResult Delete(int id)
        {
            db.CUSTOMERs.Remove(db.CUSTOMERs.Where(c => c.CUSTOMER_ID == id).FirstOrDefault());
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}