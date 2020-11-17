using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Trippy_Land
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            // Sets 404 HTTP exceptions to be handled via IIS (behavior is specified in the "httpErrors" section in the Web.config file)
            var error = Server.GetLastError();
            if ((error as HttpException)?.GetHttpCode() == 404)
            {
                Server.ClearError();
                Response.StatusCode = 404;
            }
        }

    }
}
