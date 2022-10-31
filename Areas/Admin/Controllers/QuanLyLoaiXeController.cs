using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Areas.Admin.Controllers
{
    public class QuanLyLoaiXeController : Controller
    {
        // GET: Admin/QuanLyDiaDiem
        DoAnChuyenNganhContext db = new DoAnChuyenNganhContext();
        // GET: QuanLySanPham
        public ActionResult Index(string searchString)
        {
            ViewBag.Keyword = searchString;
            var all_loaixe = db.LoaiXes.Where(n => n.TenLoaiXe != null).OrderBy(n => n.MaLoaiXe);
            if (!string.IsNullOrEmpty(searchString)) all_loaixe =
            (IOrderedQueryable<LoaiXe>)all_loaixe.Where(a => a.TenLoaiXe.Contains(searchString));

            return View(all_loaixe);
        }

        [HttpGet]
        public ActionResult TaoMoi()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(LoaiXe loaiXe)
        {

            db.LoaiXes.Add(loaiXe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChinhSua(int? id)
        {
            //lấy sp cần chỉnh sửa
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            LoaiXe loaiXe = db.LoaiXes.SingleOrDefault(n => n.MaLoaiXe == id);
            if (loaiXe == null)
            {
                return HttpNotFound();
            }

            return View(loaiXe);
        }

        [ValidateInput(false)]
        [HttpPost, ActionName("ChinhSua")]
        public ActionResult CapNhat(int id)
        {

            LoaiXe loaiXe = db.LoaiXes.SingleOrDefault(n => n.MaLoaiXe == id);
            UpdateModel(loaiXe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Xoa(int? id)
        {
            //lấy sp cần chỉnh sửa
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            LoaiXe loaiXe = db.LoaiXes.SingleOrDefault(n => n.MaLoaiXe == id);
            if (loaiXe == null)
            {
                return HttpNotFound();
            }

            return View(loaiXe);
        }

        [HttpPost]
        public ActionResult Xoa(int id)
        {
            LoaiXe loaiXe = db.LoaiXes.SingleOrDefault(n => n.MaLoaiXe == id);
            if (loaiXe == null)
            {
                return HttpNotFound();
            }
            db.LoaiXes.Remove(loaiXe);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        //Giải phóng dung lượng biến db, để ở cuối controller
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}