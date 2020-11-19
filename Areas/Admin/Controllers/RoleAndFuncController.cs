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
            ViewBag.Role = new SelectList(lstR, "Id", "TenRole", Id.HasValue ? Id.Value : 0);
        }

        [CheckAuthorize(PermissionName = "DanhSachRoleAndFunc")]
        public ActionResult DanhSachRoleAndFunc(string tuKhoa, int? idFunction, int? idRole)
        {
            try
            {
                HienThiDanhSachRole();
                HienThiDanhSachFunc();             
                IQueryable<UserRoleAndFunction> lstRAF = DataProvider.Entities.UserRoleAndFunctions;

                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    lstRAF = lstRAF.Where(c => c.Function.TenChucNang.Contains(tuKhoa) || c.UserRole.TenRole.Contains(tuKhoa));
                }
                //Tìm kiếm theo địa điểm
                if (idFunction.HasValue)
                {
                    lstRAF = lstRAF.Where(b => b.FuctionId == idFunction.Value);
                }
                //Tìm kiếm theo chủ đề
                if (idRole.HasValue)
                {
                    lstRAF = lstRAF.Where(b => b.UserRoleId == idRole.Value);
                }
                logger.Info("Have an access to admin page: RoleAndFunc ");
                return View(lstRAF);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }            
        }

        [CheckAuthorize(PermissionName = "AddRoleAndFunc")]
        [HttpGet]
        public ActionResult AddRoleAndFunc()
        {
           
            try
            {
                HienThiDanhSachRole();
                HienThiDanhSachFunc();
                return View(new UserRoleAndFunction());
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
        }


        [CheckAuthorize(PermissionName = "AddRoleAndFunc")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRoleAndFunc(UserRoleAndFunction objRoleAndFunc, int? idFunction, int? idRole)
        {
            
            try
            {
                HienThiDanhSachRole();
                HienThiDanhSachFunc();
                if(ModelState.IsValid)
                {
                    DataProvider.Entities.UserRoleAndFunctions.Add(objRoleAndFunc);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                    logger.Info("Add a UserRole and Function " +objRoleAndFunc.UserRole.TenRole
                        + objRoleAndFunc.Function.TenChucNang);
                }
                return RedirectToAction("DanhSachRoleAndFunc");              
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
        }

        [CheckAuthorize(PermissionName = "EditRoleAndFunc")]
        [HttpGet]
        public ActionResult EditRoleAndFunc(int Id)
        {
          
            try
            {
                HienThiDanhSachRole();
                HienThiDanhSachFunc();
                UserRoleAndFunction objRAF = DataProvider.Entities.UserRoleAndFunctions.Where(c => c.Id == Id).Single();
                return View(objRAF);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
        }

        [CheckAuthorize(PermissionName = "EditRoleAndFunc")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoleAndFunc(int Id, UserRoleAndFunction objRAF)
        {

            try
            {
                HienThiDanhSachRole();
                HienThiDanhSachFunc();
                var objOld_RAF = DataProvider.Entities.UserRoleAndFunctions.Find(Id);         
                //Xử lý upload file            
                if (objOld_RAF != null)
                {                 
                    DataProvider.Entities.Entry(objOld_RAF).CurrentValues.SetValues(objRAF);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                }
                logger.Info("Update a UserRole and Function ");
                return RedirectToAction("DanhSachRoleAndFunc");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
        }



        [CheckAuthorize(PermissionName = "RemoveRoleAndFunc")]
        public ActionResult RemoveRoleAndFunc(int Id)
        {          
            try
            {
                UserRoleAndFunction objRAF = DataProvider.Entities.UserRoleAndFunctions.Find(Id);
                if (objRAF != null)
                {
                    //Xóa
                    DataProvider.Entities.UserRoleAndFunctions.Remove(objRAF);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                    logger.Info("Delete a Role And Function ");
                }
                return RedirectToAction("DanhSachRoleAndFunc");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
        }
    }
}