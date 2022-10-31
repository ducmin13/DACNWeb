﻿using BotDetect.Web.Mvc;
using DoAnChuyenNganh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DoAnChuyenNganh.Controllers
{
    public class HomeController : Controller
    {
        DoAnChuyenNganhContext dbContext = new DoAnChuyenNganhContext();

        public ActionResult Index()
        {
            //Lần lượt tạo các viewbag để lấy list từ csdl
            //List địa điểm
            var lstDiaDiem = dbContext.DiaDiems.ToList();
            //Gán vào viewbag
            ViewBag.ListDiaDiem = lstDiaDiem;

            //List bài viết
            var lstBaiViet = dbContext.BaiViets.ToList();
            ViewBag.ListBaiViet = lstBaiViet;

            //List xe
            var lstXe = dbContext.Xes.ToList();
            ViewBag.ListXe = lstXe;

            return View();
        }

        public ActionResult HeaderPartial()
        {
            return PartialView();
        }
        public ActionResult FooterPartial()
        {
            return PartialView();
        }


        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CaptchaValidationActionFilter("CaptchaCode", "ExampleCaptcha", "Không hợp lệ!")]
        public ActionResult DangKy(ThanhVien tv)    //dùng post để truyền data lên csdl, dùng biến tv trong model thay formcollection
        {

            if (ModelState.IsValid)
            {
                var check = dbContext.ThanhViens.FirstOrDefault(s => s.Email == tv.Email);
                if (check != null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else
                {               
                    tv.MaLoaiThanhVien = 2;
                    dbContext.ThanhViens.Add(tv);
                    dbContext.SaveChanges();
                    ViewBag.ThongBaoThanhCong = "Đăng ký thành công";
                    // Reset the captcha if your app's workflow continues with the same view
                    MvcCaptcha.ResetCaptcha("ExampleCaptcha");
                    return RedirectToAction("Index","Home");
                }
            }
            else
            {
                ViewBag.ThongBaoThatBai = "Không hợp lệ!";
                return View();

            }
            return View();
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }



        public ActionResult HuongDanDangKyChuXe()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "ExampleCaptcha", "Không hợp lệ!")]
        public ActionResult DangKyChuXe(ThanhVien tv)    //dùng post để truyền data lên csdl, dùng biến tv trong model thay formcollection
        {

            if (ModelState.IsValid)
            {
                    tv.MaLoaiThanhVien = 3;
                    ViewBag.ThongBaoThanhCong = "Đăng ký thành công! Chúng tôi sẽ liên hệ với bạn trong thời gian tới.";
                    dbContext.ThanhViens.Add(tv);
                    dbContext.SaveChanges();
                    // Reset the captcha if your app's workflow continues with the same view
                    MvcCaptcha.ResetCaptcha("ExampleCaptcha");
                
            }
            else
            {
                ViewBag.ThongBaoThatBai = "Không hợp lệ!";
            }
            return View();
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        //đăng nhập
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            //kiểm tra tên đăng nhập và mật khẩu
            string email = f["Email"].ToString();   //lấy chuỗi trong txtTenDangNhap
            string matKhau = f["MatKhau"].ToString();    //lấy chuỗi trong txtMatKhau
            ThanhVien admin = dbContext.ThanhViens.SingleOrDefault(n => n.Email == email && n.MatKhau == matKhau && n.MaLoaiThanhVien == 1);
            if (admin != null)
            {
                return RedirectToAction("Admin", "Admin");
            }
            ThanhVien tv = dbContext.ThanhViens.SingleOrDefault(n => n.Email == email && n.MatKhau == matKhau);      //so sánh với tk và mk trong csdl
            if (tv != null)
            {
                var lstQuyen = dbContext.PhanQuyens.Where(n => n.MaLoaiThanhVien == tv.MaLoaiThanhVien);   //lấy ra list quyền tương ứng loaitv
                string quyen = "";
                if (lstQuyen.Count() != 0)
                {
                    foreach (var item in lstQuyen)   //duyệt list quyền
                    {
                        quyen += item.Quyen.MaQuyen + ",";
                    }
                    quyen = quyen.Substring(0, quyen.Length - 1); //Cắt dấu ,
                    PhanQuyenUser(tv.Email.ToString(), quyen);

                    Session["TaiKhoan"] = tv;
                    return RedirectToAction("Index", "Home");
                }

            }
            return Content("Tài khoản hoặc mật khẩu không chính xác.");
        }

        //phân quyền
        public void PhanQuyenUser(string Taikhoan, string Quyen)
        {
            FormsAuthentication.Initialize();

            var ticket = new FormsAuthenticationTicket(1,
                                            Taikhoan,   //đặt tên ticket theo tên tk 
                                            DateTime.Now,   //lấy tgian bắt đầu
                                            DateTime.Now.AddHours(3),   //thời gian 3 tiếng out ra
                                            false,  //ko lưu
                                            Quyen,  //Lấy chuỗi phân quyền
                                            FormsAuthentication.FormsCookiePath);   //Lấy đg dẫn cookie thay vì name
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));  //tạo cookie(tự tạo name, mã hóa thông tin ticket add vào cookie)
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;    //ktra cookie có chưa
            Response.Cookies.Add(cookie);     //
        }

        //đăng xuất
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null; //thiết lập session là null

            FormsAuthentication.SignOut();  //xóa bộ nhớ cookie

            return RedirectToAction("Index");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult LienHe()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //Giải phóng dung lượng biến dbContext, để ở cuối controller
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dbContext != null)
                    dbContext.Dispose();
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}