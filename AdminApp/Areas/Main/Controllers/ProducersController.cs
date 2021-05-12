using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminApp.Models;

namespace AdminApp.Controllers
{
    public class ProducersController : Controller
    {
        LIMUPAStoreEntities db = new LIMUPAStoreEntities();
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult ProducerPartial(string producer)
        {
            var producerList = db.PRODUCERs.ToList();
            return PartialView(producerList);
        }
    }
}