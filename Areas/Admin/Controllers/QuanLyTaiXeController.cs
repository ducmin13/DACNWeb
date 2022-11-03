using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Areas.Admin.Controllers
{
    public class QuanLyTaiXeController : Controller
    {
        DoAnChuyenNganhContext db = new DoAnChuyenNganhContext();

        public ActionResult Index(string searchString)
        {
            ViewBag.Keyword = searchString;
            var all_taixe = db.TaiXes.Where(n => n.Status == 1).OrderBy(n => n.MaTaiXe);
            if (!string.IsNullOrEmpty(searchString)) all_taixe =
            (IOrderedQueryable<TaiXe>)all_taixe.Where(a => a.TenTaiXe.Contains(searchString));

            return View(all_taixe);
        }
        [HttpGet]
        public ActionResult TaoMoi()
        {
            //ViewBag.MaLoaiThanhVien = new SelectList(db.LoaiThanhViens.OrderBy(n => n.MaLoaiThanhVien), "MaLoaiThanhVien", "TenLoaiThanhVien");

            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(TaiXe taixe)
        {
            //ViewBag.MaLoaiThanhVien = new SelectList(db.LoaiThanhViens.OrderBy(n => n.MaLoaiThanhVien), "MaLoaiThanhVien", "TenLoaiThanhVien");
            db.TaiXes.Add(taixe);
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
            TaiXe taiXe = db.TaiXes.SingleOrDefault(n => n.MaTaiXe == id);
            if (taiXe == null)
            {
                return HttpNotFound();
            }

            return View(taiXe);
        }

        [ValidateInput(false)]
        [HttpPost, ActionName("ChinhSua")]
        public ActionResult CapNhat(int id)
        {
            TaiXe taixe = db.TaiXes.SingleOrDefault(n => n.MaTaiXe == id);
            UpdateModel(taixe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UploadHinh(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            TaiXe taiXe = db.TaiXes.Find(id);
            if (taiXe == null)
            {
                return HttpNotFound();
            }

            return View(taiXe);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadHinh(int id, HttpPostedFileBase HinhAnh)
        {
            BaiViet baiViet = db.BaiViets.Find(id);

            if (HinhAnh != null)
            {
                string path = Server.MapPath("~/Assets/images/bai_viet/");
                string Ten = null;
                HinhAnh.SaveAs(path + HinhAnh.FileName);
                Ten = HinhAnh.FileName;

                if (!string.IsNullOrEmpty(baiViet.HinhAnh))
                {
                    string pathAndFname = Server.MapPath($"~/Assets/images/bai_viet/{baiViet.HinhAnh}");
                    if (System.IO.File.Exists(pathAndFname))
                        System.IO.File.Delete(pathAndFname);
                }
                baiViet.HinhAnh = Ten;
            }
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
            TaiXe taiXe = db.TaiXes.SingleOrDefault(n => n.MaTaiXe == id);
            if (taiXe == null)
            {
                return HttpNotFound();
            }

            return View(taiXe);
        }

        [HttpPost]
        public ActionResult Xoa(int id)
        {
            TaiXe taiXe = db.TaiXes.SingleOrDefault(n => n.MaTaiXe == id);
            if (taiXe == null)
            {
                return HttpNotFound();
            }
            db.TaiXes.Remove(taiXe);
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