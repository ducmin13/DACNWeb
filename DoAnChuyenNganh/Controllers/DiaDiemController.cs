using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Controllers
{
    public class DiaDiemController : Controller
    {
        DoAnChuyenNganhContext dbContext = new DoAnChuyenNganhContext();

        //tao partial view địa điểm để hiển thị list địa điểm trên trang chủ
        [ChildActionOnly]
        public ActionResult DiaDiemPartial()
        {
            return PartialView();
        }
    }
}