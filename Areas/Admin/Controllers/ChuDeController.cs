using log4net;
using System;
using System.Linq;
using System.Web.Mvc;
using Trippy_Land.Models;

namespace Trippy_Land.Areas.Admin.Controllers
{
    
    public class ChuDeController : Controller
    {
        private static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult DanhSachChuDe()
        {
            try
            {
                var lstChuDe = DataProvider.Entities.ChuDeBaiVietVeDiaDiems.ToList();
                logger.Info("Have an access to admin page: Chude ");
                return View(lstChuDe);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }

        }

        public ActionResult ThemMoiChuDe()
        {
            try
            {
                return View(new ChuDeBaiVietVeDiaDiem());
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
        public ActionResult ThemMoiChuDe(ChuDeBaiVietVeDiaDiem objChuDe)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //thêm vào database
                    DataProvider.Entities.ChuDeBaiVietVeDiaDiems.Add(objChuDe);
                    logger.Info("Add Chude: " + objChuDe.TenChuDe);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                }
                return RedirectToAction("DanhSachChuDe");
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
        public ActionResult XoaChuDe(int Id)
        {
            try
            {
                ChuDeBaiVietVeDiaDiem objChuDe = DataProvider.Entities.ChuDeBaiVietVeDiaDiems.Find(Id);
                if (objChuDe != null)
                {
                    //Xóa
                    DataProvider.Entities.ChuDeBaiVietVeDiaDiems.Remove(objChuDe);
                    logger.Info("Xóa 1 chủ đề: " + objChuDe.TenChuDe);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                }
                return RedirectToAction("DanhSachChuDe");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
            //Lấy đối tượng chủ đề

        }

        public ActionResult CapNhatChuDe(int Id)
        {
            try
            {
                ChuDeBaiVietVeDiaDiem objChuDe = DataProvider.Entities.ChuDeBaiVietVeDiaDiems.Where(c => c.Id == Id).Single();
                return View(objChuDe);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }

        }

        /// <summary>
        /// Hàm cập nhật một chủ đề
        /// </summary>
        /// <param name="objTinh"></param>
        /// <param name="fUpload"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatChuDe(int Id, ChuDeBaiVietVeDiaDiem objChuDe)
        {
            try
            {
                var objOld_ChuDe = DataProvider.Entities.ChuDeBaiVietVeDiaDiems.Find(Id);

                if (objOld_ChuDe != null)
                {
                    DataProvider.Entities.Entry(objOld_ChuDe).CurrentValues.SetValues(objChuDe);
                    logger.Info("Update chủ đề: " + objChuDe.TenChuDe);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                }
                return RedirectToAction("DanhSachChuDe");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }

        }
    }
}