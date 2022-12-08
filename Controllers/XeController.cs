using DoAnChuyenNganh.Models;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace DoAnChuyenNganh.Controllers
{
    public class XeController : Controller
    {
        DoAnChuyenNganhContext dbContext = new DoAnChuyenNganhContext();
        [ChildActionOnly]
        public ActionResult XePartial()
        {
            return PartialView();
        }

        public ActionResult HienThiToanBoXe(int page = 1, int pageSize = 8)
        {
            //List hãng xe
            var lstNhaSanXuat = dbContext.NhaSanXuats.ToList();
            ViewBag.ListNhaSanXuat = lstNhaSanXuat;
            //List loại xe
            var lstLoaiXe = dbContext.LoaiXes.ToList();
            ViewBag.ListLoaiXe = lstLoaiXe;

            return View(dbContext.Xes.OrderBy(x => x.MaXe).ToPagedList(page, pageSize));
        }

        public ActionResult HienThiXeTheoNhaSanXuat(int? id, int page = 1, int pageSize = 8)
        {
            //List hãng xe
            var lstNhaSanXuat = dbContext.NhaSanXuats.ToList();
            ViewBag.ListNhaSanXuat = lstNhaSanXuat;
            //List loại xe
            var lstLoaiXe = dbContext.LoaiXes.ToList();
            ViewBag.ListLoaiXe = lstLoaiXe;

            var xes = (from s in dbContext.Xes where s.MaNhaSanXuat == id select s).ToList();
            return View(xes.OrderBy(x=>x.MaXe).ToPagedList(page,pageSize));
        }

        public ActionResult HienThiXeTheoLoaiXe(int? id, int page = 1, int pageSize = 8)
        {
            //list hãng xe
            var lstNhaSanXuat = dbContext.NhaSanXuats.ToList();
            ViewBag.ListNhaSanXuat = lstNhaSanXuat;
            //List loại xe
            var lstLoaiXe = dbContext.LoaiXes.ToList();
            ViewBag.ListLoaiXe = lstLoaiXe;

            var xes = (from s in dbContext.Xes where s.MaLoaiXe == id select s).ToList();
            return View(xes.OrderBy(x => x.MaXe).ToPagedList(page, pageSize));
        }
        //public ActionResult HienThiTheoKhuVucChoXeTuLai(int? id, int page = 1, int pageSize = 8)
        //{
        //    //List khu vuc 
        //    var lstKhuVuc = dbContext.QuanHuyens.ToList();
        //    ViewBag.ListNhaSanXuat = lstKhuVuc;


        //    var xes = (from s in dbContext.Xes where s.MaQuanHuyen == id && s.MaHinhThucDatXe == 2 select s).ToList();
        //    return View(xes.OrderBy(x => x.MaXe).ToPagedList(page, pageSize));
        //}
        //public ActionResult HienThiTheoKhuVucChoXeCoTaiXe(int? id, int page = 1, int pageSize = 8)
        //{
        //    //List khu vuc 
        //    var lstKhuVuc = dbContext.QuanHuyens.ToList();
        //    ViewBag.ListNhaSanXuat = lstKhuVuc;


        //    var xes = (from s in dbContext.Xes where s.MaQuanHuyen == id && s.MaHinhThucDatXe == 1 select s).ToList();
        //    return View(xes.OrderBy(x => x.MaXe).ToPagedList(page, pageSize));
        //}
        public ActionResult ChiTietXe(int id)
        {
            var xe = from s in dbContext.Xes
                     where s.MaXe == id
                     select s;
            return View(xe.Single());
        }
        public ActionResult XeCoTaiXe(int page = 1, int pageSize = 8)
        {
          
            var xes = (from s in dbContext.Xes where s.MaHinhThucDatXe == 1 select s).ToList();
            return View(xes.OrderBy(x => x.MaXe).ToPagedList(page, pageSize));
        }
        public ActionResult XeTuLai(int page = 1, int pageSize = 8)
        {

            var xes = (from s in dbContext.Xes where s.MaHinhThucDatXe == 2 select s).ToList();
            return View(xes.OrderBy(x => x.MaXe).ToPagedList(page, pageSize));
        }
        public ActionResult TimXeCoTaiXe(string diachi)
        {
            var lstsp = dbContext.Xes.Where(n => n.DiaChi.Contains(diachi) && n.MaHinhThucDatXe == 1);
            return View(lstsp.OrderBy(n => n.DiaChi));
        }
        public ActionResult TimXeTuLai(string diachi)
        {
            var lstsp = dbContext.Xes.Where(n => n.DiaChi.Contains(diachi) && n.MaHinhThucDatXe == 2);
            return View(lstsp.OrderBy(n => n.DiaChi));
        }

    }
}