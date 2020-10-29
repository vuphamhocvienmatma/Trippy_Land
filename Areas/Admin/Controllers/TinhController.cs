using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trippy_Land.Models;

namespace Trippy_Land.Areas.Admin.Controllers
{
    public class TinhController : Controller
    {
        /// <summary>
        /// Hàm hiển thị danh sách toàn bộ các tỉnh
        /// </summary>
        /// <returns></returns>
        public ActionResult DanhSachTinh(string tuKhoa)
        {
            IQueryable<Tinh> lstTinh = DataProvider.Entities.Tinhs;
            //tìm kiếm theo từ khóa
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                lstTinh = lstTinh.Where(c => c.TenTinh.Contains(tuKhoa));
            }
            return View(lstTinh);
        }

        public ActionResult ThemMoiTinh()
        {
            return View();
        }

        /// <summary>
        /// Hàm thêm mới một tỉnh
        /// </summary>
        /// <param name="objTinh"></param>
        /// <param name="fUpload"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemMoiTinh(Tinh objTinh, HttpPostedFileBase fUpload)
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
            }
            return RedirectToAction("DanhSachTinh");
        }

        public ActionResult CapNhatTinh(int Id)
        {
            Tinh objTinh = DataProvider.Entities.Tinhs.Where(c => c.Id == Id).Single();
            return View(objTinh);
        }

        /// <summary>
        /// Hàm cập nhật một tỉnh
        /// </summary>
        /// <param name="objTinh"></param>
        /// <param name="fUpload"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatTinh(int Id, Tinh objTinh, HttpPostedFileBase fUpload)
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
            }

            return RedirectToAction("DanhSachTinh");
        }

        /// <summary>
        /// Hàm xóa một tỉnh
        /// </summary>
        /// <returns></returns>
        public ActionResult XoaTinh(int Id)
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
            return RedirectToAction("DanhSachTinh");
        }
    }
}