using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminApp.Models;

namespace AdminApp.Controllers
{
    public class ManageRoleController : Controller
    {
        LIMUPAStoreEntities db = new LIMUPAStoreEntities();
        // GET: Admin/ManageRole
        public ActionResult Index()
        {
            return View(db.ROLES.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new ROLE());
        }

        [HttpPost]
        public ActionResult Create(ROLE newRole)
        {
            var check = db.ROLES.Where(r => r.ROLE_NAME == newRole.ROLE_NAME).FirstOrDefault();
            if(check == null)
            {
                db.ROLES.Add(newRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newRole);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var r = db.ROLES.Where(a => a.ROLE_ID == id).FirstOrDefault();
            return View(r);
        }

        [HttpPost]
        public ActionResult Edit(ROLE newRole)
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            var checkName = db.ROLES.Where(r => r.ROLE_NAME == newRole.ROLE_NAME).FirstOrDefault();
            if(checkName == null)
            {
                var check = db.ROLES.Where(r => r.ROLE_ID == id).FirstOrDefault();
                check.ROLE_NAME = newRole.ROLE_NAME;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            newRole.ROLE_ID = id;
            return View(newRole);
        }
        public ActionResult Delete(int id)
        {
            db.ROLES.Remove(db.ROLES.Where(r => r.ROLE_ID == id).FirstOrDefault());
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}