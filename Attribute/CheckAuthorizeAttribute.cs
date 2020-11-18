using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trippy_Land.Models;

namespace Trippy_Land.Attribute
{
    public class CheckAuthorizeAttribute : AuthorizeAttribute
    {
        public int RoleId { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = (User)httpContext.Session["User"];
            if (session != null && session.UserRoleId.Equals(RoleId))
            {
                return true;
            }
            else
            {
                return false;
            }          
        }
    }
}