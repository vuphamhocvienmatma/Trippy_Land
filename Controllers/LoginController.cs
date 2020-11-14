using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Trippy_Land.Models;

namespace Trippy_Land.Controllers
{
    public class LoginController : Controller
    {
        private static readonly ILog logger = 
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(User objUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var check = DataProvider.Entities.Users.FirstOrDefault(s => s.TenDangNhap == objUser.TenDangNhap);
                    if (check == null)
                    {
                        objUser.MatKhau = GetSHA256(objUser.MatKhau);
                        DataProvider.Entities.Configuration.ValidateOnSaveEnabled = false;
                        objUser.UserRoleId = 2;
                        DataProvider.Entities.Users.Add(objUser);
                        DataProvider.Entities.SaveChanges();
                        logger.Info("Have a new sign up!");
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.error = "Tài khoản đã tồn tại";
                        logger.Info("Have a error when anonymous sign up!" + ViewBag.error);
                        return View();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return View(ex);
            }
        }

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
            string HashPassword = GetSHA256(objUser.MatKhau);
            if (ModelState.IsValid)
            {
                var obj = DataProvider.Entities.Users
                    .Where(u => u.TenDangNhap.Equals(objUser.TenDangNhap) && u.MatKhau.Equals(HashPassword)).FirstOrDefault();
                if (obj != null)
                {
                    logger.Info("Have a  user login! Usename: " + obj.TenDangNhap);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Vui lòng kiểm tra lại tài khoản hoặc mật khẩu";
                    logger.Info("Have a error when user sign in!" + "Wrong password or username");
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear(); //remove session 
            logger.Info("User Logout!");
            return RedirectToAction("Login", "Login");
        }

        /// <summary>
        /// Chuyển đổi một chuỗi về MD5
        /// </summary>
        /// <param name="str">Chuỗi cần băm</param>
        /// <returns></returns>
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