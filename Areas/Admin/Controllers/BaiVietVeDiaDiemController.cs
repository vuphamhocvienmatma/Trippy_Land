using log4net;
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
        private static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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

        public ActionResult DanhSachBaiViet(DateTime? date,string tuKhoa, int? idDiaDiem = null, int? idChuDe = null)
        {
            try
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
                if (date.HasValue)
                {
                    lstBaiViet = lstBaiViet.Where(b => b.DataCreated.Day == date.Value.Day
                    && b.DataCreated.Month == date.Value.Month
                    && b.DataCreated.Year == date.Value.Year);
                }
                logger.Info("Have an access to Admin page: Blog");
                return View(lstBaiViet);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
               
        }

        public ActionResult ThemMoiBaiViet()
        {
            try
            {
                HienThiDanhSachDiaDiem();
                HienThiDanhSachChuDe();
                return View(new BaiVietVeDiaDiem());
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
         
        }

        /// <summary>
        /// Hàm thêm mới bài viết
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemMoiBaiViet(BaiVietVeDiaDiem objBaiViet, HttpPostedFileBase fUpload, int? idDiaDiem = null, int? idChuDe = null)
        {
            try
            {
                HienThiDanhSachDiaDiem();
                HienThiDanhSachChuDe();
                if (ModelState.IsValid)
                {

                    objBaiViet.DataCreated = DateTime.Now;
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
                    logger.Info("Add a Blog " + objBaiViet.TenBaiViet);
                }
                return RedirectToAction("DanhSachBaiViet");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
         
        }

        /// <summary>
        /// Hàm xóa một bài viết
        /// </summary>
        /// <returns></returns>
        public ActionResult XoaBaiViet(int Id)
        {
            try
            {
                BaiVietVeDiaDiem objBaiViet = DataProvider.Entities.BaiVietVeDiaDiems.Find(Id);
                if (objBaiViet != null)
                {
                    //Xóa
                    DataProvider.Entities.BaiVietVeDiaDiems.Remove(objBaiViet);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                    logger.Info("Delete a Blog " + objBaiViet.TenBaiViet);
                }
                return RedirectToAction("DanhSachBaiViet");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
            //Lấy đối tượng
         
        }


        public ActionResult CapNhatBaiViet(int Id)
        {
            try
            {
                HienThiDanhSachDiaDiem();
                HienThiDanhSachChuDe();
                BaiVietVeDiaDiem objBaiViet = DataProvider.Entities.BaiVietVeDiaDiems.Where(c => c.Id == Id).Single();
                return View(objBaiViet);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
           
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
            try
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
                        objBaiViet.DataCreated = objOld_BaiViet.DataCreated;
                        objBaiViet.PictureId = objOld_BaiViet.PictureId;
                    }
                    DataProvider.Entities.Entry(objOld_BaiViet).CurrentValues.SetValues(objBaiViet);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                }
                logger.Info("Update a Blog " + objBaiViet.TenBaiViet);
                return RedirectToAction("DanhSachBaiViet");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
           
        }
    }
}