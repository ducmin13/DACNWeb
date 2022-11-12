using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnChuyenNganh.Models;
using DoAnChuyenNganh.MoMo;
using Newtonsoft.Json.Linq;

namespace DoAnChuyenNganh.Controllers
{
    
    public class DatXeController : Controller
    {
        // GET: DatXe
        DoAnChuyenNganhContext db = new DoAnChuyenNganhContext();
        public ActionResult DatXe()
        {
            List<GioHang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<GioHang>();
                Session["GioHang"] = lstGiohang;
            }
            return lstGiohang;
        }

        public ActionResult ThemGioHang(int iMaXe, string strURL)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang xe = lstGiohang.Find(n => n.iMaXe == iMaXe);
            if (xe == null)       
                xe = new GioHang(iMaXe);
                lstGiohang.Add(xe);
                return Redirect(strURL);
            //}
            //else
            //{
            //    xe.iSL++;
            //    return Redirect(strURL);
            //}
        }

        private int TongSoLuong()
        {

            int iTongSoLuong = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                //var ngaybatdau 
                DateTime iNgayBatDau = new DateTime();

                DateTime iNgayKetThuc = new DateTime();
                DateTime ngaybatdau = Convert.ToDateTime(iNgayBatDau);
                DateTime ngayketthuc = Convert.ToDateTime(iNgayKetThuc);
                TimeSpan Time = ngaybatdau - ngayketthuc;
                int TongSoNgay = Time.Days;
                iTongSoLuong = TongSoNgay;      
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongTien = (lstGiohang.Sum(n => n.iTHANHTIEN)) ;
            }
            return iTongTien;
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TAIKHOAN"] == null || Session["TAIKHOAN"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Home");
            }

            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();

            return View(lstGiohang);


        }
        public ActionResult DatHang(FormCollection collection)
        {
            DatXe dx = new DatXe();
           
            ThanhVien tv = (ThanhVien)Session["TAIKHOAN"];
            List<GioHang> laygh = Laygiohang();        
            dx.MaThanhVien = tv.MaThanhVien;
            dx.HoTen = tv.HoTen;
            DateTime NgayDat = Convert.ToDateTime(DateTime.Now);
            var ngayketthuc = String.Format("{0:dd/mm/yyyy}", collection["NGAYGIAO"]);

            DateTime NgayKetThuc = Convert.ToDateTime(DateTime.Parse(ngayketthuc));
            TimeSpan SoNgay = NgayKetThuc - NgayDat;
            db.DatXes.Add(dx);
            db.SaveChanges();

            foreach (var item in laygh)
            {
                ChiTietDatXe ctdx = new ChiTietDatXe();
                ctdx.MaDatXe = dx.MaDatXe;
                ctdx.MaXe = item.iMaXe;
                ctdx.Status = 0;
                ctdx.TongThanhToan = (int)item.iGia;
                db.ChiTietDatXes.Add(ctdx);
            }
            db.SaveChanges();

            return RedirectToAction("Xacnhandonhang", "Giohang");

        }
        public ActionResult Xacnhandon(FormCollection f)
        {
            if (Session["TAIKHOAN"] == null || Session["TAIKHOAN"].ToString() == "")
            {
                return RedirectToAction("Login", "KhachHang");
            }
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View();

        }

        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Payment()
        {
            string endpoint = "https://payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOQUFO20220604";
            string accessKey = "CBnwSz6apdin7hlg";
            string serectkey = "SH3p93lEUXFTFWigQDaIRGgdlnmvJcRm";
            string orderInfo = "Hiep Thanh";
            string returnUrl = "https://localhost:44394/Home/Index";
            string notifyurl = "http://ba1adf48beba.ngrok.io/Home/Index";
            string amount = TongTien().ToString();
            string orderid = DateTime.Now.Ticks.ToString();
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();

            string signature = crypto.signSHA256(rawHash, serectkey);


            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount},
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }
            };
            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
            JObject jmessage = JObject.Parse(responseFromMomo);
            Session["Giohang"] = null;
            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        public ActionResult ThanhToanThanhCong()
        {
            return View();
        }
    }
}
