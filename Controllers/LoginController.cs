using log4net;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Trippy_Land.Models;
using CaptchaMvc.HtmlHelpers;

namespace Trippy_Land.Controllers
{
    public class LoginController : Controller
    {
        private static readonly ILog logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        // GET: Login
        public ActionResult Login()
        {           
            return View();          
        }
        /// <summary>
        /// Đăng nhập với mật khẩu đã được băm qua SHA 256
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]     
        public ActionResult Login(User objUser)
        {          
            Session.Clear(); //remove session 
            if (this.IsCaptchaValid(""))
            {
                ViewBag.ErrMessage = "Mã Captcha sai";
            }           
            try
            {
                string HashPassword = GetSHA256(objUser.MatKhau);                             
                    var obj = DataProvider.Entities.Users
                        .Where(u => u.TenDangNhap.Equals(objUser.TenDangNhap) 
                        && u.MatKhau.Equals(HashPassword)).FirstOrDefault();
                   
                    if (obj != null)
                    {
                        if (obj.EmailConfirm == false)
                            ViewBag.EmailNotConfirm = "Please! Confirm your Email";
                        logger.Info("Have a  user login! Username: " + obj.TenDangNhap);                    
                        Session["UserOnline"] = obj;
                        Session["SessionTenUser"] = obj.TenDangNhap;
                        if (obj.UserRole.Id == 1 || obj.UserRole.Id == 3 || obj.UserRole.Id == 4)
                            return RedirectToAction("DanhSachTinh", "Tinh", new { area = "Admin" });
                        Session.Timeout = 5;
                        return RedirectToAction("Index", "Home", new { area = "" });                       
                    }
                    else
                    {
                        ViewBag.Error = "Vui lòng kiểm tra lại tài khoản hoặc mật khẩu";
                        logger.Info("Have a error when user sign in!" + "Wrong password or username");
                       }      
                return View();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("Return", "ErrorPage");
            }
            
        }
        public ActionResult Logout()
        {
            Session.Clear(); //remove session 
            logger.Info("User Logout!");
            return RedirectToAction("Login", "Login");
        }

       
        /// <summary>
        /// Chuyển đổi một chuỗi về SHA256
        /// </summary>
        /// <param name="clearText">Bản rõ cần băm</param>
        /// <returns></returns>
        public static string GetSHA256(string clearText)
        {
            SHA256CryptoServiceProvider sHA256 = new SHA256CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(clearText);
            byte[] targetData = sHA256.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

    }
}