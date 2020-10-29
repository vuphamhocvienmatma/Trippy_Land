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
    public class UserController : Controller
    {      
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

        public ActionResult DanhSachUser()
        {
            HienThiDanhSachUserRole();
            IQueryable<User>lstUser = DataProvider.Entities.Users;
            return View(lstUser);
        }

        public ActionResult XoaUser(int Id)
        {
            //Lấy đối tượng người dùng
            User objUser = DataProvider.Entities.Users.Find(Id);
            if (objUser != null)
            {
                //Xóa
                DataProvider.Entities.Users.Remove(objUser);
                //Lưu thay đổi
                DataProvider.Entities.SaveChanges();
            }
            return RedirectToAction("DanhSachUser");
        }

        public ActionResult ThemMoiUser()
        {
            HienThiDanhSachUserRole();
            return View(new User());
        }

        /// <summary>
        /// Hàm thêm mới người dùng
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemMoiUser(User objuser, HttpPostedFileBase fUpload)
        {
            if (ModelState.IsValid)
            {
                //Xử lý upload file
                if (fUpload != null &&
                    fUpload.ContentLength > 0)
                {
                    //Upload
                    fUpload.SaveAs(Server.MapPath("~/Content/Image/User/" + fUpload.FileName));
                    //Lưu vào db
                    objuser.PictureId = fUpload.FileName;
                }
                if(objuser.MatKhau != null)
                {
                    objuser.MatKhau = GetSHA256(objuser.MatKhau);
                }    
                //thêm vào database
                DataProvider.Entities.Users.Add(objuser);
                //Lưu thay đổi
                DataProvider.Entities.SaveChanges();
            }
            return RedirectToAction("DanhSachUser");
        }
    }
}