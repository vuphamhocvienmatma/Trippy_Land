using System.Web.Mvc;

namespace Trippy_Land.Controllers
{
    public class ErrorPageController : Controller
    {

        public ActionResult Return()
        {
            return View();
        }

        public ActionResult Login()
        {
            return Content("<script language='javascript' type='text/javascript'>alert('Thanks for Feedback!');</script>");
        }
    }
}