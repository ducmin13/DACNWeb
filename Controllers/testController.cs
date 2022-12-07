using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Controllers
{
    public class testController : Controller
    {
        // GET: Admin/test
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TinhNgay(FormCollection collection)
        {
            
            return View();
        }
        public ActionResult Tong()
        {
            return View();
        }
    }
}