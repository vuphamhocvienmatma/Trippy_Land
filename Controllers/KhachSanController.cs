﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trippy_Land.Models;

namespace Trippy_Land.Controllers
{
    public class KhachSanController : Controller
    {
        public ActionResult DanhSachKhachSan()
        {
            HienThiDanhSachTinh();
            var lstDanhSachKhachSan = DataProvider.Entities.KhachSans.ToList();
            return View(lstDanhSachKhachSan);
        }

        public ActionResult XoaKhachSan(int Id)
        {
            //Lấy đối tượng khách sạn
            KhachSan objKhachSan = DataProvider.Entities.KhachSans.Find(Id);
            if (objKhachSan != null)
            {
                //Xóa
                DataProvider.Entities.KhachSans.Remove(objKhachSan);
                //Lưu thay đổi
                DataProvider.Entities.SaveChanges();
            }
            return RedirectToAction("DanhSachKhachSan");
        }

        public ActionResult ThemKhachSan()
        {
            HienThiDanhSachTinh();
            return View(new KhachSan());
        }

        /// <summary>
        /// Hàm thêm mới khách sạn
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemKhachSan(KhachSan objKhachSan, HttpPostedFileBase fUpload)
        {
            if (ModelState.IsValid)
            {
                //Xử lý upload file
                if (fUpload != null &&
                    fUpload.ContentLength > 0)
                {
                    //Upload
                    fUpload.SaveAs(Server.MapPath("~/Content/Image/KhachSan/" + fUpload.FileName));
                    //Lưu vào db
                    objKhachSan.PictureId = fUpload.FileName;
                }
                //thêm vào database
                DataProvider.Entities.KhachSans.Add(objKhachSan);
                //Lưu thay đổi
                DataProvider.Entities.SaveChanges();
            }
            return RedirectToAction("DanhSachKhachSan");
        }

        public ActionResult CapNhatKhachSan(int Id)
        {
            HienThiDanhSachTinh();
            KhachSan objKhachSan = DataProvider.Entities.KhachSans.Where(c => c.Id == Id).Single<KhachSan>();

            return View(objKhachSan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatKhachSan(int Id, KhachSan objKhachSan, HttpPostedFileBase fUpload)
        {
            HienThiDanhSachTinh();
            var objOld_KhachSan = DataProvider.Entities.KhachSans.Find(Id);
            string img_Name = "";
            //Xử lý upload file
            if (fUpload != null &&
                fUpload.ContentLength > 0)
            {
                //Upload
                fUpload.SaveAs(Server.MapPath("~/Content/image/KhachSan/" + fUpload.FileName));
                //Lưu vào db
                objKhachSan.PictureId = fUpload.FileName;
                img_Name = fUpload.FileName;
            }
            if (objOld_KhachSan != null)
            {
                if (string.IsNullOrEmpty(img_Name))
                {
                    objKhachSan.PictureId = objOld_KhachSan.PictureId;
                }
                DataProvider.Entities.Entry(objOld_KhachSan).CurrentValues.SetValues(objKhachSan);
                //Lưu thay đổi
                DataProvider.Entities.SaveChanges();
            }
            return RedirectToAction("DanhSachKhachSan");
        }

        public void HienThiDanhSachTinh(int? idTinh = null)
        {
            List<Tinh> lstTinh = DataProvider.Entities.Tinhs.ToList();
            ViewBag.Tinh = new SelectList(lstTinh, "Id", "TenTinh", idTinh.HasValue ? idTinh.Value : 0);
        }
    }
}