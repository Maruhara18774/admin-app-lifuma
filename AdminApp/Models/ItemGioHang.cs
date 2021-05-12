using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminApp.Models
{
    public class ItemGioHang
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string HinhAnh { get; set; }

        public ItemGioHang (int id, int quantity)
        {
            using (LIMUPAStoreEntities db = new LIMUPAStoreEntities())
            {
                this.MaSP = id;
                PRODUCT product = db.PRODUCTs.Single(s => s.PRODUCT_ID == id);
                TenSP = product.PRODUCT_NAME;
                HinhAnh = product.PRODUCT_IMAGE;
                DonGia = (decimal) product.PRICE;
                SoLuong = quantity;
                ThanhTien = DonGia * SoLuong;
            }
        }

        public ItemGioHang(int id)
        {
            using (LIMUPAStoreEntities db = new LIMUPAStoreEntities())
            {
                this.MaSP = id;
                PRODUCT product = db.PRODUCTs.Single(s => s.PRODUCT_ID == id);
                TenSP = product.PRODUCT_NAME;
                HinhAnh = product.PRODUCT_IMAGE;
                DonGia = (decimal) product.PRICE;
                SoLuong = 1;
                ThanhTien = DonGia * SoLuong;
            }
        }

        public ItemGioHang()
        {

        }
    }
}