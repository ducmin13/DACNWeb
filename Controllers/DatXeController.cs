using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;//dung thu vien nay
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
                return RedirectToAction("DatXe", "DatXe");
          
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
            
            ViewBag.Tongtien = TongTien();

            return View(lstGiohang);


        }
        public ActionResult DatHang(FormCollection collection)
        {
            DatXe dx = new DatXe();
            ChiTietDatXe ctdx = new ChiTietDatXe();
            ThanhVien tv = (ThanhVien)Session["TAIKHOAN"];
            List<GioHang> laygh = Laygiohang();


            var ngaybatdau = String.Format("{0:MM/dd/yyyy}", collection["Ngaybatdau"]);
            dx.NgayDat = DateTime.Parse(ngaybatdau);
            var ngayketthuc = String.Format("{0:MM/dd/yyyy}", collection["Ngayketthuc"]);
            dx.NgayKetThuc = DateTime.Parse(ngayketthuc);
            dx.MaThanhVien = tv.MaThanhVien;
            dx.HoTen = tv.HoTen;
            db.DatXes.Add(dx);
            db.SaveChanges();

            foreach (var item in laygh)
            {

                ctdx.MaThanhVien = dx.MaThanhVien;
                ctdx.MaDatXe = dx.MaDatXe;
                ctdx.MaXe = item.iMaXe;
                ctdx.SoNgay = item.iSL;
                ctdx.DonGia = int.Parse(item.iGia.ToString());
                ctdx.Status = 0;
                ctdx.TongThanhToan = int.Parse(item.iTHANHTIEN.ToString());
                db.ChiTietDatXes.Add(ctdx);
            }

            db.SaveChanges();
            SendVerificationLinkEmail(tv.Email, ctdx.TongThanhToan.ToString());
            return RedirectToAction("Xacnhandonhang", "DatXe");

        }
        public ActionResult CapnhatGiohang(int iMaXe, FormCollection f)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sp = lstGiohang.SingleOrDefault(n => n.iMaXe == iMaXe);

            if (sp != null)
            {
                sp.iSL = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("DatXe");
        }
        public ActionResult Xacnhandonhang(FormCollection f)
        {
            if (Session["TAIKHOAN"] == null || Session["TAIKHOAN"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Home");
            }
            List<GioHang> lstGiohang = Laygiohang();
            //ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View();

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Payment()
        {
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOUMSK20220614";
            string accessKey = "FviPM3XuoPjqtHHb";
            string serectkey = "7LDKFATkz2otkjuMlDh4NYAXhrdxdKeT";
            string orderInfo = "Carbook";
            string returnUrl = "https://localhost:44340/Home/Index";
            string notifyurl = "https://localhost:44340/Home/Index";
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
        [NonAction]
        public void SendVerificationLinkEmail(string emailID,string tongtien)
        {
         

            var fromEmail = new MailAddress("jacknhoxdx3@gmail.com", "Carbook");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "jtkvzmzwikpchunx";
            string subject = "TMTcar xin cám ơn quý khách hàng!<br/>";

            string body = "Chúng tôi xin chân thành cám ơn quý khách hàng<br/>" +
                " Chúc quý khách có một chuyến đi thật ý nghĩa" +
                " Xác nhận thanh toán thành công qua phương thức thanh toán online(MoMo)<br/>" +
                " Tổng số tiền đã thanh toán:"+ tongtien;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        public ActionResult ConfirmPaymentClient()
        {
            return View();
        }
        
    }
    }
