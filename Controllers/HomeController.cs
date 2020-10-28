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
            //tìm kiếm theo từ khóa
            //if (!string.IsNullOrEmpty(tuKhoa))
            //{
            //    lstTinh = lstTinh.Where(c => c.TenTinh.Contains(tuKhoa));
            //}
            return View(lstTinh);         
        }      
       
    }
}