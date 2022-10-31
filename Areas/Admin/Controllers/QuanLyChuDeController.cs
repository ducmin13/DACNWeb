using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Areas.Admin.Controllers
{
    public class QuanLyChuDeController : Controller
    {
        // GET: Admin/QuanLyDiaDiem
        DoAnChuyenNganhContext db = new DoAnChuyenNganhContext();
        // GET: QuanLySanPham
        public ActionResult Index(string searchString)
        {
            ViewBag.Keyword = searchString;
            var all_chude = db.ChuDes.Where(n => n.TenChuDe != null).OrderBy(n => n.MaChuDe);
            if (!string.IsNullOrEmpty(searchString)) all_chude =
            (IOrderedQueryable<ChuDe>)all_chude.Where(a => a.TenChuDe.Contains(searchString));

            return View(all_chude);
        }

        [HttpGet]
        public ActionResult TaoMoi()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(ChuDe chuDe)
        {

            db.ChuDes.Add(chuDe);
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
            ChuDe chuDe = db.ChuDes.SingleOrDefault(n => n.MaChuDe == id);
            if (chuDe == null)
            {
                return HttpNotFound();
            }

            return View(chuDe);
        }

        [ValidateInput(false)]
        [HttpPost, ActionName("ChinhSua")]
        public ActionResult CapNhat(int id)
        {

            ChuDe chuDe = db.ChuDes.SingleOrDefault(n => n.MaChuDe == id);
            UpdateModel(chuDe);
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
            ChuDe chuDe = db.ChuDes.SingleOrDefault(n => n.MaChuDe == id);
            if (chuDe == null)
            {
                return HttpNotFound();
            }

            return View(chuDe);
        }

        [HttpPost]
        public ActionResult Xoa(int id)
        {
            ChuDe chuDe = db.ChuDes.SingleOrDefault(n => n.MaChuDe == id);
            if (chuDe == null)
            {
                return HttpNotFound();
            }
            db.ChuDes.Remove(chuDe);
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