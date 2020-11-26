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
            return View();
        }
    }
}