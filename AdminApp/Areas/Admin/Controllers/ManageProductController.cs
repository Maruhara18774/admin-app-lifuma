using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminApp.Models;

namespace AdminApp.Controllers
{
    public class ManageProductController : Controller
    {
        LIMUPAStoreEntities db = new LIMUPAStoreEntities();
        // GET: Admin/ManageProduct
        public ActionResult Index()
        {
            return View(db.PRODUCTs.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            PRODUCT prd = new PRODUCT();
            prd.PRODUCT_IMAGE = "~/Content/Images/bg.jpg";
            prd.lsPRODUCERS = db.PRODUCERs.ToList();
            prd.lsPRODUCTTYPE = db.PRODUCTTYPEs.ToList();
            return View(prd);
        }
        [HttpPost]
        public ActionResult Create(PRODUCT prd)
        {
            try
            {
                if (prd.UploadImage != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(prd.UploadImage.FileName);
                    string extent = Path.GetExtension(prd.UploadImage.FileName);
                    fileName = fileName + extent;
                    prd.PRODUCT_IMAGE = "~/Content/Images/" + fileName;
                    prd.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/"), fileName));
                }
                if (prd.PRODUCT_COUNT <= 0)
                {
                    prd.PRODUCT_COUNT = 0;
                }
                db.PRODUCTs.Add(prd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                prd.lsPRODUCERS = db.PRODUCERs.ToList();
                prd.lsPRODUCTTYPE = db.PRODUCTTYPEs.ToList();
                return View(prd);
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            PRODUCT prd = db.PRODUCTs.Where(p => p.PRODUCT_ID == id).FirstOrDefault();
            prd.lsPRODUCERS = db.PRODUCERs.ToList();
            prd.lsPRODUCTTYPE = db.PRODUCTTYPEs.ToList();
            return View(prd);
        }
        [HttpPost]
        public ActionResult Edit(PRODUCT prd)
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            var check = db.PRODUCTs.Where(p => p.PRODUCT_ID == id).FirstOrDefault();
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(prd.UploadImage.FileName);
                string extent = Path.GetExtension(prd.UploadImage.FileName);
                fileName = fileName + extent;
                prd.PRODUCT_IMAGE = "~/Content/Images/" + fileName;
                if(prd.PRODUCT_IMAGE != check.PRODUCT_IMAGE)
                {
                    prd.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/"), fileName));
                }
                if (prd.PRODUCT_COUNT <= 0)
                {
                    prd.PRODUCT_COUNT = 0;
                }
                check.PRODUCT_NAME = prd.PRODUCT_NAME;
                check.PRICE = prd.PRICE;
                check.COLOR = prd.COLOR;
                check.PRODUCT_IMAGE = prd.PRODUCT_IMAGE;
                check.PRODUCT_DESCRIPTION = prd.PRODUCT_DESCRIPTION;
                check.PRODUCT_CONFIGURATION = prd.PRODUCT_CONFIGURATION;
                check.PRODUCT_COUNT = prd.PRODUCT_COUNT;
                check.CREATE_DATE = prd.CREATE_DATE;
                check.PRODUCER_ID = prd.PRODUCER_ID;
                check.PRODUCTTYPE_ID = prd.PRODUCTTYPE_ID;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                prd.lsPRODUCERS = db.PRODUCERs.ToList();
                prd.lsPRODUCTTYPE = db.PRODUCTTYPEs.ToList();
                return View(prd);
            }
        }
        public ActionResult Delete(int id)
        {
            db.PRODUCTs.Remove(db.PRODUCTs.Where(p => p.PRODUCT_ID == id).FirstOrDefault());
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}