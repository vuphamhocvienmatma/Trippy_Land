using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trippy_Land.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Admin/Admin
        public ActionResult Index()
        {
            try
            {
                logger.Info("Have an access to admin page");
                return View();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return View(ex);               
            }
           
        }
    }
}