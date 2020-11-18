using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trippy_Land.Attribute;
using Trippy_Land.Models;

namespace Trippy_Land.Areas.Admin.Controllers
{
    public class RoleAndFuncController : Controller
    {
        private static readonly ILog logger =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Admin/RoleAndFunc

        public void HienThiDanhSachFunc(int? Id =null)
        {
            var lstF = DataProvider.Entities.Function.ToList();
            ViewBag.Function = new SelectList(lstF, "Id", "TenChucNang", Id.HasValue ? Id.Value : 0);
        }

        public void HienThiDanhSachRole(int? Id = null)
        {
            var lstR = DataProvider.Entities.UserRoles.ToList();
            ViewBag.Function = new SelectList(lstR, "Id", "TenRole", Id.HasValue ? Id.Value : 0);
        }
        //[CheckAuthorize(PermissionName = "DanhSachRoleAndFunc")]
        public ActionResult DanhSachRoleAndFunc(string tuKhoa, int? idFunction)
        {
            try
            {
                HienThiDanhSachRole();
                HienThiDanhSachFunc();
                var lstRAF = DataProvider.Entities.UserRoleAndFunctions.ToList();
                logger.Info("Have an access to admin page: RoleAndFunc ");
                return View(lstRAF);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }            
        }

        public ActionResult AddRoleAndFunc()
        {
            try
            {             
                return View();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
        }

        public ActionResult EditRoleAndFunc()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
        }

        public ActionResult RemoveRoleAndFunc()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
        }
    }
}