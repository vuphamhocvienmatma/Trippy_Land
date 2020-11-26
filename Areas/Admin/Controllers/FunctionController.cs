using log4net;
using System;
using System.Linq;
using System.Web.Mvc;
using Trippy_Land.Attribute;
using Trippy_Land.Models;

namespace Trippy_Land.Areas.Admin.Controllers
{
    public class FunctionController : Controller
    {
        private static readonly ILog logger =
           LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: Admin/Function
        [CheckAuthorize(PermissionName = "DanhSachFunction")]
        public ActionResult DanhSachFunction()
        {
            try
            {
                var lstF = DataProvider.Entities.Function.OrderBy(o => o.Id).ToList();
                logger.Info("Have an access to admin page: Function ");
                return View(lstF);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
        }
        [CheckAuthorize(PermissionName = "ThemMoiFunction")]
        public ActionResult ThemMoiFunction()
        {
            try
            {
                return View(new Function());
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
        }

        /// <summary>
        /// Hàm thêm mới một chủ đề
        /// </summary>
        /// <param name="objTinh"></param>
        /// <param name="fUpload"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckAuthorize(PermissionName = "ThemMoiFunction")]
        public ActionResult ThemMoiFunction(Function objF)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //thêm vào database
                    DataProvider.Entities.Function.Add(objF);
                    logger.Info("Add Function: " + objF.TenChucNang);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                }
                return RedirectToAction("DanhSachFunction");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
        }

        /// <summary>
        /// Hàm xóa một chủ đề
        /// </summary>
        /// <returns></returns>
        [CheckAuthorize(PermissionName = "XoaFunction")]
        public ActionResult XoaFunction(int Id)
        {
            try
            {
                Function objCF = DataProvider.Entities.Function.Find(Id);
                if (objCF != null)
                {
                    //Xóa
                    DataProvider.Entities.Function.Remove(objCF);
                    logger.Info("Xóa 1 Function: " + objCF.TenChucNang);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                }
                return RedirectToAction("DanhSachFunction");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
            //Lấy đối tượng chủ đề

        }
        [CheckAuthorize(PermissionName = "CapNhatFunction")]
        public ActionResult CapNhatFunction(int Id)
        {
            try
            {
                Function objCF = DataProvider.Entities.Function.Where(c => c.Id == Id).Single();
                return View(objCF);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }

        }

        /// <summary>
        /// Hàm cập nhật một chức năng
        /// </summary>
        /// <param name="objTinh"></param>
        /// <param name="fUpload"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckAuthorize(PermissionName = "CapNhatFunction")]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatFunction(int Id, Function objF)
        {
            try
            {
                var objOld_F = DataProvider.Entities.Function.Find(Id);

                if (objOld_F != null)
                {

                    DataProvider.Entities.Entry(objOld_F).CurrentValues.SetValues(objF);
                    logger.Info("Update Function: " + objF.TenChucNang);

                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                }
                return RedirectToAction("DanhSachFunction");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }

        }
    }
}