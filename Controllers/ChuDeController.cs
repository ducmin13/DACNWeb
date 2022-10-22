using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Controllers
{
    public class ChuDeController : Controller
    {
        DoAnChuyenNganhContext dbContext = new DoAnChuyenNganhContext();
        [ChildActionOnly]
        public ActionResult ChuDePartial()
        {
            return PartialView();
        }
    }
}