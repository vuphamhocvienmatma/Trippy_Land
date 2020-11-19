﻿using System.Web.Mvc;

namespace Trippy_Land.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "RoleAndFunc", action = "DanhSachRoleAndFunc", id = UrlParameter.Optional }
            );
        }
    }
}