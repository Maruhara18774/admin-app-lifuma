using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminApp.Models;

namespace AdminApp.Controllers
{
    public class WishlistController : Controller
    {
        LIMUPAStoreEntities db = new LIMUPAStoreEntities();
        // GET: Wishlist
        public ActionResult Index()
        {
            if (Session["ACCOUNT"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            List<PRODUCT> listProduct = DanhSachYeuThich();          
            return View(listProduct);
        }
        public List<PRODUCT> DanhSachYeuThich()
        {
            ACCOUNT account = Session["ACCOUNT"] as ACCOUNT;
            // Lấy hết các dòng có ACCOUNT ID trùng với ID của Session
            var favs = db.FAVORITEs.OrderByDescending(s => s.FAVORITE_ID).Where(s => s.ACCOUNT_ID == account.ACCOUNT_ID);
            // Tạo danh sách sản phẩm
            List<PRODUCT> listProduct = new List<PRODUCT>();
            // Vòng lặp từng ID sản phẩm yêu thích
            foreach (var fav in favs)
            {
                // Tìm sản phẩm có trùng ID với sản phẩm yêu thích
                var product = db.PRODUCTs.FirstOrDefault(s => s.PRODUCT_ID == fav.PRODUCT_ID);
                // Thêm vào danh sách
                listProduct.Add(product);
            }
            return (listProduct);
        }
        public ActionResult WishlistPartial()
        {
            if (Session["ACCOUNT"] == null)
            {
                return PartialView();
            }
            List<PRODUCT> listProduct = DanhSachYeuThich();
            ViewBag.Count = listProduct.Count();
            return PartialView();
        }

        public ActionResult ThemYeuThich(int id)
        {
            // Nếu chưa đăng nhập thì phải đăng nhập mới xem được danh sách yêu thích
            if (Session["ACCOUNT"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ACCOUNT account = Session["ACCOUNT"] as ACCOUNT;
            FAVORITE fav = new FAVORITE();
            fav.ACCOUNT_ID = account.ACCOUNT_ID;
            fav.PRODUCT_ID = id;
            // Kiểm tra sản phẩm đã trong danh sách yêu thích của khách hàng chưa
            FAVORITE check = db.FAVORITEs.Where(s => s.ACCOUNT_ID == account.ACCOUNT_ID && s.PRODUCT_ID == id).SingleOrDefault();
            // Không tồn tài sản phẩm thì thêm
            if (check == null)
            {
                db.FAVORITEs.Add(fav);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Wishlist");
        }

        public ActionResult XoaYeuThich(int id)
        {
            // Kiểm tra đã đăng nhâp
            if (Session["ACCOUNT"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Kiểm tra sản phẩm có tồn tại trong bảng FAVORITE hay không
            ACCOUNT account = Session["ACCOUNT"] as ACCOUNT;
            FAVORITE check = db.FAVORITEs.Where(s => s.ACCOUNT_ID == account.ACCOUNT_ID && s.PRODUCT_ID == id).SingleOrDefault();
            if (check == null)
            {
                // Trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }

            // Xóa item khỏi ds yêu thích
            db.FAVORITEs.Remove(check);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}