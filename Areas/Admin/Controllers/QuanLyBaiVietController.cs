using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Areas.Admin.Controllers
{
    public class QuanLyBaiVietController : Controller
    {

        DoAnChuyenNganhContext db = new DoAnChuyenNganhContext();

        public ActionResult Index(string searchString)
        {
            ViewBag.Keyword = searchString;
            var all_baiviet = db.BaiViets.Where(n => n.TieuDeChinh != null).OrderBy(n => n.MaBaiViet);
            if (!string.IsNullOrEmpty(searchString)) all_baiviet =
            (IOrderedQueryable<BaiViet>)all_baiviet.Where(a => a.TieuDeChinh.Contains(searchString));

            return View(all_baiviet);
        }

        [HttpGet]
        public ActionResult TaoMoi()
        {
            ViewBag.MaChuDe = new SelectList(db.ChuDes.OrderBy(n => n.MaChuDe), "MaChuDe", "TenChuDe");

            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(BaiViet baiViet, HttpPostedFileBase HinhAnh)
        {
            ViewBag.MaChuDe = new SelectList(db.ChuDes.OrderBy(n => n.MaChuDe), "MaChuDe", "TenChuDe");

            //ktra hình ảnh tồn tại trong csdl

            if (HinhAnh != null)
            {
                if (HinhAnh.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(HinhAnh.FileName);  //Lấy tên hình
                    var path = Path.Combine(Server.MapPath("~/Assets/images/bai_viet"), fileName);   //lấy hình đưa vào folder HinhAnhSP

                    //lấy hình đưa vào folder
                    HinhAnh.SaveAs(path);
                    baiViet.HinhAnh = fileName;  //lưu vào sp
                }
            }

            db.BaiViets.Add(baiViet);
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
            BaiViet baiViet = db.BaiViets.SingleOrDefault(n => n.MaBaiViet == id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }

            return View(baiViet);
        }

        [ValidateInput(false)]
        [HttpPost, ActionName("ChinhSua")]
        public ActionResult CapNhat(int id)
        {
            BaiViet baiViet = db.BaiViets.SingleOrDefault(n => n.MaBaiViet == id);
            UpdateModel(baiViet);
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
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }

            return View(baiViet);
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
            BaiViet baiViet = db.BaiViets.SingleOrDefault(n => n.MaBaiViet == id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }

            return View(baiViet);
        }

        [HttpPost]
        public ActionResult Xoa(int id)
        {
            BaiViet baiViet = db.BaiViets.SingleOrDefault(n => n.MaBaiViet == id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            db.BaiViets.Remove(baiViet);
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