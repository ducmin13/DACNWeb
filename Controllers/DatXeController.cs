using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnChuyenNganh.Models;

namespace DoAnChuyenNganh.Controllers
{
    
    public class DatXeController : Controller
    {
        // GET: DatXe
        DoAnChuyenNganhContext db = new DoAnChuyenNganhContext();
        public ActionResult DatXe()
        {
            List<GioHang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Site");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<GioHang>();
                Session["GioHang"] = lstGiohang;
            }
            return lstGiohang;
        }

        public ActionResult ThemGioHang(int iMaXe, string strURL)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang xe = lstGiohang.Find(n => n.iMaXe == iMaXe);
            if (xe == null)       
                xe = new GioHang(iMaXe);
                lstGiohang.Add(xe);
                return Redirect(strURL);
            //}
            //else
            //{
            //    xe.iSL++;
            //    return Redirect(strURL);
            //}
        }

        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSL);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongTien = (lstGiohang.Sum(n => n.iTHANHTIEN)) ;
            }
            return iTongTien;
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TAIKHOAN"] == null || Session["TAIKHOAN"].ToString() == "")
            {
                return RedirectToAction("DatXe", "DatXe");
            }

            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();

            return View(lstGiohang);


        }
        public ActionResult DatHang(FormCollection collection)
        {
            DatXe dx = new DatXe();
           
            ThanhVien tv = (ThanhVien)Session["TAIKHOAN"];
            List<GioHang> laygh = Laygiohang();
            GioHang gh = new GioHang(1);
            dx.MaThanhVien = tv.MaThanhVien;
            dx.HoTen = tv.HoTen;
            dx.NgayDat = DateTime.Now;
            var ngayketthuc = String.Format("{0:MM/dd/yyyy}", collection["NGAYGIAO"]);
            dx.NgayDat = DateTime.Parse(ngayketthuc);
            dx.ViTriBatDau = gh.iViTriKetThuc;
            db.DatXes.Add(dx);
            db.SaveChanges();



            foreach (var item in laygh)
            {
                ChiTietDatXe ctdx = new ChiTietDatXe();
                ctdx.MaDatXe = dx.MaDatXe;
                ctdx.MaXe = item.iMaXe;
                ctdx.Status = 0;
                ctdx.TongThanhToan = (int)item.iGia;
                db.ChiTietDatXes.Add(ctdx);
            }
            db.SaveChanges();

            return RedirectToAction("Xacnhandonhang", "Giohang");

        }
        }
    }
