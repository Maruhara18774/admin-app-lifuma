using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminApp.Models;

namespace AdminApp.Controllers
{
    public class CartController : Controller
    {
        LIMUPAStoreEntities db = new LIMUPAStoreEntities();
        // Lấy giỏ hàng
        public List<ItemGioHang> LayGioHang()
        {
            // Giỏ hàng đã tồn tại
            List<ItemGioHang> lstGioHang = Session["Cart"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                // Nếu Session chưa tồn tại thì khởi tạo giỏ hàng
                lstGioHang = new List<ItemGioHang>();
                Session["Cart"] = lstGioHang;
                return lstGioHang;
            }
            return lstGioHang;
        }

        public ActionResult ThemSanPham(int id, string strURL)
        {
            PRODUCT product = db.PRODUCTs.SingleOrDefault(s => s.PRODUCT_ID == id);
            if (product == null)
            {
                // Trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }

            // Lấy giỏ hàng
            List<ItemGioHang> lstGioHang = LayGioHang();

            // Nếu sản phâm đã tồn tại trong giỏ hàng
            ItemGioHang check = lstGioHang.SingleOrDefault(s => s.MaSP == id); 
            if (check != null)
            {
                // Kiểm tra số lượng tồn sản phẩm
                if (product.PRODUCT_COUNT< check.SoLuong)
                {
                    return View("ThongBao");
                }
                check.SoLuong++;
                check.ThanhTien = check.SoLuong * check.DonGia;
                return Redirect(strURL);
            }
            ItemGioHang item = new ItemGioHang(id);
            if (product.PRODUCT_COUNT < item.SoLuong)
            {
                return View("ThongBao");
            }
            lstGioHang.Add(item);
            return Redirect(strURL);
        }
        // Tính tổng số lượng
        public double TongSoLuong()
        {
            // Lấy giỏ hàng
            List<ItemGioHang> lstGioHang = Session["Cart"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(s => s.SoLuong);
        }
        // Tính tổng tiền
        public decimal TongTien()
        {
            // Lấy giỏ hàng
            List<ItemGioHang> lstGioHang = Session["Cart"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(s => s.ThanhTien);
        }

        // Xem giỏ hàng
        public ActionResult Index() 
        {
            // Lấy giỏ hàng
            List<ItemGioHang> lstGioHang = LayGioHang();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);      
        }

        [ChildActionOnly]
        public ActionResult GioHangPartial()
        {
            // Lấy giỏ hàng
            List<ItemGioHang> lstGioHang = LayGioHang();
            if (TongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView(lstGioHang);
        }
        
        // Chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang(int id)
        {
            // Kiểm tra giỏ hàng đã tồn tại chưa
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Kiểm tra sản phẩm có tồn tại trong CSDL hay không
            PRODUCT product = db.PRODUCTs.FirstOrDefault(s => s.PRODUCT_ID == id);
            if (product == null)
            {
                // Trang đường dẫn không hợp lệ
                Response.StatusCode = 404;  
                return null;
            }

            // Lấy list giỏ hàng từ session
            List <ItemGioHang> lstGioHang = LayGioHang();
            ItemGioHang check = lstGioHang.SingleOrDefault(s => s.MaSP == id);
            if (check == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Lấy list giỏ hàng tạo giao diện
            ViewBag.GioHang = lstGioHang;

            // Nếu tồn tại sản phẩm
            return View(check);
        }

        // Cập nhật giỏ hàng
        [HttpPost]
        public ActionResult CapNhatGioHang(ItemGioHang itemGH)
        {
            // Kiểm tra số lượng tồn
            PRODUCT check = db.PRODUCTs.SingleOrDefault(s => s.PRODUCT_ID == itemGH.MaSP);
            if (check.PRODUCT_COUNT < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            // Cập nhật số lượng sản phẩm trong giỏ hàng
            // B1: Lấy list giỏ hàng từ session["Cart"]
            List<ItemGioHang> lstGH = LayGioHang();
            // B2: Lấy sản phẩm cần cập nhật trong list
            ItemGioHang itemGHUpdate = lstGH.Find(s => s.MaSP == itemGH.MaSP);
            // B3: Cập nhập lại số lượng sản phẩm và thành tiền
            itemGHUpdate.SoLuong = itemGH.SoLuong;
            itemGHUpdate.ThanhTien = itemGHUpdate.SoLuong * itemGHUpdate.DonGia;
            return RedirectToAction("Index");
        }

        // Xóa giỏ hàng
        public ActionResult XoaGioHang(int id)
        {
            // Kiểm tra giỏ hàng đã tồn tại chưa
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Kiểm tra sản phẩm có tồn tại trong CSDL hay không
            PRODUCT product = db.PRODUCTs.FirstOrDefault(s => s.PRODUCT_ID == id);
            if (product == null)
            {
                // Trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }

            // Lấy list giỏ hàng từ session
            List<ItemGioHang> lstGioHang = LayGioHang();
            ItemGioHang check = lstGioHang.SingleOrDefault(s => s.MaSP == id);
            if (check == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Xóa item trong giỏ hàng
            lstGioHang.Remove(check);
            return RedirectToAction("Index");
        }

        public ActionResult DatHang()
        {
            // Kiểm tra giỏ hàng đã tồn tại chưa
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Lấy list giỏ hàng từ session
            List<ItemGioHang> lstGioHang = LayGioHang();
            ViewBag.TongTien = TongTien();
            // Khách hàng chưa đăng nhập đặt hàng
            if (Session["ACCOUNT"] == null)
            {
                return View(lstGioHang);
            }
            else
            {
                // Khách hàng đã đăng nhập đặt hàng
                ACCOUNT account = Session["ACCOUNT"] as ACCOUNT;
                // Lấy thông tin khách hàng từ session
                ViewBag.Account = account;
                return View(lstGioHang);
            }    
        }

        public ActionResult XacNhanDatHang(FormCollection form)
        {
            // Kiểm tra giỏ hàng đã tồn tại chưa
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ACCOUNT account;
            CUSTOMER cus;

            // Xử lý khi mua hàng không đăng nhập
            if (Session["ACCOUNT"] == null)
            {
                // Thêm khách hàng vào bảng khách hàng
                cus = new CUSTOMER();
                cus.CUSTOMER_FIRSTNAME = form["firstName"].ToString();
                cus.CUSTOMER_LASTNAME = form["lastName"].ToString();
                cus.CUSTOMER_ADDRESS = form["address"].ToString();
                cus.PHONE = form["phone"].ToString();
                db.CUSTOMERs.Add(cus);
                db.SaveChanges();

                // Thêm đơn hàng
                ORDER order = new ORDER();
                order.CUSTOMER_ID = cus.CUSTOMER_ID;
                order.SHIP_ADDRESS = form["address"].ToString();
                order.PHONE = form["phone"].ToString();
                order.CREATE_DATE = DateTime.Now;
                // Payment
                //////////////////////////////////                
                // Đang xác nhận đơn hàng
                order.STATUS_ID = 1;
                order.TOTAL = (double)TongTien();
                db.ORDERS.Add(order);
                db.SaveChanges();

                // Thêm chi tiết đơn hàng
                List<ItemGioHang> lstGioHang = LayGioHang();
                foreach (var item in lstGioHang)
                {
                    // Tạo chi tiết đơn đặt hàng
                    ORDER_DETAIL order_detail = new ORDER_DETAIL();
                    // Gán mã đơn hàng vừa tạo cho các chi tiết đơn hàng
                    order_detail.ORDER_ID = order.ORDER_ID;
                    order_detail.PRODUCT_ID = item.MaSP;
                    order_detail.QUANTITY = item.SoLuong;
                    order_detail.PRICE = (double)item.DonGia;
                    db.ORDER_DETAIL.Add(order_detail);
                }   
                db.SaveChanges();
            } 
            // Xửa lý khi mua hàng đã đăng nhập
            else
            {
                // Lấy thông tin khách hàng từ session đã đăng nhập
                account = Session["ACCOUNT"] as ACCOUNT;

                // Thêm khách hàng vào bảng khách hàng
                cus = new CUSTOMER();
                cus.CUSTOMER_FIRSTNAME = form["firstName"].ToString();
                cus.CUSTOMER_LASTNAME = form["lastName"].ToString();
                cus.CUSTOMER_ADDRESS = form["address"].ToString();
                cus.PHONE = form["phone"].ToString();
                cus.ACCOUNT_ID = account.ACCOUNT_ID;
                db.CUSTOMERs.Add(cus);
                db.SaveChanges();

                // Thêm đơn hàng
                ORDER order = new ORDER();
                order.CUSTOMER_ID = cus.CUSTOMER_ID;
                order.SHIP_ADDRESS = form["address"].ToString();
                order.PHONE = form["phone"].ToString();
                order.CREATE_DATE = DateTime.Now;
                // Payment
                //////////////////////////////////                
                // Đang xác nhận đơn hàng
                order.STATUS_ID = 1;
                order.TOTAL = (double)TongTien();
                db.ORDERS.Add(order);
                db.SaveChanges();

                // Thêm chi tiết đơn hàng
                List<ItemGioHang> lstGioHang = LayGioHang();
                foreach (var item in lstGioHang)
                {
                    // Tạo chi tiết đơn đặt hàng
                    ORDER_DETAIL order_detail = new ORDER_DETAIL();
                    // Gán mã đơn hàng vừa tạo cho các chi tiết đơn hàng
                    order_detail.ORDER_ID = order.ORDER_ID;
                    order_detail.PRODUCT_ID = item.MaSP;
                    order_detail.QUANTITY = item.SoLuong;
                    order_detail.PRICE = (double)item.DonGia;
                    db.ORDER_DETAIL.Add(order_detail);
                }
                db.SaveChanges();
            }
            
            Session["Cart"] = null;
            return RedirectToAction("Index", "Cart");
        }
    }
}