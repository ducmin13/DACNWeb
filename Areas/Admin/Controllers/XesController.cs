using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnChuyenNganh.Models;

namespace DoAnChuyenNganh.Areas.Admin.Controllers
{
    public class XesController : Controller
    {
        private DoAnChuyenNganhContext db = new DoAnChuyenNganhContext();

        // GET: Admin/Xes
        public ActionResult Index()
        {
            var xes = db.Xes.Include(x => x.HinhThucDatXe).Include(x => x.LoaiXe).Include(x => x.NhaSanXuat).Include(x => x.ThanhVien);
            return View(xes.ToList());
        }

        // GET: Admin/Xes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xes.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            return View(xe);
        }

        // GET: Admin/Xes/Create
        public ActionResult Create()
        {
            ViewBag.MaHinhThucDatXe = new SelectList(db.HinhThucDatXes, "MaHinhThucDatXe", "TenHinhThucDatXe");
            ViewBag.MaLoaiXe = new SelectList(db.LoaiXes, "MaLoaiXe", "TenLoaiXe");
            ViewBag.MaNhaSanXuat = new SelectList(db.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat");
            ViewBag.MaThanhVien = new SelectList(db.ThanhViens, "MaThanhVien", "MatKhau");
            return View();
        }

        // POST: Admin/Xes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaXe,TenXe,Gia,HinhAnh,MoTa,MaNhaSanXuat,MaLoaiXe,MaThanhVien,MaHinhThucDatXe")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                db.Xes.Add(xe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHinhThucDatXe = new SelectList(db.HinhThucDatXes, "MaHinhThucDatXe", "TenHinhThucDatXe", xe.MaHinhThucDatXe);
            ViewBag.MaLoaiXe = new SelectList(db.LoaiXes, "MaLoaiXe", "TenLoaiXe", xe.MaLoaiXe);
            ViewBag.MaNhaSanXuat = new SelectList(db.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat", xe.MaNhaSanXuat);
            ViewBag.MaThanhVien = new SelectList(db.ThanhViens, "MaThanhVien", "MatKhau", xe.MaThanhVien);
            return View(xe);
        }

        // GET: Admin/Xes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xes.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHinhThucDatXe = new SelectList(db.HinhThucDatXes, "MaHinhThucDatXe", "TenHinhThucDatXe", xe.MaHinhThucDatXe);
            ViewBag.MaLoaiXe = new SelectList(db.LoaiXes, "MaLoaiXe", "TenLoaiXe", xe.MaLoaiXe);
            ViewBag.MaNhaSanXuat = new SelectList(db.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat", xe.MaNhaSanXuat);
            ViewBag.MaThanhVien = new SelectList(db.ThanhViens, "MaThanhVien", "MatKhau", xe.MaThanhVien);
            return View(xe);
        }

        // POST: Admin/Xes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaXe,TenXe,Gia,HinhAnh,MoTa,MaNhaSanXuat,MaLoaiXe,MaThanhVien,MaHinhThucDatXe")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHinhThucDatXe = new SelectList(db.HinhThucDatXes, "MaHinhThucDatXe", "TenHinhThucDatXe", xe.MaHinhThucDatXe);
            ViewBag.MaLoaiXe = new SelectList(db.LoaiXes, "MaLoaiXe", "TenLoaiXe", xe.MaLoaiXe);
            ViewBag.MaNhaSanXuat = new SelectList(db.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat", xe.MaNhaSanXuat);
            ViewBag.MaThanhVien = new SelectList(db.ThanhViens, "MaThanhVien", "MatKhau", xe.MaThanhVien);
            return View(xe);
        }

        // GET: Admin/Xes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xes.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            return View(xe);
        }

        // POST: Admin/Xes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Xe xe = db.Xes.Find(id);
            db.Xes.Remove(xe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
