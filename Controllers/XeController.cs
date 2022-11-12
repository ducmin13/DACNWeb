using DoAnChuyenNganh.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Controllers
{
    public class XeController : Controller
    {
        DoAnChuyenNganhContext dbContext = new DoAnChuyenNganhContext();
        [ChildActionOnly]
        public ActionResult XePartial()
        {
            return PartialView();
        }

        public ActionResult HienThiToanBoXe(int page = 1, int pageSize = 8)
        {
            //List hãng xe
            var lstNhaSanXuat = dbContext.NhaSanXuats.ToList();
            ViewBag.ListNhaSanXuat = lstNhaSanXuat;
            //List loại xe
            var lstLoaiXe = dbContext.LoaiXes.ToList();
            ViewBag.ListLoaiXe = lstLoaiXe;

            return View(dbContext.Xes.OrderBy(x => x.MaXe).ToPagedList(page, pageSize));
        }

        public ActionResult HienThiXeTheoNhaSanXuat(int? id, int page = 1, int pageSize = 8)
        {
            //List hãng xe
            var lstNhaSanXuat = dbContext.NhaSanXuats.ToList();
            ViewBag.ListNhaSanXuat = lstNhaSanXuat;
            //List loại xe
            var lstLoaiXe = dbContext.LoaiXes.ToList();
            ViewBag.ListLoaiXe = lstLoaiXe;

            var xes = (from s in dbContext.Xes where s.MaNhaSanXuat == id select s).ToList();
            return View(xes.OrderBy(x=>x.MaXe).ToPagedList(page,pageSize));
        }

        public ActionResult HienThiXeTheoLoaiXe(int? id, int page = 1, int pageSize = 8)
        {
            //list hãng xe
            var lstNhaSanXuat = dbContext.NhaSanXuats.ToList();
            ViewBag.ListNhaSanXuat = lstNhaSanXuat;
            //List loại xe
            var lstLoaiXe = dbContext.LoaiXes.ToList();
            ViewBag.ListLoaiXe = lstLoaiXe;

            var xes = (from s in dbContext.Xes where s.MaLoaiXe == id select s).ToList();
            return View(xes.OrderBy(x => x.MaXe).ToPagedList(page, pageSize));
        }
    }
}