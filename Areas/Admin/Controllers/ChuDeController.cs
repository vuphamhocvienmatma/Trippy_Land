using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trippy_Land.Models;

namespace Trippy_Land.Areas.Admin.Controllers
{
    public class ChuDeController : Controller
    {
        private static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult DanhSachChuDe()
        {
            var lstChuDe = DataProvider.Entities.ChuDeBaiVietVeDiaDiems.ToList();

            return View(lstChuDe);
        }

        public ActionResult ThemMoiChuDe()
        {
            return View();
        }

        /// <summary>
        /// Hàm thêm mới một chủ đề
        /// </summary>
        /// <param name="objTinh"></param>
        /// <param name="fUpload"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemMoiChuDe(ChuDeBaiVietVeDiaDiem objChuDe)
        {
            if (ModelState.IsValid)
            {
                //thêm vào database
                DataProvider.Entities.ChuDeBaiVietVeDiaDiems.Add(objChuDe);
                //Lưu thay đổi
                DataProvider.Entities.SaveChanges();
            }
            return RedirectToAction("DanhSachChuDe");
        }

        /// <summary>
        /// Hàm xóa một chủ đề
        /// </summary>
        /// <returns></returns>
        public ActionResult XoaChuDe(int Id)
        {
            //Lấy đối tượng chủ đề
            ChuDeBaiVietVeDiaDiem objChuDe = DataProvider.Entities.ChuDeBaiVietVeDiaDiems.Find(Id);
            if (objChuDe != null)
            {
                //Xóa
                DataProvider.Entities.ChuDeBaiVietVeDiaDiems.Remove(objChuDe);
                //Lưu thay đổi
                DataProvider.Entities.SaveChanges();
            }
            return RedirectToAction("DanhSachChuDe");
        }

        public ActionResult CapNhatChuDe(int Id)
        {
            ChuDeBaiVietVeDiaDiem objChuDe = DataProvider.Entities.ChuDeBaiVietVeDiaDiems.Where(c => c.Id == Id).Single();
            return View(objChuDe);
        }

        /// <summary>
        /// Hàm cập nhật một chủ đề
        /// </summary>
        /// <param name="objTinh"></param>
        /// <param name="fUpload"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatChuDe(int Id, ChuDeBaiVietVeDiaDiem objChuDe)
        {
            var objOld_ChuDe = DataProvider.Entities.ChuDeBaiVietVeDiaDiems.Find(Id);
          
            if (objOld_ChuDe != null)
            {              
                DataProvider.Entities.Entry(objOld_ChuDe).CurrentValues.SetValues(objChuDe);
                //Lưu thay đổi
                DataProvider.Entities.SaveChanges();
            }
            return RedirectToAction("DanhSachChuDe");
        }
    }
}