﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Trippy_Land.Areas
{
    public class SessionCheckAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if (session != null && session["Admin"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "Login",
                    controller = "ErrorPage",
                    area = ""
                }));
            }
        }
    }

}