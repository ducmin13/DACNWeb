using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Controllers
{
    public class NhaSanXuatController : Controller
    {
        DoAnChuyenNganhContext dbContext = new DoAnChuyenNganhContext();
        [ChildActionOnly]
        public ActionResult NhaSanXuatPartial()
        {
            return PartialView();
        }
    }
}