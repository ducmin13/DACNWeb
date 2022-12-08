using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class AdminController : Controller
    {
        DoAnChuyenNganhContext db = new DoAnChuyenNganhContext();
        public ActionResult Index()
        {
            DateTime hienTai = DateTime.Today;
            ViewBag.TongDoanhThu = ThongKeDoanhThu();
            ViewBag.SoDonHang = ThongKeDonHang();
            ViewBag.SoThanhVien = ThongKeThanhVien();
            ViewBag.DoanhThuThang = ThongKeDoanhThuThang(hienTai.Month, hienTai.Year);

           
            return View();
        }
        public decimal ThongKeDoanhThu()
        {
            decimal TongDoanhThu = db.ChiTietDatXes.Sum(n => n.SoNgay * n.DonGia).Value;
            return TongDoanhThu;
        }
        public decimal ThongKeDoanhThuThang(int Thang, int Nam)
        {
            var lstDDH = db.DatXes.Where(n => n.NgayDat.Value.Month == Thang && n.NgayDat.Value.Year == Nam);  //lấy ds đơn hàng có date tương ứng
            decimal TongDoanhThu = 0;
            foreach (var item in lstDDH) //duyệt chi tiết từng đơn và tính tổng tiền
            {
                TongDoanhThu += decimal.Parse(item.ChiTietDatXes.Sum(n => n.SoNgay * n.DonGia).Value.ToString());
            }
            return TongDoanhThu;
        }

        //Thống kê tổng đơn hàng
        public double ThongKeDonHang()
        {
            double slddh = db.DatXes.Count();    //đếm số đơn hàng
            return slddh;
        }
        public double ThongKeThanhVien()
        {
            double sltv = db.ThanhViens.Count();    //đếm số thành viên
            return sltv;
        }
    }
}