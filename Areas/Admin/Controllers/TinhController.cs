using log4net;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trippy_Land.Attribute;
using Trippy_Land.Models;

namespace Trippy_Land.Areas.Admin.Controllers
{
   
    public class TinhController : Controller
    {
        private static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Hàm hiển thị danh sách toàn bộ các tỉnh
        /// </summary>
        /// <returns></returns>
        [CheckAuthorize(PermissionName = "DanhSachTinh")]
        public ActionResult DanhSachTinh(string tuKhoa)
        {
            try
            {
                IQueryable<Tinh> lstTinh = DataProvider.Entities.Tinhs;
                //tìm kiếm theo từ khóa
                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    lstTinh = lstTinh.Where(c => c.TenTinh.Contains(tuKhoa));
                }
                logger.Info("Have an access to Admin page: Province");
                return View(lstTinh);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }

        }

        [CheckAuthorize(PermissionName = "ThemMoiTinh")]
        public ActionResult ThemMoiTinh()
        {
            try
            {
                return View(new Tinh());
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
        }

        /// <summary>
        /// Hàm thêm mới một tỉnh
        /// </summary>
        /// <param name="objTinh"></param>
        /// <param name="fUpload"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckAuthorize(PermissionName = "ThemMoiTinh")]
        public ActionResult ThemMoiTinh(Tinh objTinh, HttpPostedFileBase fUpload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Xử lý upload file
                    if (fUpload != null &&
                        fUpload.ContentLength > 0)
                    {
                        //Upload
                        fUpload.SaveAs(Server.MapPath("~/Content/Image/Tinh/" + fUpload.FileName));
                        //Lưu vào db
                        objTinh.PictureId = fUpload.FileName;
                    }

                    //thêm vào database
                    DataProvider.Entities.Tinhs.Add(objTinh);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                    logger.Info("Add a Province: " + objTinh.TenTinh);
                }
                return RedirectToAction("DanhSachTinh");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
        }

        [CheckAuthorize(PermissionName = "CapNhatTinh")]
        public ActionResult CapNhatTinh(int Id)
        {
            try
            {
                Tinh objTinh = DataProvider.Entities.Tinhs.Where(c => c.Id == Id).Single();
                logger.Info("Update a Province: " + objTinh.TenTinh);
                return View(objTinh);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
        }

        /// <summary>
        /// Hàm cập nhật một tỉnh
        /// </summary>
        /// <param name="objTinh"></param>
        /// <param name="fUpload"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckAuthorize(PermissionName = "CapNhatTinh")]
        public ActionResult CapNhatTinh(int Id, Tinh objTinh, HttpPostedFileBase fUpload)
        {
            try
            {
                var objOld_Tinh = DataProvider.Entities.Tinhs.Find(Id);
                string img_Name = "";
                //Xử lý upload file
                if (fUpload != null &&
                    fUpload.ContentLength > 0)
                {
                    //Upload
                    fUpload.SaveAs(Server.MapPath("~/Content/Image/Tinh/" + fUpload.FileName));
                    //Lưu vào db
                    objTinh.PictureId = fUpload.FileName;
                    img_Name = fUpload.FileName;
                }
                if (objOld_Tinh != null)
                {
                    if (string.IsNullOrEmpty(img_Name))
                    {
                        objTinh.PictureId = objOld_Tinh.PictureId;
                    }
                    DataProvider.Entities.Entry(objOld_Tinh).CurrentValues.SetValues(objTinh);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                    logger.Info("Update a Province: " + objTinh.TenTinh);
                }

                return RedirectToAction("DanhSachTinh");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }

        }

        /// <summary>
        /// Hàm xóa một tỉnh
        /// </summary>
        /// <returns></returns>
        public ActionResult XoaTinh(int Id)
        {
            try
            {
                //Lấy đối tượng tỉnh
                Tinh objTinh = DataProvider.Entities.Tinhs.Find(Id);
                if (objTinh != null)
                {
                    //Xóa
                    DataProvider.Entities.Tinhs.Remove(objTinh);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                }
                logger.Info("Delete a Province: " + objTinh.TenTinh);
                return RedirectToAction("DanhSachTinh");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }

        }
    }
}