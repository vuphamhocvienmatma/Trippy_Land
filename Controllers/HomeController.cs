using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trippy_Land.Models;

namespace Trippy_Land.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IQueryable<Tinh> lstTinh = DataProvider.Entities.Tinhs;           
            return View(lstTinh);         
        }
        public ActionResult AboutUs()
        {
            return View();
        }


        public ActionResult Blog()
        {
            List<BaiVietVeDiaDiem> lstBaiViet = DataProvider.Entities.BaiVietVeDiaDiems.ToList();
            return View(lstBaiViet);
        }

        public ActionResult BlogDetail()
        {
            return View();
        }

        /// <summary>
        /// Hàm lấy danh sách KS theo Id của tỉnh
        /// </summary>
        /// <param name="Id">Id của Tỉnh</param>
        /// <returns></returns>
        public ActionResult GetHotelByProvinceId(int? Id)
        {
            IQueryable<KhachSan> lstDanhSachKhachSan = DataProvider.Entities.KhachSans;
            if (Id.HasValue)
            {
                lstDanhSachKhachSan = lstDanhSachKhachSan.Where(b => b.idTinh == Id.Value);
            }
            return View(lstDanhSachKhachSan);
        }

        /// <summary>
        /// Hàm lấy danh sách thức ăn theo Id của tỉnh
        /// </summary>
        /// <param name="Id">Id của Tỉnh</param>
        /// <returns></returns>
        public ActionResult GetFoodByProvinceId(int? Id)
        {
            IQueryable<MonAn> lstDanhSachMonAn = DataProvider.Entities.MonAns;
            if (Id.HasValue)
            {
                lstDanhSachMonAn = lstDanhSachMonAn.Where(b => b.idTinh == Id.Value);
            }
            return View(lstDanhSachMonAn);          
        }

        /// <summary>
        /// Hàm lấy danh sách địa điểm theo Id của tỉnh
        /// </summary>
        /// <param name="Id">Id của Tỉnh</param>
        /// <returns></returns>
        public ActionResult GetLocationByProvinceId(int? Id)
        {
            IQueryable<DiaDiem> lstDanhSachDiaDiem = DataProvider.Entities.DiaDiems;
            if (Id.HasValue)
            {
                lstDanhSachDiaDiem = lstDanhSachDiaDiem.Where(b => b.idTinh == Id.Value);
            }
            return View(lstDanhSachDiaDiem);
        }
    }
}