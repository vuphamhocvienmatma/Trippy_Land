using log4net;
using System;
using System.Web.Mvc;

namespace Trippy_Land.Areas.Admin.Controllers
{
    [SessionCheckAdmin]
    public class AdminController : Controller
    {
        private static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Admin/Admin
        public ActionResult Index()
        {
            try
            {
                logger.Info("Have an access to Admin page");
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