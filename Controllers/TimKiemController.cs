using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnChuyenNganh.Models;
namespace DoAnChuyenNganh.Controllers
{
    public class TimKiemController : Controller
    {
        DoAnChuyenNganhContext db = new DoAnChuyenNganhContext();
        // GET: TimKirm
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CoTaiXe()
        {
            return  View();
        }
        public ActionResult XacNhanXe(int id)
        {
            var xecotaixe = db.LoaiXes.Where(n => n.TenLoaiXe != null).OrderBy(n => n.MaLoaiXe);
            if (!string.IsNullOrEmpty(searchString)) all_loaixe =
            (IOrderedQueryable<LoaiXe>)all_loaixe.Where(a => a.TenLoaiXe.Contains(searchString));

            return View(all_loaixe);
        }
    }
}