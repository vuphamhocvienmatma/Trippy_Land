using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trippy_Land.Models;

namespace Trippy_Land.Areas.Admin.Controllers
{
    public class MonAnController : Controller
    {
        private static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult DanhSachMonAn(string tuKhoa, int? idTinh)
        {
            try
            {
                HienThiDanhSachTinh();
                IQueryable<MonAn> lstDanhSachMonAn = DataProvider.Entities.MonAns;
                //tìm kiếm theo từ khóa
                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    lstDanhSachMonAn = lstDanhSachMonAn.Where(c => c.TenMonAn.Contains(tuKhoa));
                }
                //Tìm kiếm theo loại khách hàng
                if (idTinh.HasValue)
                {
                    lstDanhSachMonAn = lstDanhSachMonAn.Where(b => b.idTinh == idTinh.Value);
                }
                logger.Info("Have an access to Admin page: Food");
                return View(lstDanhSachMonAn);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
          
        }

        public ActionResult XoaMonAn(int Id)
        {
            try
            {
                //Lấy đối tượng món ăn
                MonAn objMonan = DataProvider.Entities.MonAns.Find(Id);
                if (objMonan != null)
                {
                    //Xóa
                    DataProvider.Entities.MonAns.Remove(objMonan);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                    logger.Info("Delete a food: " + objMonan.TenMonAn);
                }
                return RedirectToAction("DanhSachMonAn");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
          
        }

        public ActionResult ThemMonAn()
        {
            try
            {
                HienThiDanhSachTinh();
                return View(new MonAn());
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }         
        }

        /// <summary>
        /// Hàm thêm mới món ăn
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemMonAn(MonAn objMonAn, HttpPostedFileBase fUpload)
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
                        fUpload.SaveAs(Server.MapPath("~/Content/Image/MonAn/" + fUpload.FileName));
                        //Lưu vào db
                        objMonAn.PictureId = fUpload.FileName;
                    }
                    //thêm vào database
                    DataProvider.Entities.MonAns.Add(objMonAn);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                    logger.Info("Add a food: " + objMonAn.TenMonAn);
                }
                return RedirectToAction("DanhSachMonAn");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
           
        }

        public ActionResult CapNhatMonAn(int Id)
        {
            try
            {
                HienThiDanhSachTinh();
                MonAn objMonAn = DataProvider.Entities.MonAns.Where(c => c.Id == Id).Single<MonAn>();
                return View(objMonAn);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatMonAn(int Id, MonAn objMonAn, HttpPostedFileBase fUpload)
        {
            try
            {
                HienThiDanhSachTinh();
                var objOld_MonAn = DataProvider.Entities.MonAns.Find(Id);
                string img_Name = "";
                //Xử lý upload file
                if (fUpload != null &&
                    fUpload.ContentLength > 0)
                {
                    //Upload
                    fUpload.SaveAs(Server.MapPath("~/Content/image/MonAn/" + fUpload.FileName));
                    //Lưu vào db
                    objMonAn.PictureId = fUpload.FileName;
                    img_Name = fUpload.FileName;
                }
                if (objOld_MonAn != null)
                {
                    if (string.IsNullOrEmpty(img_Name))
                    {
                        objMonAn.PictureId = objOld_MonAn.PictureId;
                    }
                    DataProvider.Entities.Entry(objOld_MonAn).CurrentValues.SetValues(objMonAn);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                }
                logger.Info("Update a food: " + objMonAn.TenMonAn);
                return RedirectToAction("DanhSachMonAn");
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