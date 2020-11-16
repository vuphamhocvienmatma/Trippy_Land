using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trippy_Land.Models;

namespace Trippy_Land.Areas.Admin.Controllers
{
    public class KhachSanController : Controller
    {
        private static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult DanhSachKhachSan(string tuKhoa, int? idTinh)
        {
            try
            {
                HienThiDanhSachTinh();
                IQueryable<KhachSan> lstDanhSachKhachSan = DataProvider.Entities.KhachSans;
                //tìm kiếm theo từ khóa
                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    lstDanhSachKhachSan = lstDanhSachKhachSan.Where(c => c.TenKhachSan.Contains(tuKhoa) || c.DiaDiemChiTiet.Contains(tuKhoa));
                }
                //Tìm kiếm theo loại khách hàng
                if (idTinh.HasValue)
                {
                    lstDanhSachKhachSan = lstDanhSachKhachSan.Where(b => b.idTinh == idTinh.Value);
                }
                logger.Info("Have an access to Admin page: Hotel");
                return View(lstDanhSachKhachSan);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }

        }

        public ActionResult XoaKhachSan(int Id)
        {
            try
            {
                //Lấy đối tượng khách sạn
                KhachSan objKhachSan = DataProvider.Entities.KhachSans.Find(Id);
                if (objKhachSan != null)
                {
                    //Xóa
                    DataProvider.Entities.KhachSans.Remove(objKhachSan);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                    logger.Info("Delete a hotel: " + objKhachSan.TenKhachSan);

                }
                return RedirectToAction("DanhSachKhachSan");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }

        }

        public ActionResult ThemKhachSan()
        {
            try
            {
                HienThiDanhSachTinh();
                return View(new KhachSan());
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
        public ActionResult ThemKhachSan(KhachSan objKhachSan, HttpPostedFileBase fUpload)
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
                        fUpload.SaveAs(Server.MapPath("~/Content/Image/KhachSan/" + fUpload.FileName));
                        //Lưu vào db
                        objKhachSan.PictureId = fUpload.FileName;
                    }
                    //thêm vào database
                    DataProvider.Entities.KhachSans.Add(objKhachSan);
                    //Lưu thay đổi
                    DataProvider.Entities.SaveChanges();
                    logger.Info("Add a Hotel: " + objKhachSan.TenKhachSan);
                }
                return RedirectToAction("DanhSachKhachSan");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }

        }

        public ActionResult CapNhatKhachSan(int Id)
        {
            try
            {
                HienThiDanhSachTinh();
                KhachSan objKhachSan = DataProvider.Entities.KhachSans.Where(c => c.Id == Id).Single<KhachSan>();
                return View(objKhachSan);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Redirect("~/ErrorPage/Return");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatKhachSan(int Id, KhachSan objKhachSan, HttpPostedFileBase fUpload)
        {
            try
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
                    logger.Info("Update a hotel: " + objKhachSan.TenKhachSan);
                }
                return RedirectToAction("DanhSachKhachSan");
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