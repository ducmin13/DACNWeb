using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnChuyenNganh.Models;
using PagedList;

namespace DoAnChuyenNganh.Controllers
{
    public class TimKiemController : Controller
    {
        DoAnChuyenNganhContext db = new DoAnChuyenNganhContext();
        
       
        
       
        public ActionResult TimXeCoTaiXe(string diachi/*, int page = 1, int pageSize = 8*/)
        {
            var lstsp = db.Xes.Where(n => n.KhuVuc.Contains(diachi) && n.MaHinhThucDatXe == 1);
            return View(lstsp.OrderBy(n => n.KhuVuc)/*.ToPagedList(page, pageSize)*/);
        }
        public ActionResult TimXeTuLai(string diachi/*, int page = 1, int pageSize = 8*/)
        {
            var lstsp = db.Xes.Where(n => n.KhuVuc.Contains(diachi) && n.MaHinhThucDatXe == 2);
            return View(lstsp.OrderBy(n => n.KhuVuc));
        }
    }
}