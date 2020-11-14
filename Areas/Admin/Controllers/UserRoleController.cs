using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trippy_Land.Models;

namespace Trippy_Land.Areas.Admin.Controllers
{
    public class UserRoleController : Controller
    {
        private static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult DanhSachUserRole()
        {
            try
            {
                IQueryable<UserRole> lstUserRole = DataProvider.Entities.UserRoles;
                logger.Info("Have an access to UserRole Page!");
                return View(lstUserRole);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return View(ex);
            }
            
        }

        public ActionResult XoaUserRole(int Id)
        {
            try
            {
                //Lấy đối tượng tỉnh
                UserRole objUserRole = DataProvider.Entities.UserRoles.Find(Id);
                if (objUserRole != null)
                {
                    //Xóa
                    DataProvider.Entities.UserRoles.Remove(objUserRole);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                }
                logger.Info("Delete an UserRole");
                return RedirectToAction("DanhSachUserRole");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return View(ex);
            }
            
        }

        public ActionResult ThemMoiUserRole()
        {
            return View();
        }

        /// <summary>
        /// Hàm thêm mới Role
        /// </summary>
        /// <param name="objTinh"></param>
        /// <param name="fUpload"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemMoiUserRole(UserRole objUserRole)
        {
            try
            {
                DataProvider.Entities.UserRoles.Add(objUserRole);
                //Lưu thay đổi
                DataProvider.Entities.SaveChanges();
                logger.Info("Add an UserRole");
                return RedirectToAction("DanhSachUserRole");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return View(ex);
            }              
        }
    }
}