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
        
        public ActionResult DanhSachUserRole()
        {
            IQueryable<UserRole> lstUserRole = DataProvider.Entities.UserRoles;
            return View(lstUserRole);
        }

        public ActionResult XoaUserRole(int Id)
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
            return RedirectToAction("DanhSachUserRole");
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
                //thêm vào database
                DataProvider.Entities.UserRoles.Add(objUserRole);
                //Lưu thay đổi
                DataProvider.Entities.SaveChanges();           
            return RedirectToAction("DanhSachUserRole");
        }
    }
}