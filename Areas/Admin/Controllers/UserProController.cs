using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Trippy_Land.Models;

namespace Trippy_Land.Areas.Admin.Controllers
{
    public class UserProController : Controller
    {
        private static readonly ILog logger =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
        //Hiển thị danh sách UserRole
        public void HienThiDanhSachUserRole(int? idUserRole = null)
        {
            List<UserRole> lstUserRole = DataProvider.Entities.UserRoles.ToList();

            ViewBag.UserRole = new SelectList(lstUserRole, "Id", "TenRole", idUserRole.HasValue ? idUserRole.Value : 0);
        }

        public ActionResult DanhSachUser(string tuKhoa, int? idUserRole)
        {
            try
            {
                HienThiDanhSachUserRole();
                IQueryable<User> lstUser = DataProvider.Entities.Users;
                
                logger.Info("Have an access to User Page");
                return View(lstUser);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("Return", "ErrorPage");
            }

        }
    }
}