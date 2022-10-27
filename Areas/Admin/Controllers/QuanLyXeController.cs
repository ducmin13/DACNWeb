using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Areas.Admin.Controllers
{
    public class QuanLyXeController : Controller
    {
        DoAnChuyenNganhContext db = new DoAnChuyenNganhContext();
        // GET: Admin/Xe
        public ActionResult Index()
        {        
            return View(db.Xes.ToList());
        }

    }
}