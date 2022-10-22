using DoAnChuyenNganh.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Controllers
{
    public class BaiVietController : Controller
    {
        DoAnChuyenNganhContext dbContext = new DoAnChuyenNganhContext();
        [ChildActionOnly]
        public ActionResult BaiVietPartial()
        {
            return PartialView();
        }
        public ActionResult HienThiToanBoBaiViet(int page = 1, int pageSize = 4)
        {
            //List chủ đề
            var lstChuDe = dbContext.ChuDes.ToList();
            ViewBag.ListChuDe = lstChuDe;

            return View(dbContext.BaiViets.OrderBy(x => x.MaBaiViet).ToPagedList(page, pageSize));
        }
        public ActionResult ChiTietBaiViet(int? id)
        {
            var thongTinBaiViet = dbContext.BaiViets.Find(id);
            if (thongTinBaiViet == null)
            {
                return HttpNotFound();
            }
            return View(thongTinBaiViet);
        }
    }
}