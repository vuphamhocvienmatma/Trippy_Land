using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Trippy_Land.Models;

namespace Trippy_Land.Attribute
{
    public class CheckAuthorizeAttribute : AuthorizeAttribute
    {
        private static readonly ILog logger =
              LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string PermissionName { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //lấy thông tin của người dùng rồi lưu vào Session 
            User session = (User)httpContext.Session["UserOnline"];
            if (session != null)
            {                             
                //lấy chức năng 
                Function ChucNang = DataProvider.Entities.Function.Where(o => o.TenForm == PermissionName).First<Function>();
                if(ChucNang != null)
                {
                    List<UserRoleAndFunction> objroleandFunc = DataProvider.Entities.UserRoleAndFunctions.ToList();
                    try
                    {
                        //phải bằng nhau
                        foreach (UserRoleAndFunction item in objroleandFunc)
                        {
                            if (item.UserRoleId != 0 && item.UserRoleId == session.UserRoleId
                                && item.Function.TenForm == ChucNang.TenForm && item.Xem != false)
                                return true;
                            else
                                return false;                          
                        }   
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        return false;
                    }
                }               
            }       
            return false;                   
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
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