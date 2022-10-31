using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnChuyenNganh.Models;
namespace DoAnChuyenNganh.Areas.Admin.Controllers
{
    public class QuanLyDiaDiemController : Controller
    {
        // GET: Admin/QuanLyDiaDiem
        DoAnChuyenNganhContext db = new DoAnChuyenNganhContext();
        // GET: QuanLySanPham
        public ActionResult Index(string searchString)
        {
            ViewBag.Keyword = searchString;
            var all_diadiem = db.DiaDiems.Where(n => n.TenDiaDiem != null).OrderBy(n => n.MaDiaDiem);
            if (!string.IsNullOrEmpty(searchString)) all_diadiem =
            (IOrderedQueryable<DiaDiem>)all_diadiem.Where(a => a.TenDiaDiem.Contains(searchString));
            return View(all_diadiem);
        }

        [HttpGet]
        public ActionResult TaoMoi()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(DiaDiem diaDiem, HttpPostedFileBase HinhAnh)
        {
            //ktra hình ảnh tồn tại trong csdl
            if (HinhAnh != null)
            {
                if (HinhAnh.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(HinhAnh.FileName);  //Lấy tên hình
                    var path = Path.Combine(Server.MapPath("~/Assets/images/dia_diem"), fileName);   //lấy hình đưa vào folder HinhAnhSP

                    //lấy hình đưa vào folder
                    HinhAnh.SaveAs(path);
                    diaDiem.HinhAnh = fileName;  //lưu vào sp
                }
            }

            db.DiaDiems.Add(diaDiem);
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
            DiaDiem diaDiem = db.DiaDiems.SingleOrDefault(n => n.MaDiaDiem == id);
            if (diaDiem == null)
            {
                return HttpNotFound();
            }

            return View(diaDiem);
        }

        [ValidateInput(false)]
        [HttpPost, ActionName("ChinhSua")]
        public ActionResult CapNhat(int id)
        {

            DiaDiem diaDiem = db.DiaDiems.SingleOrDefault(n => n.MaDiaDiem == id);
            UpdateModel(diaDiem);
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
            DiaDiem diaDiem = db.DiaDiems.Find(id);
            if (diaDiem == null)
            {
                return HttpNotFound();
            }

            return View(diaDiem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadHinh(int id, HttpPostedFileBase HinhAnh)
        {
            DiaDiem diaDiem = db.DiaDiems.Find(id);

            if (HinhAnh != null)
            {
                string path = Server.MapPath("~/Assets/images/dia_diem/");
                string Ten = null;
                HinhAnh.SaveAs(path + HinhAnh.FileName);
                Ten = HinhAnh.FileName;

                if (!string.IsNullOrEmpty(diaDiem.HinhAnh))
                {
                    string pathAndFname = Server.MapPath($"~/Assets/images/dia_diem/{diaDiem.HinhAnh}");
                    if (System.IO.File.Exists(pathAndFname))
                        System.IO.File.Delete(pathAndFname);
                }
                diaDiem.HinhAnh = Ten;
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
            DiaDiem diaDiem = db.DiaDiems.SingleOrDefault(n => n.MaDiaDiem == id);
            if (diaDiem == null)
            {
                return HttpNotFound();
            }

            return View(diaDiem);
        }

        [HttpPost]
        public ActionResult Xoa(int id)
        {
            ////lấy sp cần chỉnh sửa
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            //}
            DiaDiem diaDiem = db.DiaDiems.SingleOrDefault(n => n.MaDiaDiem == id);
            if (diaDiem == null)
            {
                return HttpNotFound();
            }
            db.DiaDiems.Remove(diaDiem);
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
