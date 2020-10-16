﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trippy_Land.Models;

namespace Trippy_Land.Controllers
{
    public class DiaDiemController : Controller
    {
        // GET: DiaDiem
        public ActionResult DanhSachDiaDiem()
        {
            HienThiDanhSachTinh();
            var lstDanhSachDiaDiem = DataProvider.Entities.DiaDiems.ToList();
            return View(lstDanhSachDiaDiem);
        }

        public ActionResult XoaDiaDiem(int Id)
        {
            //Lấy đối tượng địa điểm
            DiaDiem objDiaDiem = DataProvider.Entities.DiaDiems.Find(Id);
            if (objDiaDiem != null)
            {
                //Xóa
                DataProvider.Entities.DiaDiems.Remove(objDiaDiem);
                //Lưu thay đổi
                DataProvider.Entities.SaveChanges();
            }
            return RedirectToAction("DanhSachDiaDiem");
        }

        public ActionResult ThemDiaDiem()
        {
            HienThiDanhSachTinh();
            return View(new DiaDiem());
        }

        /// <summary>
        /// Hàm thêm mới khách sạn
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemDiaDiem(DiaDiem objDiaDiem, HttpPostedFileBase fUpload)
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
            }
            return RedirectToAction("DanhSachDiaDiem");
        }

        public ActionResult CapNhatDiaDiem(int Id)
        {
            HienThiDanhSachTinh();
            DiaDiem objDiaDiem = DataProvider.Entities.DiaDiems.Where(c => c.Id == Id).Single<DiaDiem>();

            return View(objDiaDiem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatDiaDiem(int Id, DiaDiem objDiaDiem, HttpPostedFileBase fUpload)
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
            }
            return RedirectToAction("DanhSachDiaDiem");
        }

        public void HienThiDanhSachTinh(int? idTinh = null)
        {
            List<Tinh> lstTinh = DataProvider.Entities.Tinhs.ToList();
            ViewBag.Tinh = new SelectList(lstTinh, "Id", "TenTinh", idTinh.HasValue ? idTinh.Value : 0);
        }
    }
}