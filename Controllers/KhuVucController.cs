using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Controllers
{
    public class KhuVucController : Controller
    {
        DoAnChuyenNganhContext dbContext = new DoAnChuyenNganhContext();
        [ChildActionOnly]
        public ActionResult KhuVucTuLaiPartial()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult KhuVucXeCoTaiXePartial()
        {
            return PartialView();
        }
    }
}