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

    public class DiaDiemController : Controller
    {
        private static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: DiaDiem
        [CheckAuthorize(PermissionName = "DanhSachDiaDiem")]
        public ActionResult DanhSachDiaDiem(string tuKhoa, int? idTinh)
        {
            try
            {
                HienThiDanhSachTinh();
                IQueryable<DiaDiem> lstDiaDiem = DataProvider.Entities.DiaDiems;
                //tìm kiếm theo từ khóa
                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    lstDiaDiem = lstDiaDiem.Where(c => c.TenDiaDiem.Contains(tuKhoa) || c.HoatDongChinh.Contains(tuKhoa));
                }
                //Tìm kiếm theo loại khách hàng
                if (idTinh.HasValue)
                {
                    lstDiaDiem = lstDiaDiem.Where(b => b.idTinh == idTinh.Value);
                }
                logger.Info("Have an access to Admin page: Location");
                return View(lstDiaDiem);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }

        }
        [CheckAuthorize(PermissionName = "XoaDiaDiem")]
        public ActionResult XoaDiaDiem(int Id)
        {
            try
            {   
                DiaDiem objDiaDiem = DataProvider.Entities.DiaDiems.Find(Id);
                if (objDiaDiem != null)
                {
                    //Xóa
                    DataProvider.Entities.DiaDiems.Remove(objDiaDiem);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                    logger.Info("Delete a Location: " + objDiaDiem.TenDiaDiem);
                }
                return RedirectToAction("DanhSachDiaDiem");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
            //Lấy đối tượng địa điểm

        }
        [CheckAuthorize(PermissionName = "ThemDiaDiem")]
        public ActionResult ThemDiaDiem()
        {
            try
            {
                HienThiDanhSachTinh();
                return View(new DiaDiem());
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }

        }

        /// <summary>
        /// Hàm thêm mới khách sạn
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckAuthorize(PermissionName = "ThemDiaDiem")]
        public ActionResult ThemDiaDiem(DiaDiem objDiaDiem, HttpPostedFileBase fUpload)
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
                        fUpload.SaveAs(Server.MapPath("~/Content/Image/DiaDiem/" + fUpload.FileName));
                        //Lưu vào db
                        objDiaDiem.PictureId = fUpload.FileName;
                    }
                    //thêm vào database
                    DataProvider.Entities.DiaDiems.Add(objDiaDiem);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                    logger.Info("Add a Location: " + objDiaDiem.TenDiaDiem);
                }
                return RedirectToAction("DanhSachDiaDiem");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");

            }
        }
        [CheckAuthorize(PermissionName = "CapNhatDiaDiem")]
        public ActionResult CapNhatDiaDiem(int Id)
        {
            try
            {
                HienThiDanhSachTinh();
                DiaDiem objDiaDiem = DataProvider.Entities.DiaDiems.Where(c => c.Id == Id).Single<DiaDiem>();

                return View(objDiaDiem);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckAuthorize(PermissionName = "CapNhatDiaDiem")]
        public ActionResult CapNhatDiaDiem(int Id, DiaDiem objDiaDiem, HttpPostedFileBase fUpload)
        {
            try
            {
                HienThiDanhSachTinh();
                var objOld_DiaDiem = DataProvider.Entities.DiaDiems.Find(Id);
                string img_Name = "";
                //Xử lý upload file
                if (fUpload != null &&
                    fUpload.ContentLength > 0)
                {
                    //Upload
                    fUpload.SaveAs(Server.MapPath("~/Content/image/DiaDiem/" + fUpload.FileName));
                    //Lưu vào db
                    objDiaDiem.PictureId = fUpload.FileName;
                    img_Name = fUpload.FileName;
                }
                if (objOld_DiaDiem != null)
                {
                    if (string.IsNullOrEmpty(img_Name))
                    {
                        objDiaDiem.PictureId = objOld_DiaDiem.PictureId;
                    }
                    DataProvider.Entities.Entry(objOld_DiaDiem).CurrentValues.SetValues(objDiaDiem);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                    logger.Info("Update a Location: " + objDiaDiem.TenDiaDiem);
                }
                return RedirectToAction("DanhSachDiaDiem");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }

        }

        public void HienThiDanhSachTinh(int? idTinh = null)
        {
            List<Tinh> lstTinh = DataProvider.Entities.Tinhs.ToList();
            ViewBag.Tinh = new SelectList(lstTinh, "Id", "TenTinh", idTinh.HasValue ? idTinh.Value : 0);
        }
    }
}