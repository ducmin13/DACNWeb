using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Areas.Admin.Controllers
{
    public class QuanLyChuXeController : Controller
    {
        DoAnChuyenNganhContext db = new DoAnChuyenNganhContext();
        // GET: Admin/QuanLyChuXe
        public ActionResult QuanLyChuXe(string searchString)
        {
            ViewBag.Keyword = searchString;
            var chuxe = db.ChuXes.Where(n => n.Status == 1).OrderBy(n => n.MaChuXe);
            if (!string.IsNullOrEmpty(searchString)) chuxe =
            (IOrderedQueryable<ChuXe>)chuxe.Where(n => n.TenChuXe.Contains(searchString));

            return View(chuxe);
        }

        public ActionResult YeuCauChoThueXe(string searchString)
        {
            ViewBag.Keyword = searchString;
            var chuxe = db.ChuXes.Where(n => n.Status == 0).OrderBy(n => n.MaChuXe);
            if (!string.IsNullOrEmpty(searchString)) chuxe =
            (IOrderedQueryable<ChuXe>)chuxe.Where(n => n.TenChuXe.Contains(searchString));

            return View(chuxe);
        }


    }
}