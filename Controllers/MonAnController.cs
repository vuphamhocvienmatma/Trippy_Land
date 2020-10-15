using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trippy_Land.Models;

namespace Trippy_Land.Controllers
{
    public class MonAnController : Controller
    {
        public ActionResult DanhSachMonAn()
        {
            HienThiDanhSachTinh();
            var lstDanhSachMonAn = DataProvider.Entities.MonAns.ToList();
            return View(lstDanhSachMonAn);
        }

        public ActionResult XoaMonAn(int Id)
        {
            //Lấy đối tượng món ăn
            MonAn objMonan = DataProvider.Entities.MonAns.Find(Id);
            if (objMonan != null)
            {
                //Xóa
                DataProvider.Entities.MonAns.Remove(objMonan);
                //Lưu thay đổi
                DataProvider.Entities.SaveChanges();
            }
            return RedirectToAction("DanhSachMonAn");
        }

        public ActionResult ThemMonAn()
        {
            HienThiDanhSachTinh();
            return View(new MonAn());
        }

        /// <summary>
        /// Hàm thêm mới món ăn
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemMonAn(MonAn objMonAn, HttpPostedFileBase fUpload)
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
            }
            return RedirectToAction("DanhSachMonAn");
        }

        public ActionResult CapNhatMonAn()
        {
            return View();
        }

        public void HienThiDanhSachTinh(int? idTinh = null)
        {
            List<Tinh> lstTinh = DataProvider.Entities.Tinhs.ToList();
            ViewBag.Tinh = new SelectList(lstTinh, "Id", "TenTinh", idTinh.HasValue ? idTinh.Value : 0);
        }
    }
}