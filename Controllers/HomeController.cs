using log4net;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Trippy_Land.Models;
namespace Trippy_Land.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            try
            {
                IQueryable<Tinh> lstTinh = DataProvider.Entities.Tinhs;
                logger.Info("Have an access the website: Index");
                return View(lstTinh);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("Return", "ErrorPage");
            }
        }

        public ActionResult AboutUs()
        {
            try
            {
                logger.Info("Have an access the website: Aboutus");
                return View();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("Return", "ErrorPage");
            }
        }

        public ActionResult GetAllChuDe()
        {
            return View();
        }

        public ActionResult Blog(int? page, string sortOrder)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                List<BaiVietVeDiaDiem> lstBaiViet = DataProvider.Entities.BaiVietVeDiaDiems.ToList();
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                logger.Info("Have an access to the blog");
                return View(lstBaiViet.ToPagedList(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("Return", "ErrorPage");
            }
        }

        public ActionResult BlogDetail(int? Id)
        {
            try
            {
                BaiVietVeDiaDiem ObjbaiVietVeDiaDiem = DataProvider.Entities.BaiVietVeDiaDiems.Where(b => b.Id == Id).FirstOrDefault();
                return View(ObjbaiVietVeDiaDiem);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("Return", "ErrorPage");
            }

        }

        /// <summary>
        /// lọc bài viết theo địa điểm
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sortOrder"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult BlogbyLoacationId(int? page, string sortOrder, int? Id)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                IQueryable<BaiVietVeDiaDiem> lstBaiViet = DataProvider.Entities.BaiVietVeDiaDiems;

                if (Id.HasValue)
                {
                    lstBaiViet = lstBaiViet.Where(b => b.idDiaDiem == Id.Value);
                }
                return View(lstBaiViet.ToList().ToPagedList(pageNumber, pageSize));
            }
            catch (Exception ex)
            {

                logger.Error(ex.ToString());
                return RedirectToAction("Return", "ErrorPage");
            }

        }
        /// <summary>
        /// Hàm lấy danh sách KS theo Id của tỉnh
        /// </summary>
        /// <param name="Id">Id của Tỉnh</param>
        /// <returns></returns>
        public ActionResult GetHotelByProvinceId(int? page, string sortOrder, int? Id)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                IQueryable<KhachSan> lstDanhSachKhachSan = DataProvider.Entities.KhachSans;
                if (Id.HasValue)
                {
                    lstDanhSachKhachSan = lstDanhSachKhachSan.Where(b => b.idTinh == Id.Value);
                }
                return View(lstDanhSachKhachSan.ToList().ToPagedList(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("Return", "ErrorPage");
            }
        }

        /// <summary>
        /// Hàm lấy danh sách thức ăn theo Id của tỉnh
        /// </summary>
        /// <param name="Id">Id của Tỉnh</param>
        /// <returns></returns>
        public ActionResult GetFoodByProvinceId(int? page, string sortOrder, int? Id)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                IQueryable<MonAn> lstDanhSachMonAn = DataProvider.Entities.MonAns;
                if (Id.HasValue)
                {
                    lstDanhSachMonAn = lstDanhSachMonAn.Where(b => b.idTinh == Id.Value);
                }
                return View(lstDanhSachMonAn.ToList().ToPagedList(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("Return", "ErrorPage");
            }

        }

        /// <summary>
        /// Hàm lấy danh sách địa điểm theo Id của tỉnh
        /// </summary>
        /// <param name="Id">Id của Tỉnh</param>
        /// <returns></returns>
        public ActionResult GetLocationByProvinceId(int? page, string sortOrder, int? Id)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                IQueryable<DiaDiem> lstDanhSachDiaDiem = DataProvider.Entities.DiaDiems;
                if (Id.HasValue)
                {
                    lstDanhSachDiaDiem = lstDanhSachDiaDiem.Where(b => b.idTinh == Id.Value);
                }
                return View(lstDanhSachDiaDiem.ToList().ToPagedList(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return RedirectToAction("Return", "ErrorPage");
            }
        }
    }
}