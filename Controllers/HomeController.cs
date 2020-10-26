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
            //LoginController loginController = new LoginController();
            return View();
        }      
    }
}