using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminApp.Models;

namespace AdminApp.Controllers
{
    public class HomeController : Controller
    {
        LIMUPAStoreEntities db = new LIMUPAStoreEntities();
        public ActionResult Index()
        {
            return View(db.PRODUCTs.ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult GetNewArrival()
        {
            List<PRODUCT> ls = db.PRODUCTs.OrderByDescending(s => s.CREATE_DATE).ToList();
            //int n = ls.Count;
            //List<PRODUCT> result = new List<PRODUCT>();
            //for (int i = 0; i < n; i++)
            //{
                //PRODUCT searching = getMaxDateCreate(ls);
                //result.Add(searching);
                //ls.Remove(searching);
            //}
            return PartialView(ls);
        }

        //private PRODUCT getMaxDateCreate(List<PRODUCT> data)
        //{
        //    DateTime max = new DateTime(getYear(data[0].CREATE_DATE), getMonth(data[0].CREATE_DATE), getDay(data[0].CREATE_DATE));
        //    string key = data[0].PRODUCT_ID;
        //    foreach(var item in data)
        //    {
        //        DateTime temp = new DateTime(getYear(item.CREATE_DATE), getMonth(item.CREATE_DATE), getDay(item.CREATE_DATE));
        //        int result = DateTime.Compare(max, temp);
        //        if (result > 0)
        //        {
        //            max = temp;
        //            key = item.PRODUCT_ID;
        //        }
        //    }
        //    return data.Find(x => x.PRODUCT_ID == key);
        //}
        /*public ActionResult GetBestSeller()
        {
           //   MINDSET - Best seller trong năm nay
           //   *Loop 1: Lấy bộ id và count-- > Danh sách sản phẩm
           //   *Loop 2: Count số lượng các hàng hóa đã bán trong năm


            int year = DateTime.Now.Year;
            List<int> count = new List<int>();
            List<string> id = new List<string>();
            foreach (var item in db.PRODUCTs.ToList())
            {
                count.Add(0);
                id.Add(item.PRODUCT_ID);
            }
            foreach (var item in db.CARTDETAILs)
            {
                // Convert part
                int yearCart = getYear(item.CART.CREATE_DATE);
                if (yearCart > year)
                {
                    int key = id.FindIndex(x => x == item.PRODUCT_ID);
                    count[key]++;
                }
            }
            List<PRODUCT> result = new List<PRODUCT>();
            for (int i = 0; i < id.Count; i++)
            {
                if (i == 7)
                {
                    break;
                }
                int key = findMax(count);
                var currentProduct = db.PRODUCTs.Where(p => p.PRODUCT_ID == id[key]).FirstOrDefault();
                result.Add(currentProduct);
                count.RemoveAt(key);
                id.RemoveAt(key);
            }
            return PartialView(result);
        }*/
        private int getYear(string date)
        {
            return int.Parse(date.Substring(6, 4));
        }
        private int getDay(string date)
        {
            return int.Parse(date.Substring(0, 2));
        }
        private int getMonth(string date)
        {
            return int.Parse(date.Substring(3, 2));
        }
        private int findMax(List<int> data)
        {
            int max = 0;
            int location = 0;
            for(int i =0;i<data.Count;i++)
            {
                if (data[i] > max)
                {
                    max = data[i];
                    location = i;
                }
            }
            return location;
        }
        public ActionResult GetFeatureProducts()
        {
            List<PRODUCT> result = new List<PRODUCT>();
            foreach(var item in db.FEATUREs)
            {
                result.Add(item.PRODUCT);
            }
            return PartialView(result);
        }
    }
}