using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Areas.Admin.Controllers
{
    public class XeController : Controller
    {
        DoAnChuyenNganhContext db = new DoAnChuyenNganhContext();
        // GET: Admin/Xe
        public ActionResult Quanlyxe()
        {

            return View();
        }


    }
}