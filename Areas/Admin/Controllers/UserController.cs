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
    
    public class UserController : Controller
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
                //tìm kiếm theo từ khóa
                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    lstUser = lstUser.Where(c => c.TenDangNhap.Contains(tuKhoa) || c.TenNguoiDung.Contains(tuKhoa) || c.PhoneNumber.ToString().Contains(tuKhoa));
                }
                //Tìm kiếm theo loại khách hàng
                if (idUserRole.HasValue)
                {
                    lstUser = lstUser.Where(b => b.UserRoleId == idUserRole.Value);
                }
                logger.Info("Have an access to User Page");
                return View(lstUser);
            }
            catch (Exception ex)
            {

                logger.Error(ex.ToString());
                return RedirectToAction("Return", "ErrorPage");
            }
            
        }

        public ActionResult XoaUser(int Id)
        {
            try
            {
                //Lấy đối tượng người dùng
                User objUser = DataProvider.Entities.Users.Find(Id);
                if (objUser != null)
                {
                    logger.Info("Delete User" + objUser.TenDangNhap);
                    //Xóa
                    DataProvider.Entities.Users.Remove(objUser);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                }             
                return RedirectToAction("DanhSachUser");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("Return", "ErrorPage");
            }
          
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
            try
            {
                HienThiDanhSachUserRole();
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
                    if (objuser.MatKhau != null)
                    {
                        objuser.MatKhau = GetSHA256(objuser.MatKhau);
                    }
                    //thêm vào database
                    DataProvider.Entities.Users.Add(objuser);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                    logger.Info("Add an User" + objuser.TenDangNhap);
                }

                return RedirectToAction("DanhSachUser");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("Return", "ErrorPage");
            }          
        }



        public ActionResult CapNhatUser(int Id)
        {
            HienThiDanhSachUserRole();
            User objUser = DataProvider.Entities.Users.Where(c => c.Id == Id).Single<User>();
            return View(objUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatUser(int Id, User objUser, HttpPostedFileBase fUpload)
        {
            try
            {
                HienThiDanhSachUserRole();
                var objOld_User = DataProvider.Entities.Users.Find(Id);
                string img_Name = "";
                if (objUser.MatKhau != null)
                {
                    objUser.MatKhau = GetSHA256(objUser.MatKhau);
                }
                //Xử lý upload file
                if (fUpload != null &&
                    fUpload.ContentLength > 0)
                {
                    //Upload
                    fUpload.SaveAs(Server.MapPath("~/Content/image/User/" + fUpload.FileName));
                    //Lưu vào db
                    objUser.PictureId = fUpload.FileName;
                    img_Name = fUpload.FileName;
                }
                if (objOld_User != null)
                {
                    if (string.IsNullOrEmpty(img_Name))
                    {
                        objUser.PictureId = objOld_User.PictureId;
                    }
                    DataProvider.Entities.Entry(objOld_User).CurrentValues.SetValues(objUser);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                    logger.Info("Update an User" + objUser.TenDangNhap);
                }
                return RedirectToAction("DanhSachUser");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("Return", "ErrorPage");
            }
          
        }

    }
}