using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trippy_Land.Models;

namespace Trippy_Land.Areas.Admin.Controllers
{
    public class DanhGiaController : Controller
    {
        private static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void HienThiDanhSachUser(int? idUser = null)
        {
            List<User> lstuser = DataProvider.Entities.Users.ToList();
            ViewBag.User = new SelectList(lstuser, "Id", "TenDangNhap", idUser.HasValue ? idUser.Value : 0);
        }
        // GET: Admin/DanhGia
        public ActionResult DanhSachDanhGia(string tuKhoa, int? idUser)
        {
            HienThiDanhSachUser();
            IQueryable<DanhGia> lstDanhGia = DataProvider.Entities.DanhGias;
            //tìm kiếm theo từ khóa
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                lstDanhGia = lstDanhGia.Where(c => c.NoiDungDanhGia.Contains(tuKhoa));
            }
            //Tìm kiếm theo User
            if (idUser.HasValue)
            {
                lstDanhGia = lstDanhGia.Where(b => b.idUser == idUser.Value);
            }
            return View(lstDanhGia);
        }

        //Thêm mới
        public ActionResult ThemMoiDanhGia()
        {
            HienThiDanhSachUser();
            return View(new DanhGia());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemMoiDanhGia(DanhGia DanhGia)
        {
            HienThiDanhSachUser();
            if (ModelState.IsValid)
            {
                //thêm vào database
                DataProvider.Entities.DanhGias.Add(DanhGia);
                //Lưu thay đổi
                DataProvider.Entities.SaveChanges();
            }
            return RedirectToAction("DanhSachDanhGia");         
        }

        //Xóa
        public ActionResult XoaDanhGia(int Id)
        {
            HienThiDanhSachUser();
            //Lấy đối tượng chủ đề
            DanhGia DanhGia = DataProvider.Entities.DanhGias.Find(Id);
            if (DanhGia != null)
            {
                //Xóa
                DataProvider.Entities.DanhGias.Remove(DanhGia);
                //Lưu thay đổi
                DataProvider.Entities.SaveChanges();
            }
            return RedirectToAction("DanhSachDanhGia");           
        }
    }
}