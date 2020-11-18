using log4net;
using System;
using System.Web.Mvc;
using Trippy_Land.Attribute;

namespace Trippy_Land.Areas.Admin.Controllers
{
    
    public class AdminController : Controller
    {
        private static readonly ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Admin/Admin
        [CheckAuthorize(PermissionName = "Index_Admin")]
        public ActionResult Index_Admin()
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