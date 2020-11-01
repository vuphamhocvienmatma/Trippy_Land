using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trippy_Land.Models;

namespace Trippy_Land.Areas.Admin.Controllers
{
    public class BaiVietVeDiaDiemController : Controller
    {
        public void HienThiDanhSachDiaDiem(int? idDiaDiem = null)
        {
            List<DiaDiem> lstDiaDiem = DataProvider.Entities.DiaDiems.ToList();

            ViewBag.DiaDiem = new SelectList(lstDiaDiem, "Id", "TenDiaDiem", idDiaDiem.HasValue ? idDiaDiem.Value : 0);
        }

        public void HienThiDanhSachChuDe(int? idChuDe = null)
        {
            List<ChuDeBaiVietVeDiaDiem> lstChuDe = DataProvider.Entities.ChuDeBaiVietVeDiaDiems.ToList();

            ViewBag.ChuDe = new SelectList(lstChuDe, "Id", "TenChuDe", idChuDe.HasValue ? idChuDe.Value : 0);
        }

        public ActionResult DanhSachBaiViet(string tuKhoa, int? idDiaDiem = null, int? idChuDe = null)
        {
            HienThiDanhSachDiaDiem();
            HienThiDanhSachChuDe();
            IQueryable<BaiVietVeDiaDiem> lstBaiViet = DataProvider.Entities.BaiVietVeDiaDiems;
            //tìm kiếm theo từ khóa
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                lstBaiViet = lstBaiViet.Where(c => c.TenBaiViet.Contains(tuKhoa));
            }
            //Tìm kiếm theo địa điểm
            if (idDiaDiem.HasValue)
            {
                lstBaiViet = lstBaiViet.Where(b => b.idDiaDiem == idDiaDiem.Value);
            }
            //Tìm kiếm theo chủ đề
            if (idChuDe.HasValue)
            {
                lstBaiViet = lstBaiViet.Where(b => b.IdChude == idChuDe.Value);
            }          
            return View(lstBaiViet);
           
        }

        //public ActionResult ThemMoiBaiViet()
        //{
        //    HienThiDanhSachDiaDiem();
        //    HienThiDanhSachChuDe();
        //    return View(new BaiVietVeDiaDiem());
        //}

        /// <summary>
        /// Hàm thêm mới bài viết
        /// </summary>
        /// <returns></returns>
        public ActionResult ThemMoiBaiViet(BaiVietVeDiaDiem objBaiViet, HttpPostedFileBase fUpload, int? idDiaDiem = null, int? idChuDe = null)
        {
            HienThiDanhSachDiaDiem();
            HienThiDanhSachChuDe();
            if (ModelState.IsValid)
            {
                //Xử lý upload file
                if (fUpload != null &&
                    fUpload.ContentLength > 0)
                {
                    //Upload
                    fUpload.SaveAs(Server.MapPath("~/Content/Image/BaiViet/" + fUpload.FileName));
                    //Lưu vào db
                    objBaiViet.PictureId = fUpload.FileName;
                }
                //thêm vào database
                DataProvider.Entities.BaiVietVeDiaDiems.Add(objBaiViet);
                //Lưu thay đổi
                DataProvider.Entities.SaveChanges();
            }
            return View(new BaiVietVeDiaDiem());
        }

        /// <summary>
        /// Hàm xóa một bài viết
        /// </summary>
        /// <returns></returns>
        public ActionResult XoaBaiViet(int Id)
        {
            //Lấy đối tượng
            BaiVietVeDiaDiem objBaiViet = DataProvider.Entities.BaiVietVeDiaDiems.Find(Id);
            if (objBaiViet != null)
            {
                //Xóa
                DataProvider.Entities.BaiVietVeDiaDiems.Remove(objBaiViet);
                //Lưu thay đổi
                DataProvider.Entities.SaveChanges();
            }
            return RedirectToAction("DanhSachBaiViet");
        }


        public ActionResult CapNhatBaiViet(int Id)
        {
            HienThiDanhSachDiaDiem();
            HienThiDanhSachChuDe();
            BaiVietVeDiaDiem objBaiViet = DataProvider.Entities.BaiVietVeDiaDiems.Where(c => c.Id == Id).Single();
            return View(objBaiViet);
        }

        /// <summary>
        /// Hàm cập nhật một bài viết
        /// </summary>
        /// <param name=""></param>
        /// <param name="fUpload"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatBaiViet(int Id, BaiVietVeDiaDiem objBaiViet, HttpPostedFileBase fUpload)
        {
            var objOld_BaiViet = DataProvider.Entities.BaiVietVeDiaDiems.Find(Id);
            string img_Name = "";
            //Xử lý upload file
            if (fUpload != null &&
                fUpload.ContentLength > 0)
            {
                //Upload
                fUpload.SaveAs(Server.MapPath("~/Content/Image/BaiViet/" + fUpload.FileName));
                //Lưu vào db
                objBaiViet.PictureId = fUpload.FileName;
                img_Name = fUpload.FileName;
            }
            if (objOld_BaiViet != null)
            {
                if (string.IsNullOrEmpty(img_Name))
                {
                    objBaiViet.PictureId = objOld_BaiViet.PictureId;
                }
                DataProvider.Entities.Entry(objOld_BaiViet).CurrentValues.SetValues(objBaiViet);
                //Lưu thay đổi
                DataProvider.Entities.SaveChanges();
            }

            return RedirectToAction("DanhSachBaiViet");
        }
    }
}