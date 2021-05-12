using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminApp.Models;

namespace AdminApp.Controllers
{
    public class ProductController : Controller
    {
        LIMUPAStoreEntities db = new LIMUPAStoreEntities();
        // GET: Product
        [HttpGet]
        public ActionResult ListProduct(string producerName)
        {
            if (producerName == "All")
            {
                var productList = db.PRODUCTs.OrderByDescending(x => x.PRODUCT_NAME);
                return View(productList);
            }
            else
            {
                // Lấy Producer ID dựa trên Producer Name nhận được
                var producerID = db.PRODUCERs.Where(x => x.PRODUCER_NAME == producerName).SingleOrDefault().PRODUCER_ID;
                var productList = db.PRODUCTs.OrderByDescending(x => x.PRODUCT_NAME).Where(x => x.PRODUCER_ID == producerID);
                ViewBag.Producer = producerName;
                return View(productList);
            }
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            // Lấy chi tiết sản phẩm theo ID
            var product = db.PRODUCTs.SingleOrDefault(s => s.PRODUCT_ID == id);
            if (product == null)
            {
                return new HttpNotFoundResult();
            }
            var producer = db.PRODUCERs.SingleOrDefault(s => s.PRODUCER_ID == product.PRODUCER_ID).PRODUCER_NAME;
            ViewBag.Producer = producer;
            ViewBag.Detail = product;
            return View(product);
        }
    }
}